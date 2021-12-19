using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    abstract class Node
    {
        public abstract atomType tip { get; }
        public abstract IEnumerable<Node> getChildrens();
    }
}