using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    public enum Type
    {
        INT,
        DOUBLE,
        FLOAT,
        STRING
    }
    public class AndroVar
    {
        public Type _type { get; set; }
        public string _identifier { get; set; }
        public string _val { get; set; }

        public AndroVar(Type t, string identifier, string val)
        {
            this._type = t;
            this._identifier = identifier;
            this._val = val;
        }

        public AndroVar() { }
    }
}
