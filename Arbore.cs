using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    class Arbore
    {
        public Expresie Root { get; }
        public AtomLexical End { get; }

        public Arbore(Expresie root, AtomLexical end)
        {
            Root = root;
            End = end;
        }
    }
}