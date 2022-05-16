using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;
using UserProfile.Application.Model;
using UserProfile.Common;


namespace UserProfile.ConsoleApp.Commands
{
    class Command_statistics : ICommand
    {        
        public void Execute(IProfileService service, object[] parameters)
        {
            var statistic = service.GetStatistic();

            if (statistic.Code != CodeResult.Success)
            {
                Console.WriteLine($"При получении данных о статистике возникла ошибка {statistic.Message}");
                return;
            }

            Console.WriteLine($"Средний возраст всех опрошенных: {statistic.Data.AverageAge.ToString()}");
            Console.WriteLine($"Самый популярный язык программирования: {statistic.Data.MostPopularLang}");
            Console.WriteLine($"Самый опытный программист: {statistic.Data.Senior}");

        }
    }
}
