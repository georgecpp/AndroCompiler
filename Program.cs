using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            FileLab fileLab = new FileLab(args[0]);
            var variables = fileLab.HandleLineOfCode();
        }
    }
}
