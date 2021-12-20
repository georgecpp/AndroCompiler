using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    class ExpresieNumerica : Expresie
    {
        public AtomLexical Numar { get; }

        public ExpresieNumerica(AtomLexical numar)
        {
            Numar = numar;
        }

        public override atomType tip => Numar.AtomType;

        public override IEnumerable<Node> getChildrens()
        {
            yield return Numar;
        }
    }
}