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
                //string appendNumber = "";
                //while (char.IsDigit(getSymbol))
                //{
                //    appendNumber += getSymbol;
                //    incrementIndex();
                //}
                //if (!int.TryParse(appendNumber, out int result))
                //{
                //    errorList.Add("Error! Numar mult prea mare! Nu se poate face conversia la int!");
                //}
                //return new AtomLexical(atomType.Numar, currentIndex, appendNumber, result);

                var start = this.currentIndex;
                int dot = 0;
                while(char.IsDigit(getSymbol) || getSymbol == '.')
                {
                    if(getSymbol == '.')
                    {
                        if(dot==0)
                        {
                            dot++;
                        }
                        else
                        {
                            errorList.Add($"Lexer: Acesta nu este un numar decimal valid!");
                            throw new Exception("Lexer: numar decimal inavlid");
                        }
                    }
                    incrementIndex();
                }

                var lungime = currentIndex - start;
                var input = textInput.Substring(start, lungime);

                if(dot == 1)
                {
                    if(decimal.TryParse(input, out var valoare) == false)
                    {
                        errorList.Add($"Lexer: Exceptie: Nu s-a putut realiza conversia la decimal '{textInput}'");
                        throw new Exception("Lexer: nu s-a putut realiza conversia - numar decimal invalid");
                    }

                    return new AtomLexical(atomType.nr_float, start, input, valoare);
                }
                else
                {
                    if (int.TryParse(input, out var valoare) == false)
                    {
                        errorList.Add($"Lexer: Exceptie: Nu s-a putut realiza conversia la int '{textInput}'");
                        throw new Exception("Lexer: nu s-a putut realiza conversia - numar intreg invalid");
                    }
                    return new AtomLexical(atomType.nr_intreg, start, input, valoare);
                }
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

            if(getSymbol == '"')
            {
                var start = currentIndex++;
                while(getSymbol != '"' && getSymbol != '\0')
                {
                    incrementIndex();
                }
                if(getSymbol == '\0')
                {
                    errorList.Add("Lexer: String-ul constant nu a fost inchis");
                    throw new Exception("Lexer: Ghilimelele deschise nu a fost niciodata inchise");
                }
                incrementIndex();
                var lungime = currentIndex - 1 - (start + 1);
                var input = textInput.Substring(start + 1, lungime);
                return new AtomLexical(atomType.text, start + 1, input, input);
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
