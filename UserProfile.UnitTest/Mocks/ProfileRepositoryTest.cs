using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Common;
using UserProfile.Infrastructure;
using UserProfile.Infrastructure.DBModel;

namespace UserProfile.UnitTest.Mocks
{
    public class ProfileRepositoryTest : IRepository<DbProfileTextFile>
    {
        public OperationResult Create(string fileName, DbProfileTextFile profile)
        {
            throw new NotImplementedException();
        }

        public OperationResult Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        public OperationResult<List<string>> FindProfiles(bool isTodayList = true)
        {
            throw new NotImplementedException();
        }

        public OperationResult<List<DbProfileTextFile>> GetProfiles(string fileName = null)
        {
            var result = new OperationResult<List<DbProfileTextFile>>();

            result.Code = CodeResult.Success;
            result.Data = new List<DbProfileTextFile>();

            var profile1 = new DbProfileTextFile();
            
            profile1.Text = $"1. ФИО: Иванов Иван Иванович{Environment.NewLine}2. Дата рождения: 04.12.1990{Environment.NewLine}3. Любимый язык программирования: C#{Environment.NewLine}4. Опыт программирования на указанном языке: 5{Environment.NewLine}5. Мобильный телефон: 11111111{Environment.NewLine}{Environment.NewLine}Анкета заполнена: 19.05.2022";
            result.Data.Add(profile1);

            var profile2 = new DbProfileTextFile();
            profile2.Text = $"1. ФИО: Сидоров Сидор Сидорович{Environment.NewLine}2. Дата рождения: 07.06.1987{Environment.NewLine}3. Любимый язык программирования: ruby{Environment.NewLine}4. Опыт программирования на указанном языке: 8{Environment.NewLine}5. Мобильный телефон: 22222222{Environment.NewLine}{Environment.NewLine}Анкета заполнена: 19.05.2022";
            result.Data.Add(profile2);

            var profile3 = new DbProfileTextFile();
            profile3.Text = $"1. ФИО: Петров Петр Петрович{Environment.NewLine}2. Дата рождения: 14.05.1985{Environment.NewLine}3. Любимый язык программирования: ruby{Environment.NewLine}4. Опыт программирования на указанном языке: 12{Environment.NewLine}5. Мобильный телефон: 33333333{Environment.NewLine}{Environment.NewLine}Анкета заполнена: 19.05.2022";
            result.Data.Add(profile3);

            var profile4 = new DbProfileTextFile();
            profile4.Text = $"1. ФИО: Ивашкин Иван Петрович{Environment.NewLine}2. Дата рождения: 17.09.1991{Environment.NewLine}3. Любимый язык программирования: C#{Environment.NewLine}4. Опыт программирования на указанном языке: 9{Environment.NewLine}5. Мобильный телефон: 444444444{Environment.NewLine}{Environment.NewLine}Анкета заполнена: 19.05.2022";
            result.Data.Add(profile4);

            var profile5 = new DbProfileTextFile();
            profile5.Text = $"1. ФИО: Сидорушкин Иван Петрович{Environment.NewLine}2. Дата рождения: 25.10.1995{Environment.NewLine}3. Любимый язык программирования: C#{Environment.NewLine}4. Опыт программирования на указанном языке: 3{Environment.NewLine}5. Мобильный телефон: 555555555{Environment.NewLine}{Environment.NewLine}Анкета заполнена: 19.05.2022";
            result.Data.Add(profile5);

            return result;
        }

        public OperationResult Update(string fileName, DbProfileTextFile profile)
        {
            throw new NotImplementedException();
        }
    }
}
