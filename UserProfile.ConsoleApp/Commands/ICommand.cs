using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;

namespace UserProfile.ConsoleApp.Commands
{
    public interface ICommand
    {
        void Execute(IProfileService service, object[] parameters);
    }
}
