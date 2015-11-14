using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace DIContainer.Commands
{
    public class HelpCommand : BaseCommand
    {
        private readonly Lazy<ICommand[]> _commands;

        public HelpCommand(Lazy<ICommand[]> commands)
        {
            _commands = commands;
        }

        public override void Execute()
        {
            foreach (var command in _commands.Value)
                Writer.WriteLine(command.Name);
        }
    }
}
