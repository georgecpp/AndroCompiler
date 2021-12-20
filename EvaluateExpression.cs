using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    class EvaluateExpression
    {
        private readonly Expresie exp;

        public EvaluateExpression(Expresie expresie)
        {
            exp = expresie;
        }

        private object evaluate(Expresie expresie)
        {
            if (expresie is ExpresieNumerica n)
            {
                switch(n.tip)
                {
                    case atomType.nr_float:
                        return (decimal)n.Numar.Value;
                    case atomType.nr_intreg:
                        return (int)n.Numar.Value;
                    case atomType.text:
                        return (string)n.Numar.Value;
                }
            }
            if (expresie is ExpresieParanteze p)
            {
                return evaluate(p.Expresie);
            }
            if (expresie is ExpresieBinara b)
            {
                var stg = b.ExpresieStg;
                var dr = b.ExpresieDr;
                var op = b.OperatorLexical;

                if (op.tip == atomType.Plus)
                {
                    return (dynamic) evaluate(stg) + (dynamic) evaluate(dr);
                }
                if (op.tip == atomType.Minus)
                {
                    return (dynamic) evaluate(stg) - (dynamic) evaluate(dr);
                }
                if (op.tip == atomType.Inmultire)
                {
                    return (dynamic) evaluate(stg) * (dynamic) evaluate(dr);
                }
                if (op.tip == atomType.Impartire)
                {
                    return (dynamic) evaluate(stg) / (dynamic) evaluate(dr);
                }
            }
            throw new Exception("Expresie invalida!");
        }

        public object getEvaluate => evaluate(exp);
    }
}