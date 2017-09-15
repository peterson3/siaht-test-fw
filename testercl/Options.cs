using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testercl
{
    class Options
    {
        // Omitting long name, default --verbose
        [Option('v', "verbose", Required = false, DefaultValue = true,
          HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }


        [Option('c' ,"ctf", Required=false,
            HelpText = "Espera como parametro casos de teste a serem executados")]
        public IList<string> CasosDeTesteToExec { get; set; }

    }
}
