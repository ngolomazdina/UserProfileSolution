using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;
using UserProfile.Common;

namespace UserProfile.ConsoleApp.Commands
{
    public class Command_list_today : ICommand
    {        
        public void Execute(IProfileService service, object[] parameters)
        {
            var findListResult = service.FindProfiles(true);
            if (findListResult.Code != CodeResult.Success)
            {
                Console.WriteLine($"При получении списка анкет произошла ошибка {findListResult.Message}");
                return;
            }

            Console.WriteLine($"Список заполненных {DateTime.Today.ToShortDateString()} анкет");
            foreach (var p in findListResult.Data)
                Console.WriteLine($"{p}");
        }
    }
}
