using System;
using System.IO;
using System.Linq;
using DIContainer.Commands;
using Ninject;
using Ninject.Extensions.Conventions;
using FakeItEasy.Configuration;


namespace DIContainer
{
    public class Program
    {
        private readonly CommandLineArgs arguments;
        private readonly ICommand[] commands;

        public Program(CommandLineArgs arguments, params ICommand[] commands)
        {
            this.arguments = arguments;
            this.commands = commands;
        }

        static void Main(string[] args)
        {
            var container = new StandardKernel();
            container.Bind<CommandLineArgs>()
                .ToSelf()
//                .WithConstructorArgument(new[] { "printTime" });
                .WithConstructorArgument(new[] { "timer", "2000" });
//                .WithConstructorArgument(new[] { "help" });
            container.Bind(x =>
            {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .BindDefaultInterfaces();
            });
            container.Bind<TextWriter>().ToConstant(Console.Out);
            container.Get<Program>().Run();
        }

        public void Run()
        {
            if (arguments.Command == null)
            {
                Console.WriteLine("Please specify <command> as the first command line argument");
                return;
            }
            var command = commands.FirstOrDefault(c => c.Name.Equals(arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                Console.WriteLine("Sorry. Unknown command {0}", arguments.Command);
            else
                command.Execute();
        }
    }
}
