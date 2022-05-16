using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.Model;
using UserProfile.Common;

namespace UserProfile.Application.BL
{
    public interface IProfileService
    {
        OperationResult<Statistic> GetStatistic();
        OperationResult SaveProfile(string profileName, List<Question> questions, Profile profile);
        OperationResult DeleteProfile(string profileFullName);
        OperationResult<Profile> GetProfile(string profileFullName);
        OperationResult<List<string>> FindProfiles(bool isTodayList = true);
        
    }
}
