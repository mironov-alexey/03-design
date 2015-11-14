using System;
using System.IO;
using Ninject;

namespace DIContainer.Commands
{
    public class PrintTimeCommand : BaseCommand
    {
        public override void Execute()
        {
            Writer.WriteLine(DateTime.Now);
        }
    }
}