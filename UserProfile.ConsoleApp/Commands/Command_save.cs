using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;
using UserProfile.Application.Model;
using UserProfile.Common.Helpers;

namespace UserProfile.ConsoleApp.Commands
{
    public class Command_save : ICommand
    {        
        public void Execute(IProfileService service, object[] parameters)
        {
            Console.WriteLine("Вызову команды предшествует заполнение анкеты");
        }
    }
}
