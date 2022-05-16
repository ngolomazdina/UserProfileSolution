using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Infrastructure.DBModel;
using UserProfile.Common;

namespace UserProfile.Infrastructure
{
    public interface IRepository<T>
        where T: DbProfile
    {        
        OperationResult Create(string fileName, T profile);
        OperationResult Update(string fileName, T profile);
        OperationResult Delete(string fileName);
        OperationResult<List<T>> GetProfiles(string fileName = null);
        OperationResult<List<string>> FindProfiles(bool isTodayList = true);
    }
}
