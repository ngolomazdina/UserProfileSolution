using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfile.Application.Model
{
    public class Profile
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
