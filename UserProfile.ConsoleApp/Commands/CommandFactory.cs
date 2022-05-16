using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;

namespace UserProfile.ConsoleApp.Commands
{
    public static class CommandFactory
    {
        public static ICommand GetCommand(string commandName)
        {
            var commands = from type in Assembly.GetExecutingAssembly().GetTypes()
                           where typeof(ICommand).IsAssignableFrom(type) && !type.IsInterface
                           select type;


            var command = (from cmd in commands
                           where cmd.Name.Contains(commandName.ToLower())
                           select cmd).FirstOrDefault();

            if (command == null)
                return null;
            
            return  (ICommand)Activator.CreateInstance(command);
        }
    }
}
