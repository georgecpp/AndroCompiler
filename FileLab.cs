using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AndroCompiler
{
    public class FileLab
    {
        private string _filename;

        public FileLab(string filename)
        {
            this._filename = filename;
        }

        public List<AndroVar> HandleLineOfCode()
        {
            List<AndroVar> listVars = new List<AndroVar>();
            string pattern = @"^\b(int|float|double|string)\b";
            foreach(string line in System.IO.File.ReadLines(_filename))
            {
                string linecpy = line.Trim(new char[] { ';' });
                Match match = Regex.Match(linecpy, pattern);
                if(match.Success)
                {
                    ExtractAndroVars(listVars, linecpy, match);
                }
                if(!match.Success && linecpy.Contains('='))
                {
                    AssignAndroVars(listVars, linecpy);
                }
                if(!match.Success && !linecpy.Contains('=') && !string.IsNullOrWhiteSpace(linecpy))
                {
                    EvaluateExpressions(listVars, linecpy);
                }
            }
            return listVars;
        }

        public void ExtractAndroVars(List<AndroVar> listVars, string line, Match match)
        {
            var tokensComma = tokenizeCommas(line);
            if (tokensComma.Length == 1)
            {
                var tokensEqual = tokenizeEquals(line);
                string ident = tokensComma[0].Split(' ')[1];
                if (ident.Contains('='))
                {
                    ident = tokensComma[0].Split(' ')[1].Split('=')[0];
                }
                if (tokensEqual.Length == 1)
                {

                    listVars.Add(new AndroVar
                    {
                        _type = getType(match.Value),
                        _identifier = ident
                    });
                }
                else
                {
                    listVars.Add(new AndroVar
                    {
                        _type = getType(match.Value),
                        _identifier = ident,
                        _val = tokensEqual[1].Trim()
                    });
                }
            }
            else
            {
                foreach (var tokenComma in tokensComma)
                {
                    var tokensEqual = tokenizeEquals(tokenComma);
                    if (tokensEqual.Length == 1)
                    {
                        listVars.Add(new AndroVar
                        {
                            _type = getType(match.Value),
                            _identifier = tokenComma.Trim()
                        });
                    }
                    else
                    {
                        string ident;
                        if (tokenComma.Split(' ').Length == 1)
                        {
                            ident = tokenComma;
                        }
                        else
                        {
                            ident = tokenComma.Split(' ')[1];
                        }
                        if (ident.Contains('='))
                        {
                            ident = ident.Split('=')[0];
                        }
                        listVars.Add(new AndroVar
                        {
                            _type = getType(match.Value),
                            _identifier = ident,
                            _val = tokensEqual[1].Trim()
                        });
                    }
                }
            }
        }

        public void AssignAndroVars(List<AndroVar> listVars, string line)
        {
            string var_name = line.Split('=')[0].Trim();
            string var_value = line.Split('=')[1].Trim();
            foreach(var x in listVars)
            {
                if(x._identifier.Equals(var_name))
                {
                    x._val = var_value;
                }
            }
        }

        public void EvaluateExpressions(List<AndroVar> listVars, string line)
        {

            string finalExpression = ReplaceValuesInExpression(line, listVars);
            ParserClass parser = new ParserClass(finalExpression);
            var arbore = parser.parseExpression();
            var evaluare = new EvaluateExpression(arbore.Root);
            if (parser.getErrorList.Count() > 0)
            {
                foreach (var x in parser.getErrorList)
                {
                    Console.WriteLine(x);
                }
            }
            else
            {
                AfiseazaArbore(arbore.Root);
                Console.WriteLine("\nResult = " + evaluare.getEvaluate+"\n\n");
            }
        }

        public string ReplaceValuesInExpression(string line, List<AndroVar> listVars)
        {
            foreach(var x in listVars)
            {
                if(line.Contains(x._identifier))
                {
                    line = line.Replace(x._identifier, x._val);
                }
            }
            return line;
        }

        static void AfiseazaArbore(Node nod, string indentare = "", bool ultimulNod = true)
        {
            var aux = ultimulNod ? "└──" : "├──";
            Console.Write(indentare);
            Console.Write(aux);
            Console.Write(nod.tip);

            if (nod is AtomLexical t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indentare += ultimulNod ? "    " : "|   ";

            var lastChild = nod.getChildrens().LastOrDefault();

            foreach (var copil in nod.getChildrens())
            {
                AfiseazaArbore(copil, indentare, copil == lastChild);
            }
        }

        private string[] tokenizeCommas(string line)
        {
            return line.Split(',');
        }

        private string[] tokenizeEquals(string line)
        {
            return line.Split('=');
        }

        public Type getType(string type)
        {
            if (type == "int")
            {
                return Type.INT;
            }
            else if (type == "float")
            {
                return Type.FLOAT;
            }
            else if (type == "double")
            {
                return Type.DOUBLE;
            }
            else if (type == "string")
            {
                return Type.STRING;
            }

            return Type.STRING;
        }
    }
}
