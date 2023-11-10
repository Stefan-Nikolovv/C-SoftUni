using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandPattern.Core.Contracts;

namespace CommandPattern.Core
{
    public class Engine : IEngine
    {
        private ICommandInterpreter command;

        public Engine(ICommandInterpreter command)
        {
            this.command = command;
        }

        public void Run()
        {
            while (true)
            {
                string input = Console.ReadLine();
                try
                {
                    string output = command.Read(input);

                    Console.WriteLine(output);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }
        }
           
    }
}
