using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserProfile.Application.BL;
using UserProfile.Application.Model;
using UserProfile.Common.Enums;
using UserProfile.Common.Extentions;

namespace UserProfile.ConsoleApp.Commands
{
    class Command_new_profile : ICommand
    {        
        public void Execute(IProfileService service, object[] parameters)
        {
            var questions = (List<Question>)parameters[0];
            var answers = (List<Answer>)parameters[1];

            for (int i = 1; i< questions.Count+1; i++)
            {
                var question = (from q in questions where q.Id == i select q).First();
                Console.WriteLine(question.QuestionText);
                var ansText = Console.ReadLine().Trim();

                switch (ansText)
                {
                    case var someVal when new Regex("-goto_question [1-5]").IsMatch(someVal):
                        var position = int.Parse(ansText.Split(' ')[1]);
                        var answer = (from a in answers where a.QuestionId == position select a).FirstOrDefault();
                        if (answer!=null)
                            answers.Remove(answer);

                        i = position - 1;                        
                        break;
                    case "-goto_prev_question":
                        answers.RemoveAt(i-2);
                        i = i - 2;
                        break;
                    case "-restart_profile":
                        i = 0;
                        answers.Clear();
                        break;                    
                    case "-save":
                        if (answers.Count<5)
                        {
                            Console.WriteLine("Требуется ответить на все вопросы анкеты");
                            i--;                           
                        }
                        break;
                        
                    default:                        
                        if (ansText.StartsWith("-"))
                        {
                            Console.WriteLine("Команда с указанным именем не применима при заполнении анкеты");
                            i--;
                        }
                        else
                        {
                            var duble = (from a in answers where a.QuestionId == i select a).FirstOrDefault();
                            if (duble != null)
                                answers.Remove(duble);

                            if(i == 3)
                            {

                                var langs = new List<string>();
                                foreach (Langs l in Enum.GetValues(typeof(Langs)))
                                    langs.Add(l.ToDisplayString().ToLower());
                                
                                if(!langs.Contains(ansText.ToLower()))
                                {
                                    Console.WriteLine("Неизвестный язык программирования");
                                    i--;                                   
                                }
                            }

                            if(i == 2)
                            {
                                Regex regex = new Regex(@"(0[1-9]|[12][0-9]|3[01])[\.](0[1-9]|1[012])[\.](19|20)\d\d");
                                
                                if (!regex.IsMatch(ansText.ToLower()))
                                {
                                    Console.WriteLine("Формат даты рождения ДД.ММ.ГГГГ");
                                    i--;                                   
                                }
                            }
                            
                            var ans = new Answer() { Id = question.Id, QuestionId = question.Id, AnswerBody = ansText };
                            answers.Add(ans);
                        }
                        break;
                }
            }

            if (Console.ReadLine().Trim() == "-save")
            {
                var profileName = $"{(from a in answers where a.QuestionId == 1 select a).First().AnswerBody.Replace(" ","_")}_{DateTime.Today.ToShortDateString()}";
                var profile = new Profile() { Uid = Guid.NewGuid(), Answers = answers, Name = profileName };
                var saveReslt = service.SaveProfile(profileName, questions, profile);

                if (saveReslt.Code != Common.CodeResult.Success)
                    Console.WriteLine($"При сохранении анкеты {profileName} произошла ошибка {saveReslt.Message}");
                else
                    Console.WriteLine($"Анкета успешно сохранена");
            }
            else
            {
                Console.WriteLine($"Воспользуйтесь командой -help и заполните анкету снова");
            }
        }
    }
}
