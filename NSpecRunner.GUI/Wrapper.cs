using System;
using NSpec.Domain;

namespace NSpecRunner.GUI
{
    public class Wrapper : MarshalByRefObject
    {
        public int Execute(RunnerInvocation invocation, Func<RunnerInvocation, int> action)
        {
            return action(invocation);
        }

        public ContextWrapper Execute(RunnerInvocation invocation, Func<RunnerInvocation, ContextWrapper> action)
        {
            return action(invocation);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }
    }
}