using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    sealed class ExpresieBinara : Expresie
    {
        public Expresie ExpresieStg { get; }
        public Expresie ExpresieDr { get; }
        public AtomLexical OperatorLexical { get; }

        public ExpresieBinara(Expresie expresieStg, AtomLexical operatorLexical, Expresie expresieDr)
        {
            ExpresieStg = expresieStg;
            ExpresieDr = expresieDr;
            OperatorLexical = operatorLexical;
        }

        public override atomType tip => atomType.ExpresieBinara;

        public override IEnumerable<Node> getChildrens()
        {
            yield return ExpresieStg;
            yield return OperatorLexical;
            yield return ExpresieDr;
        }
    }
}