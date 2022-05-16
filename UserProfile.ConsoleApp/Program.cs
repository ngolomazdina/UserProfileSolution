using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using UserProfile.Application.BL;
using UserProfile.Application.Model;
using UserProfile.Common.ConfigElements;
using UserProfile.ConsoleApp.Commands;
using UserProfile.Infrastructure;
using UserProfile.Infrastructure.DBModel;

namespace UserProfile.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            ConsoleKeyInfo key;
            List<object> parameters = new List<object>();
            List<Question> questions = new List<Question>();
            List<Answer> answers = new List<Answer>();

            var container = AutofacContainer.ConfigureContainer();

            var repository = container.Resolve<IRepository<DbProfileTextFile>>();
            var service = container.Resolve<IProfileService>();

            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            QuestionConfigElementConfigSection questionSections = (QuestionConfigElementConfigSection)cfg.Sections["QuestionConfigElement"];
            if (questionSections == null)
            {
                Console.WriteLine("В файле конфигураций не перечислены вопросы анкеты");
                return;
            }
            foreach(QuestionConfigElement q in questionSections.CustomItems)            
                questions.Add(new Question() {  Id= int.Parse(q.Id), QuestionName= q.QuestionName, QuestionText=q.QuestionText, AnswerType=q.AnswerType});
            

            Console.WriteLine("Выберите действие");

            do
            {
                key = Console.ReadKey();
                string[] consoleCommandParams = Console.ReadLine().Split(' ');

                var command = CommandFactory.GetCommand(consoleCommandParams[0]);

                parameters.Add(questions);
                parameters.Add(answers);
                parameters.AddRange(consoleCommandParams.Skip(1));

                if (command == null)
                    Console.WriteLine("Команда с указанным именем не обнаружена. Используйте команду help для справки");
                else 
                { 
                    command.Execute(service, parameters.ToArray());
                    parameters.Clear();
                }
                    
            }
            while (key.Key != ConsoleKey.Escape);
            
        }

    }
}
