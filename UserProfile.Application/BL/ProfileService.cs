using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.Model;
using UserProfile.Common;
using UserProfile.Infrastructure;
using UserProfile.Infrastructure.DBModel;

namespace UserProfile.Application.BL
{
    public class ProfileService<T> : IProfileService
        where T: DbProfile
    {
        IRepository<T> _repository;
                
        public ProfileService(IRepository<T> repository)
        {
            _repository = repository;            
        }
                    
        public OperationResult DeleteProfile(string profileName)
        {
            return _repository.Delete(profileName); 
        }

        public OperationResult<Profile> GetProfile(string profileName)
        {                        
            var dbProfile = _repository.GetProfiles(profileName);
            if (dbProfile.Code != CodeResult.Success)
                return new OperationResult<Profile>() { Code=dbProfile.Code, Message=dbProfile.Message};

            if (dbProfile.Data.Count == 0)
                return new OperationResult<Profile>() { Code = CodeResult.Success, Message = $"Файл {profileName} не найден" };


            var result = new OperationResult<Profile>() { Code = CodeResult.Error };

            try
            {
                var profile = ProfileConverter.ConvertFromRepositoryEntity(dbProfile.Data.FirstOrDefault());
                result.Data = profile;
                result.Code = CodeResult.Success;
            }
            catch
            {
                result.Message="В процессе конвертации анкеты возникла ошибка";
            }

            return result;
        }
       
        public OperationResult<List<string>> FindProfiles(bool isTodayList = true)
        {            
            return _repository.FindProfiles(isTodayList);
        }

        public OperationResult<Statistic> GetStatistic()
        {
            OperationResult<Statistic> result= new OperationResult<Statistic>();

            try
            {
                Statistic statistic = new Statistic();
                List<int> ages = new List<int>();
                List<string> langs = new List<string>();
                Dictionary<string, int> devs = new Dictionary<string, int>();

                foreach( var dbProfile in _repository.GetProfiles().Data)
                {
                    var profile = ProfileConverter.ConvertFromRepositoryEntity(dbProfile);
                    var lang = (from ans in profile.Answers where ans.QuestionId == 3 select ans.AnswerBody).FirstOrDefault();
                    var name = (from ans in profile.Answers where ans.QuestionId == 1 select ans.AnswerBody).FirstOrDefault(); 
                    var ex = (from ans in profile.Answers where ans.QuestionId == 4 select ans.AnswerBody).FirstOrDefault();
                    var birthDay = (from ans in profile.Answers where ans.QuestionId == 2 select ans.AnswerBody).FirstOrDefault();
                    var age = DateTime.Today.Year - DateTime.Parse(birthDay).Year;

                    ages.Add(age);
                    langs.Add(lang);
                    devs.Add(name, int.Parse(ex));                   
                }

                var langGroups = langs.GroupBy(x => x).Select(g => new { Text = g.Key, Count = g.Count() });
                var max = langGroups.Max(c => c.Count);

                statistic.AverageAge = (int)ages.Average();
                statistic.MostPopularLang = (from lg in langGroups where lg.Count == max select lg).FirstOrDefault().Text.Trim();
                statistic.Senior = (from d in devs.OrderByDescending(x=>x.Value) select d).First().Key.Trim();

                result.Code = CodeResult.Success;
                result.Data = statistic;

            }
            catch
            {
                result.Code = CodeResult.Error;
            }

            return result;
        }

        public OperationResult SaveProfile(string profileName, List<Question> questions, Profile profile)
        {
            OperationResult result; 

            try
            {                
                var dbProfile = ProfileConverter.ConvertToRepositoryEntity(profile, questions, _repository.GetType().Name);
                result = _repository.Create(profileName, dbProfile as T);
            }
            catch
            {
                result = new OperationResult() { Code = CodeResult.Error, Message="Конвертация анкеты произошла с ошибкой" };
            }
            return result;
        }
        
    }
}
