using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    class ExpresieText : Expresie
    {
        public AtomLexical atomLexicalString { get; }

        public ExpresieText(AtomLexical atomLexical)
        {
            atomLexicalString = atomLexical;
        }

        public override atomType tip => atomType.text;

        public override IEnumerable<Node> getChildrens()
        {
            yield return atomLexicalString;
        }
    }
}