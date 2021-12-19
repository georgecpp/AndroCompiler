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

        private int evaluate(Expresie expresie)
        {
            if (expresie is ExpresieNumerica n)
            {
                return (int)n.Numar.Value;
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
                    return evaluate(stg) + evaluate(dr);
                }
                if (op.tip == atomType.Minus)
                {
                    return evaluate(stg) - evaluate(dr);
                }
                if (op.tip == atomType.Inmultire)
                {
                    return evaluate(stg) * evaluate(dr);
                }
                if (op.tip == atomType.Impartire)
                {
                    return evaluate(stg) / evaluate(dr);
                }
            }
            throw new Exception("Expresie invalida!");
        }

        public int getEvaluate => evaluate(exp);
    }
}