using System;
using System.Collections.Generic;
using System.Text;

namespace KernelOne.Applications
{
    public abstract class Application
    {
        public readonly string Name;

        public Application(string name)
        {
            Name = name;
        }

        public abstract void Run();
    }
}
