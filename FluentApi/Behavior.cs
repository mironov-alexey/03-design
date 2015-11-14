using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections.Immutable;
namespace FluentTask
{
    public class Behavior
    {
        private readonly ImmutableList<Action> _actions;

        public Behavior()
        {
            _actions = ImmutableList<Action>.Empty;
        }
        public Behavior(IEnumerable<Action> actions)
        {
            this._actions = actions.ToImmutableList();
        }

        public Behavior Say(string message)
        {
            return new Behavior(_actions.Add(() => Console.WriteLine(message)));
        }

        public Behavior UntilKeyPressed(Func<Behavior, Behavior> func)
        {
            return new Behavior(_actions.Add(() => { Wait(func); }));
        }

        private static void Wait(Func<Behavior, Behavior> inner)
        {
            while (!Console.KeyAvailable)
            {
                var behaviour = new Behavior();
                inner(behaviour).Execute();
                Thread.Sleep(500);
            }
            Console.ReadKey(true);
        }

        public Behavior Jump(JumpHeight height)
        {
            return new Behavior(_actions.Add(() => Console.WriteLine("Jump: " + height)));
        }

        public Behavior Delay(TimeSpan timeSpan)
        {
            return new Behavior(_actions.Add(() => Thread.Sleep(timeSpan)));
        }

        public void Execute()
        {
            foreach (var action in _actions)
                action();
        }

        public Behavior AddAction(Action action)
        {
            return new Behavior(_actions.Add(action));
        }
    }
}