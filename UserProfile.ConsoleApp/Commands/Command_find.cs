using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;
using UserProfile.Common;

namespace UserProfile.ConsoleApp.Commands
{
    public class Command_find : ICommand
    {        
        public void Execute(IProfileService service, object[] parameters)
        {
            string name;

            try
            {
                name = (string)parameters[2];
            }
            catch
            {
                Console.WriteLine($"Требуется указать имя файла");
                return;
            }

            var findResult= service.GetProfile(name);
            if (findResult.Code!=CodeResult.Success)
            {
                Console.WriteLine($"При поиске файла {name} произошла ошибка {findResult.Message}");
                return;
            }

            if (findResult.Data == null)
            {
                Console.WriteLine(findResult.Message);
                return;
            }

            foreach(var a in findResult.Data.Answers.OrderBy(a=>a.QuestionId))
                Console.WriteLine($"{a.QuestionId} {a.AnswerBody}");            
        }
    }
}
