using NUnit.Framework;
using TopDown_QA_FrameWork.Geradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes_TopDown
{
    public class Testes_Geradores
    {
        //[Test]
        //[Description("Testa Gerador de Nome de Uma Pessoa")]
        public static void TesteGeraNomePessoa()
        {
            Logger.escrever(Utils.GerarNomePessoa());
        }
    }
}
