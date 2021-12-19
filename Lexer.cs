using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    class Lexer
    {
        private readonly String textInput;
        private int currentIndex;
        private List<string> errorList;

        public Lexer(String text)
        {
            textInput = text;
            errorList = new List<string>();
        }

        public List<string> getErrorList()
        {
            return this.errorList;
        }

        private char getSymbol
        {
            get
            {
                if (currentIndex >= textInput.Length)
                {
                    return '\0';
                }
                return textInput[currentIndex];
            }
        }

        private void incrementIndex()
        {
            currentIndex++;
        }

        public AtomLexical returnAtomLexical()
        {
            if (currentIndex >= textInput.Length)
            {
                return new AtomLexical(atomType.TerminalAtom, currentIndex, "\0", null);
            }
            // 121 + 33 * 2

            if (char.IsDigit(getSymbol))
            {
                string appendNumber = "";
                while (char.IsDigit(getSymbol))
                {
                    appendNumber += getSymbol;
                    incrementIndex();
                }
                if (!int.TryParse(appendNumber, out int result))
                {
                    errorList.Add("Error! Numar mult prea mare! Nu se poate face conversia la int!");
                }
                return new AtomLexical(atomType.Numar, currentIndex, appendNumber, result);
            }

            if (char.IsWhiteSpace(getSymbol))
            {
                string append = "";
                while (char.IsWhiteSpace(getSymbol))
                {
                    append += getSymbol;
                    incrementIndex();
                }
                return new AtomLexical(atomType.Spatiu, currentIndex, append, null);
            }

            if (getSymbol == '+')
            {
                return new AtomLexical(atomType.Plus, currentIndex++, "+", null);
            }

            if (getSymbol == '-')
            {
                return new AtomLexical(atomType.Minus, currentIndex++, "-", null);
            }

            if (getSymbol == '*')
            {
                return new AtomLexical(atomType.Inmultire, currentIndex++, "*", null);
            }

            if (getSymbol == '/')
            {
                return new AtomLexical(atomType.Impartire, currentIndex++, "/", null);
            }

            if (getSymbol == '(')
            {
                return new AtomLexical(atomType.ParantezaDeschisa, currentIndex++, "(", null);
            }

            if (getSymbol == ')')
            {
                return new AtomLexical(atomType.ParantezaInchisa, currentIndex++, ")", null);
            }

            errorList.Add("Error! Invalid atom lexical: " + getSymbol);

            return new AtomLexical(atomType.InvalidAtom, currentIndex++, textInput[currentIndex-1].ToString(), null);
        }
    }
}
