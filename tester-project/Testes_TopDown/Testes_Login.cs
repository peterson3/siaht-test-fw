using System;
using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;
using NUnit.Framework;
using TopDown_QA_FrameWork.Paginas;

namespace Testes_TopDown
{
    [TestFixture]
    public class Testes_Login : TestBase
    {
        
        //Propriedades do Teste
        [Test]
        [Description("O Teste deve Conseguir Logar no Sistema")]
        [Property("Nome", "Teste de Login")]
        [Property("Módulo", "Autenticação")]
        [Property("Função", "Login")]
        [Property("Pré Condição", "")]
        [Property("Pós Condição", "Usuário Garante Acesso Ao Sistema")]
        [Property("Ambiente", "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med")]
        [Property("Versão", "11")]
        [Property("SAC", "N/A")]
        [Property("Responsável", "PETERSON ANDRADE")]
        public static void ConsegueLogar()
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
            ////
            //#endregion

            #region Cabeçalho CTF
            CTF.Iniciar("teste-login");
            CTF.InformacoesIniciais(
                "login",
                "login",
                "",
                "usuário logado",
                "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med",
                "11",
                "N/A",
                "Peterson Andrade",
                DateTime.Today.ToString(@"DD/MM/YYYY"));
            //
            #endregion

            #region Passos do Caso de Teste
            //try
            //{
                Tela_Login.IrPara();
                Tela_Login.InformarBase("HOMO_MED");
                Tela_Login.InformarUsuario("admin");
                Tela_Login.InformarSenha("topdown");
                CTF.inserirImagem(Browser.Print());
                Tela_Login.ClicarEmLogar();
                Assert.IsTrue(Tela_Principal.EstaEm());
                CTF.inserirImagem(Browser.Print());
                CTF.registrarSucesso();
            //}
            //catch (Exception ex)
            //{
            //    CTF.registrarErro();
            //    throw ex;
            //}

            CTF.Finalizar();
            #endregion
        }


        //[Ignore("Teste Ignorado")]
        //[Test]
        //[Description("Propositadamente errar a senha de Login, o teste não deve conseguir entrar no sistema")]
        //public void NaoConsegueLogar()
        //{
        //    //Errando a Senha
        //    Tela_Login.IrPara();
        //    Tela_Login.InformarBase("HOMO_MED");
        //    Tela_Login.InformarUsuario("admin");
        //    Tela_Login.InformarSenha("aaaaaa");
        //    Tela_Login.ClicarEmLogar();
        //    Assert.IsFalse(Tela_Principal.EstaEm());
        //    //Assert.IsFalse(Tela_Login.LogIn("HOMO_MED", "admin", "aaaaaa"));
        //}

       
    }
}
