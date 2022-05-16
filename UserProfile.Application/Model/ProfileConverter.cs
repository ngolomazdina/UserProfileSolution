using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.Model;
using UserProfile.Infrastructure.DBModel;
using UserProfile.Common.Helpers;

namespace UserProfile.Application
{
    public static class ProfileConverter
    {
        private static NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();

        public static DbProfile ConvertToRepositoryEntity(Profile profile, List<Question> questions, string repositoryTypeName)
        {
            DbProfile dbProfile = null;

            if (repositoryTypeName == "ProfileRepositoryTextFile")
                dbProfile = GetProfileText(profile, questions);

            return dbProfile;
        }

        public static Profile ConvertFromRepositoryEntity(DbProfile dbProfile)
        {
            Profile profile = null;

            if (dbProfile.GetType() != typeof(DbProfileTextFile))
                return profile;

            try
            {
                profile = new Profile() { Uid= Guid.NewGuid(), Answers= new List<Answer>() };

                var profileText = dbProfile as DbProfileTextFile;

                string[] profileLines = profileText.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                foreach(string ln in profileLines)
                {
                    if (string.IsNullOrEmpty(ln) || string.IsNullOrWhiteSpace(ln))
                        continue;

                    int res;                    
                    if (!Int32.TryParse(ln.Substring(0, 1), out res))
                        continue;

                    string[] lnParts = ln.Split('.');
                    Int32.TryParse(lnParts[0], out res);

                    profile.Answers.Add(new Answer() { Id= res, QuestionId= res, AnswerBody= ln.Split(':')[1] });
                }
            }
            catch(Exception ex)
            {
                _log.Error(LogHelper.GetErrorMessage(string.Empty, ex));
            }
            return profile;
        }

        internal static DbProfileTextFile GetProfileText(Profile profile, List<Question> questions)
        {
            DbProfileTextFile profileText = null; 
            
            try
            {
                profileText = new DbProfileTextFile() { CreateDate = DateTime.Today.ToString() };
                foreach (Answer ans in from a in profile.Answers
                                        orderby a.QuestionId
                                        select a)
                {
                    string questionText = (from q in questions
                                            where q.Id == ans.QuestionId
                                            select q).FirstOrDefault().QuestionText;

                    string newProfileLine = $"{ans.QuestionId.ToString()}. {questionText}: {ans.AnswerBody}";

                    profileText.Text = $"{profileText.Text}{Environment.NewLine}{newProfileLine}";
                }

                profileText.Text = $"{profileText.Text}{Environment.NewLine}Анкета заполнена {profileText.CreateDate}";
            }
            catch (Exception ex)
            {
                _log.Error(LogHelper.GetErrorMessage(profile.Uid.ToString(), ex));
            }

            return profileText;
        }
    }
}
