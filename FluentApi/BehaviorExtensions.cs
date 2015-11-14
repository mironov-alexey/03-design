using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentTask
{
    static class BehaviorExtensions
    {
        public static Behavior Run(this Behavior behavior, string text)
        {
            return behavior.AddAction(() => Console.WriteLine("Run " + text));
        }
    }
}
