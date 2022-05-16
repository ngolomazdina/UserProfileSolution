using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;

namespace UserProfile.ConsoleApp.Commands
{
    public class Command_delete : ICommand
    {       
        public void Execute(IProfileService service, object[] parameters)
        {
            var name = (string)parameters[2];            
            var deleteReslt = service.DeleteProfile(name);
            if (deleteReslt.Code != Common.CodeResult.Success)
                Console.WriteLine($"При удалении анкеты {0} произошла ошибка {deleteReslt.Message}");
            else
                Console.WriteLine(deleteReslt.Message);
        }
    }
}
