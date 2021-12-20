using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    class ParserClass
    {
        private readonly AtomLexical[] arrayAtomLexical;
        private int currentIndex;
        private AtomLexical currentAtomLexical => getAtom(0);
        private List<string> errorList;

        public ParserClass(String text)
        {
            List<AtomLexical> atomLexicals = new List<AtomLexical>();
            errorList = new List<string>();
            Lexer lexer = new Lexer(text);
            AtomLexical currAtom;
            do
            {
                currAtom = lexer.returnAtomLexical();
                if (currAtom.AtomType != atomType.Spatiu)
                {
                    atomLexicals.Add(currAtom);
                }
            } while (currAtom.AtomType != atomType.TerminalAtom);
            
            if (lexer.getErrorList().Count() > 0)
            {
                this.errorList = lexer.getErrorList();
            }
            arrayAtomLexical = atomLexicals.ToArray();
            
            //foreach (var i in arrayAtomLexical)
            //{
            //    Console.WriteLine(i.AtomType);
            //}
        }

        public List<string> getErrorList => this.errorList;

        private AtomLexical getAtom(int offset)
        {
            if (currentIndex+offset >= arrayAtomLexical.Length)
            {
                return this.arrayAtomLexical[arrayAtomLexical.Length - 1];
            }
            return this.arrayAtomLexical[currentIndex + offset];
        }

        private AtomLexical getCurrAtomAndIncrement()
        {
            var current = currentAtomLexical;
            currentIndex++;
            return current;
        }

        public AtomLexical checkAtomType(atomType type)
        {
            if (currentAtomLexical.AtomType == type)
            {
                return getCurrAtomAndIncrement();
            }
            errorList.Add("< Error! Invalid atom lexical type!\n Tip atom dat: " + currentAtomLexical.tip.ToString() + ";\n Tip atom asteptat: " + type.ToString() + " >");
            return new AtomLexical(type, currentAtomLexical.Index, null, null);
        }

        // 13 + 43 - 21*1
        // 13 + 43 - 21
        // *
        // 1
        
        // 13 + 43
        // -
        // 21

        // 13
        // + 
        // 43

        
        // 11 + 2*3

        private Expresie parseFirstExpression()
        {
            //if (currentAtomLexical.AtomType == atomType.ParantezaDeschisa)
            //{
            //    var stanga = getCurrAtomAndIncrement();
            //    var expresie = parseTerms();
            //    var dreapta = checkAtomType(atomType.ParantezaInchisa);
            //    return new ExpresieParanteze(stanga, expresie, dreapta);
            //}
            //var numar = checkAtomType(atomType.nr_intreg);
            //return new ExpresieNumerica(numar);
            if (currentAtomLexical.AtomType == atomType.ParantezaDeschisa)
            {
                var stanga = getCurrAtomAndIncrement();
                var expresie = parseTerms();
                AtomLexical dreapta;
                if (currentAtomLexical.AtomType == atomType.ParantezaInchisa)
                {
                    dreapta = getCurrAtomAndIncrement();
                }
                else
                {
                    errorList.Add($"Atom Lexical Invalid {currentAtomLexical.AtomType} se asteapta {atomType.ParantezaInchisa}");
                    throw new Exception("Parser: Se astepta paranteza inchisa");
                }

                return new ExpresieParanteze(stanga, expresie, dreapta);
            }

            var val = getCurrAtomAndIncrement();
            return new ExpresieNumerica(val);
            //switch (val.AtomType)
            //{
            //    case atomType.nr_intreg:
            //        // do stuff
            //        return new ExpresieNumerica(val);
            //        break;
            //    case atomType.nr_float:
            //        // do stuff
            //        break;
            //    case atomType.text:
            //        // do stuff
            //        break;
            //    default:
            //        break;
            //}
        }

        public Expresie parseTerms()
        {
            var stanga = parseFactors();
            while (currentAtomLexical.AtomType == atomType.Plus || currentAtomLexical.AtomType == atomType.Minus)
            {
                var op = getCurrAtomAndIncrement();
                var dreapta = parseFactors();
                stanga = new ExpresieBinara(stanga, op, dreapta);
            }
            return stanga;
        }

        public Expresie parseFactors()
        {
            var stanga = parseFirstExpression();
            while (currentAtomLexical.AtomType == atomType.Inmultire || currentAtomLexical.AtomType == atomType.Impartire)
            {
                var op = getCurrAtomAndIncrement();
                var dreapta = parseFirstExpression();
                stanga = new ExpresieBinara(stanga, op, dreapta);
            }
            return stanga;
        }

        public Arbore parseExpression()
        {
            var expresie = parseTerms();
            var end = checkAtomType(atomType.TerminalAtom);
            return new Arbore(expresie, end);
        }
    }
}