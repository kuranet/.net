using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Commands
{
    public abstract class Command
    {
        public abstract void Execute();
    }

    public abstract class Command<T>
    {
        public abstract void Execute(T t1);
    }

    public abstract class Command<T, G>
    {
        public abstract void Execute(T t1, G t2);
    }
}
