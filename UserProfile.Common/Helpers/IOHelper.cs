using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Common.ConfigElements;

namespace UserProfile.Common.Helpers
{
    public sealed class IOHelper
    {
        public static string GetProfilesPath()
        {
            string profilesPath = null;

            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            LocationConfigElementConfigSection profileLocation = (LocationConfigElementConfigSection)cfg.Sections["LocationConfigElement"];
            if (profileLocation == null)
                return profilesPath;

            var profileDirectory = profileLocation.CustomItems[0].Value;

            profilesPath = profileDirectory.Contains(":")
                ? profileDirectory
                : Path.Combine(Environment.CurrentDirectory, profileDirectory);

            return profilesPath;
        }
    }
}
