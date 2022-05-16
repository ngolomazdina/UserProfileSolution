using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfile.Infrastructure.DBModel
{
    public class DbProfileTextFile: DbProfile
    {
        public string CreateDate { get; set; }
        public string ProfileFileName { get; set; }
        public string Text { get; set; }

    }
}
