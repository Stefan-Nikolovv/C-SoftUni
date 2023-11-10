using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] arguments = args.Split(' ');

            string commandName = arguments[0];
            string[] commandArgumnets = arguments.Skip(1).ToArray();


            Type commnadType = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == $"{commandName}Command");

            if ( commnadType == null ) 
            {
                throw new InvalidOperationException("Command is invalid");
            }

            ICommand commadnInstance = Activator.CreateInstance( commnadType ) as ICommand;

           string result = commadnInstance.Execute(commandArgumnets);
            return result;
        }
    }
}
