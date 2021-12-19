using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    sealed class ExpresieParanteze : Expresie
    {
        public AtomLexical Paranteza_Stanga { get; }
        public Expresie Expresie { get; }
        public AtomLexical Paranteza_Dreapta { get; }

        public ExpresieParanteze(AtomLexical paranteza_stanga, Expresie expresie, AtomLexical paranteza_dreapta) 
        {
            Paranteza_Stanga = paranteza_stanga;
            Expresie = expresie;
            Paranteza_Dreapta = paranteza_dreapta;
        }

        public override atomType tip => atomType.ExpresieParanteze;

        public override IEnumerable<Node> getChildrens()
        {
            yield return Paranteza_Stanga;
            yield return Expresie;
            yield return Paranteza_Dreapta;
        }
    }
}