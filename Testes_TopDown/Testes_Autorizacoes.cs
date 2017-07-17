using System;
using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;
using NUnit.Framework;
using System.Collections.Generic;

namespace Testes_TopDown
{
    [TestFixture]
    public class Testes_Autorizacoes : TestBase
    {

        //[Test]
        //[Description("Teste de Ir para a Tela Incluir Pedido Autorizacao")]
        //[Property("Nome", "Teste de Login")]
        //[Property("Módulo", "Autenticação")]
        //[Property("Função", "Login")]
        //[Property("Pré Condição", "")]
        //[Property("Pós Condição", "Usuário Garante Acesso Ao Sistema")]
        //[Property("Ambiente", "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med")]
        //[Property("Versão", "11")]
        //[Property("SAC", "N/A")]
        //[Property("Responsável", "PETERSON ANDRADE")]
        //public void EntrarNaTelaIncluirPedidoAutorizacao()
        //{
        //    Assert.IsTrue(Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.IrPara("HOMO_MED", "admin", "topdown"));
        //}

        [Test]
        [Description("O Teste deve conseguir Incluir um Pedido de Autorização")]
        [Property("Nome", "autorizacao-pedido-inclusao")]
        [Property("Módulo", "Autorização")]
        [Property("Função", "Pedidos de Autorização > Inclusão")]
        [Property("Pré Condição", "Usuário Está Autenticado no Sistema")]
        [Property("Pós Condição", "Pedido de Autorização do Tipo Consulta Eletiva incluído")]
        [Property("Ambiente", "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med")]
        [Property("Versão", "11")]
        [Property("SAC", "N/A")]
        [Property("Responsável", "PETERSON ANDRADE")]
        public static void IncluirPedidoDeConsultaEletivaParaBeneficiario()
        {
            //#region Cabeçalho CTF
            //CTF.Iniciar(TestContext.CurrentContext.Test.Properties.Get("Nome").ToString());
            //CTF.InformacoesIniciais(
            //    TestContext.CurrentContext.Test.Properties.Get("Módulo").ToString(),
            //    TestContext.CurrentContext.Test.Properties.Get("Função").ToString(),
            //    TestContext.CurrentContext.Test.Properties.Get("Pré Condição").ToString(),
            //    TestContext.CurrentContext.Test.Properties.Get("Pós Condição").ToString(),
            //    TestContext.CurrentContext.Test.Properties.Get("Ambiente").ToString(),
            //    TestContext.CurrentContext.Test.Properties.Get("Versão").ToString(),
            //    TestContext.CurrentContext.Test.Properties.Get("SAC").ToString(),
            //    TestContext.CurrentContext.Test.Properties.Get("Responsável").ToString(),
            //    DateTime.Today.ToString(@"DD/MM/YYYY"));
            //
            //#endregion


            #region Cabeçalho CTF
            CTF.Iniciar("teste-autorizacoes");
            CTF.InformacoesIniciais(
                "autorizacoes",
                "autorizacoes",
                "",
                "usuário logado",
                "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med",
                "11",
                "N/A",
                "Peterson Andrade",
                DateTime.Today.ToString(@"DD/MM/YYYY"));
            //
            #endregion


            try
            {
                //Lista de Beneficiários Ativos
                string[] beneficiariosAtivos = { "002010625850007", "002010625853006", "004510625887000", "002010625848002", "002010625808000"  };
                //
                Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.IrPara("HOMO_MED", "admin", "topdown");
                Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.informarBeneficiario(Utils.recuperarAletorio(beneficiariosAtivos));
                Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.informarTipoConsulta("CONSULTA ELETIVA");
                Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.infomarDataSolicitacaoConsulta("30/11/2016");
                CTF.inserirImagem(PrintUtils.takeSS());
                Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.clicarNaToolbar("AN");
                ////Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.moverBarraLateralParaInicio();
                Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.aguardarInclusao();
                Assert.IsTrue(Tela_Autorizacoes_PedidosDeAutorizacao_Inclusao.verificarSucessoInclusao());
                CTF.inserirImagem(PrintUtils.takeSS());
                CTF.registrarSucesso();
            }
            catch (Exception ex)
            {
                CTF.registrarErro();
                throw ex;
            }
        }

    

        

    }
}
