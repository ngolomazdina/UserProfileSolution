using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;

namespace UserProfile.ConsoleApp.Commands
{
    public class Command_exit : ICommand
    {        
        public void Execute(IProfileService service, object[] parameters)
        {
            Console.WriteLine("Вы уверены, что хотите закрыть приложение? Данные не сохранятся (д/н)");

            ConsoleKeyInfo key= Console.ReadKey();
            if ((int)key.Key != 76)
                Environment.Exit(0);
        }
    }
}
