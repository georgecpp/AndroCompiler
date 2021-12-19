using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    enum atomType { Numar, 
        Spatiu, 
        Plus, 
        Minus, 
        Inmultire, 
        Impartire, 
        ParantezaDeschisa, 
        ParantezaInchisa,
        InvalidAtom,
        TerminalAtom,
        ExpresieBinara,
        ExpresieParanteze
    }

    class AtomLexical : Node
    {
        public atomType AtomType { get; }
        public int Index { get; }
        public string Content { get; }
        public object Value { get; }

        public override atomType tip => AtomType;

        public AtomLexical(atomType atom, int index, String content, object value)
        {
            AtomType = atom;
            Index = index;
            Content = content;
            Value = value;
        }

        public override IEnumerable<Node> getChildrens()
        {
            return Enumerable.Empty<Node>();
        }
    }
}
