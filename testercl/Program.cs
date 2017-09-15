using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testercl
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            var isValid = CommandLine.Parser.Default.ParseArgumentsStrict(args, options);


           if (isValid) 
           {

               Console.WriteLine("*******************************");
               Console.WriteLine("****Auto-Tester Console App****");
               Console.WriteLine("*******************************");

               Console.Write ("CTF(s) count: ", options.CasosDeTesteToExec.Count.ToString());  
           }

           else
           {
               Console.WriteLine("Argumentos Inválidos");

           }

           
        }

    }
}
