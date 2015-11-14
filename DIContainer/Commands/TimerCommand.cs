using System;
using System.IO;
using System.Threading;

namespace DIContainer.Commands
{
    public class TimerCommand : BaseCommand
    {
        private readonly CommandLineArgs arguments;
        private readonly TextWriter _writer;

        public TimerCommand(CommandLineArgs arguments, TextWriter writer)
        {
            this.arguments = arguments;
            _writer = writer;
        }

        public override void Execute()
        {
            var timeout = TimeSpan.FromMilliseconds(arguments.GetInt(0));
            _writer.WriteLine("Waiting for " + timeout);
            Thread.Sleep(timeout);
            _writer.WriteLine("Done!");
        }
    }
}