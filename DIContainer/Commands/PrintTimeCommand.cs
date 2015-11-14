using System;
using System.IO;

namespace DIContainer.Commands
{
    public class PrintTimeCommand : BaseCommand
    {
        private readonly TextWriter _writer;

        public PrintTimeCommand(TextWriter writer)
        {
            _writer = writer;
        }

        public override void Execute()
        {
            _writer.WriteLine(DateTime.Now);
        }
    }
}