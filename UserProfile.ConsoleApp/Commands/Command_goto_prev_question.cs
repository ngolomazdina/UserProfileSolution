using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;

namespace UserProfile.ConsoleApp.Commands
{
    public class Command_goto_prev_question : ICommand
    {       
        public void Execute(IProfileService service, object[] parameters)
        {
            Console.WriteLine("Вызову команды предшествует заполнение анкеты");
        }
    }
}
