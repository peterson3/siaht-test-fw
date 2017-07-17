using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;
using NUnit.Framework;
using TopDown_QA_FrameWork.Paginas.Gestao_Prestador.Cadastro_Prestador.Inclusao;
using TopDown_QA_FrameWork.Paginas.Gestao_Prestador.Cadastro_Prestador.Alteracao;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace Testes_TopDown
{
    [TestFixture]
    public class Testes_Prestador : TestBase
    {

        //[Test]
        //[Order(1)]
        //[Description("Teste de Ir para a Tela Incluir Prestador")]
        //public void EntrarNaTelaIncluirPrestador()
        //{
        //    Assert.IsTrue(InclusaoPrestador.IrPara("HOMO_MED", "admin", "topdown"));
        //    // Assert.IsTrue(InclusaoPrestador.EstaEm());
        //}

        //[Test]
        //[Order(1)]
        //[Description("Teste de Inserção de Novo Prestador do tipo pessoa física")]
        //[Property("Nome", "Incluir Novo Prestador")]
        //[Property("Módulo", "Gestão Prestador")]
        //[Property("Função", "Cadastro Prestador > Inclusão")]
        //[Property("Pré Condição", "Usuário com Acesso Ao Sistema")]
        //[Property("Pós Condição", "Novo Prestador Adicionado")]
        //[Property("Ambiente", "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med")]
        //[Property("Versão", "11")]
        //[Property("SAC", "N/A")]
        //[Property("Responsável", "PETERSON ANDRADE")]
        //public void InserirNovoPrestador()
        //{
        //    #region Cabeçalho CTF
        //    CTF.Iniciar(TestContext.CurrentContext.Test.Properties.Get("Nome").ToString());
        //    CTF.InformacoesIniciais(
        //        TestContext.CurrentContext.Test.Properties.Get("Módulo").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Função").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Pré Condição").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Pós Condição").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Ambiente").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Versão").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("SAC").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Responsável").ToString(),
        //        DateTime.Today.ToString(@"DD/MM/YYYY"));
        //    //
        //    #endregion

        //    //Dados Cadastrais
        //    InclusaoPrestador.IrPara("HOMO_MED", "admin", "topdown");
        //    InclusaoPrestador.informarVinculacao("Credenciado");
        //    InclusaoPrestador.informarTipoPessoa(InclusaoPrestador.PessoaFisica);
        //    InclusaoPrestador.Clicar_IncluirPessoaFisica();
        //    InclusaoPrestador.InformarNome("Daniel Amarantos (TESTE_AUTO 10)");
        //    InclusaoPrestador.InformarNaturalidade("6889"); //Código de Casemiro de Abreu
        //    InclusaoPrestador.InformarSexo(InclusaoPrestador.sexoMasculino);
        //    InclusaoPrestador.InformarNascimento("10/05/1982");
        //    InclusaoPrestador.InformarCPF(Utils.GerarCpf());

        //    // Endereços
        //    InclusaoPrestador.AcessarMenu("Endereços");
        //    InclusaoPrestador.InformarCEP(Utils.geraCep());
        //    InclusaoPrestador.InformarNumeroEndereco("15454");
        //    //Dados Cadastrais
        //    //InclusaoPrestador.AcessarMenu("Dados Cadastrais");
        //    //InclusaoPrestador.InformarSiglaConselho("CRM");
        //    //InclusaoPrestador.InformarNumeroConselho("99999999");
        //    //InclusaoPrestador.InformarUFConselho("RJ-Rio de Janeiro");
        //    ////Informações Complementares
        //    //InclusaoPrestador.AcessarMenu("Informações Complementares");
        //    //InclusaoPrestador.InformarOperadora("1 - SEPACO AutoGestão");
        //    //InclusaoPrestador.InformarTipoEstabelecimento_ANS("Assistência hospitalar");
        //    //InclusaoPrestador.InformarDisponibilidadeServico_ANS("Parcial");
        //    //InclusaoPrestador.InformarTipoContratualizacao_ANS("Direta");
        //    //InclusaoPrestador.InformarDataCredenciamento_ANS("10/05/1982");
        //    //InclusaoPrestador.InformarDataContratualizacao_ANS("07/12/2016");

        //    ////Dados de pagamento
        //    //InclusaoPrestador.AcessarMenu("Dados Pagamento");
        //    //InclusaoPrestador.InformarDataVigenciaDe("01/01/2016");
        //    //InclusaoPrestador.InformarDataVigenciaAte("01/01/2017");
        //    //InclusaoPrestador.InformarFormaPagamentoDasContas("Dinheiro / Cheque");
        //    //InclusaoPrestador.SelecionarEmiteNotaFiscal("Sim");
        //    //InclusaoPrestador.SelecionarMomentoApresentacao("Na entrega da remessa");

        //    //Toolbar
        //    InclusaoPrestador.Clicar_icone_incluir(); //Tem que gerar um número

        //    //Rede Atendimento
        //    //InclusaoPrestador.AcessarMenu("Rede Atendimento");
        //    //InclusaoPrestador.InformarOperadora("1- SEPACO AutoGestão");
        //    //InclusaoPrestador.SelecionarRedeAtendimento("TODOS");
        //    //Tipo Estabelecimento
        //    //InclusaoPrestador.AcessarMenu("Tipo Estabelecimento");
        //    //InclusaoPrestador.InformarOperadora("1- SEPACO AutoGestão");
        //    //InclusaoPrestador.SelecionarTipoEstabelecimento("TODOS");
        //    ////Informações Complementares 
        //    //InclusaoPrestador.AcessarMenu("Informações Complementares");
        //    //InclusaoPrestador.InformarOperadora("2- SEPACO Saúde");
        //    //InclusaoPrestador.InformarTipoEstabelecimento_ANS("Assistência hospitalar");
        //    //InclusaoPrestador.InformarDisponibilidadeServico_ANS("Parcial");
        //    //InclusaoPrestador.InformarTipoContratualizacao_ANS("Direta");
        //    //InclusaoPrestador.InformarDataCredenciamento_ANS("10/05/1982");
        //    //InclusaoPrestador.InformarDataContratualizacao_ANS("05/12/2016");
        //    ////Rede Atendimento
        //    //InclusaoPrestador.AcessarMenu("Rede Atendimento");
        //    //InclusaoPrestador.InformarOperadora("2- SEPACO Saúde");
        //    //InclusaoPrestador.SelecionarRedeAtendimento("TODOS");
        //    ////Tipo Estabelecimento
        //    //InclusaoPrestador.AcessarMenu("Tipo Estabelecimento");
        //    //InclusaoPrestador.InformarOperadora("1- SEPACO Saúde");
        //    //InclusaoPrestador.SelecionarTipoEstabelecimento("TODOS");
        //    ////Barra de Ferramentas
        //    //InclusaoPrestador.Clicar_icone_incluir();

        //    //Verifica Sucesso da Inclusão
        //    Assert.IsTrue(InclusaoPrestador.verificarSucessoInclusao());
        //}


        //[Test]
        //[Order(2)]
        //[Description("Habilita Rede de Atendimentoe Tipo Estabelecimento Para Prestador tipo pessoa física")]
        //public void HabilitaPrestador()
        //{
        //    AlteracaoPrestador.IrPara("HOMO_MED", "admin", "topdown");
        //    AlteracaoPrestador.InformarPrestador("0002826");

        //    //Alterar Tipo Estabelecimento
        //    //Selecionado
        //    InclusaoPrestador.AcessarMenu("Tipo Estabelecimento");
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(0, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(1, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(2, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(3, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(4, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(5, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(6, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(7, 0);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(8, 0);
        //    //Principal?
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(0, 3);
        //    //Divulgação?
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(0, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(1, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(2, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(3, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(4, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(5, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(6, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(7, 4);
        //    AlteracaoPrestador.SelecionarTipoEstabelecimento(8, 4);
        //    //Validar Alterações

        //    //Salvar Alterações
        //    AlteracaoPrestador.Clicar_Botao_Alterar();
        //}


        [Test]
        [Order(1)]
        [Description("Habilita Rede de Atendimento Tipo Estabelecimento Para Prestador tipo pessoa física")]
        [Property("Nome", "Incluir e Habilitar Novo Prestador")]
        [Property("Módulo", "Gestão Prestador")]
        [Property("Função", "Cadastro Prestador > Inclusão / Alteração")]
        [Property("Pré Condição", "Usuário com Acesso Ao Sistema")]
        [Property("Pós Condição", "Novo Prestador Adicionado e Habilitado")]
        [Property("Ambiente", "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med")]
        [Property("Versão", "11")]
        [Property("SAC", "N/A")]
        [Property("Responsável", "PETERSON ANDRADE")]
        public void InsereEHabilitaNovoPrestador()
        {
            #region Cabeçalho CTF
            CTF.Iniciar(TestContext.CurrentContext.Test.Properties.Get("Nome").ToString());
            CTF.InformacoesIniciais(
                TestContext.CurrentContext.Test.Properties.Get("Módulo").ToString(),
                TestContext.CurrentContext.Test.Properties.Get("Função").ToString(),
                TestContext.CurrentContext.Test.Properties.Get("Pré Condição").ToString(),
                TestContext.CurrentContext.Test.Properties.Get("Pós Condição").ToString(),
                TestContext.CurrentContext.Test.Properties.Get("Ambiente").ToString(),
                TestContext.CurrentContext.Test.Properties.Get("Versão").ToString(),
                TestContext.CurrentContext.Test.Properties.Get("SAC").ToString(),
                TestContext.CurrentContext.Test.Properties.Get("Responsável").ToString(),
                DateTime.Today.ToString(@"DD/MM/YYYY"));
            //
            #endregion
            try
            {
                //Insere e Habilita Novo Prestador

                //Dados Cadastrais
                InclusaoPrestador.IrPara("HOMO_MED", "admin", "topdown");
                InclusaoPrestador.informarVinculacao("Credenciado");
                InclusaoPrestador.informarTipoPessoa(InclusaoPrestador.PessoaFisica);
                InclusaoPrestador.Clicar_IncluirPessoaFisica();

                //Gerando Dados Para serem Inseridos
                Pessoa.gerar();

                //Dados Cadastrais
                InclusaoPrestador.InformarNome(Pessoa.Nome);
                InclusaoPrestador.InformarNaturalidade(Pessoa.CodCidade);
                InclusaoPrestador.InformarSexo(Pessoa.Sexo);
                InclusaoPrestador.InformarNascimento(Pessoa.DataNascimento);
                InclusaoPrestador.InformarCPF(Pessoa.CPF);
                CTF.inserirImagem(Browser.Print());

                // Endereços
                InclusaoPrestador.AcessarMenu("Endereços");
                InclusaoPrestador.InformarCEP(Pessoa.CEP);
                InclusaoPrestador.InformarNumeroEndereco(Utils.GerarNumeroAleatorio(1, 1000));
                CTF.inserirImagem(Browser.Print());

                //Toolbar
                InclusaoPrestador.Clicar_icone_incluir();
                InclusaoPrestador.verificarSucessoInclusao();

                string NumeroPrestadorIncluido = InclusaoPrestador.recuperarNumeroPrestadorIncluido();
                AlteracaoPrestador.IrPara("HOMO_MED", "admin", "topdown");
                AlteracaoPrestador.InformarPrestador(NumeroPrestadorIncluido);

                //Dados Cadastrais
                InclusaoPrestador.AcessarMenu("Dados Cadastrais");
                InclusaoPrestador.InformarSiglaConselho("CRM");
                InclusaoPrestador.InformarNumeroConselho(Utils.GerarNumeroAleatorio(1000000000, 9999999999));
                InclusaoPrestador.InformarUFConselho("RJ-Rio de Janeiro");
                CTF.inserirImagem(Browser.Print());

                //Finalizar Alteração
                AlteracaoPrestador.Clicar_Botao_Alterar();


                AlteracaoPrestador.IrPara("HOMO_MED", "admin", "topdown");
                AlteracaoPrestador.InformarPrestador(NumeroPrestadorIncluido);

                //Informações Complementares
                InclusaoPrestador.AcessarMenu("Informações Complementares");
                InclusaoPrestador.InformarOperadora("1 - SEPACO AutoGestão");
                InclusaoPrestador.InformarTipoEstabelecimento_ANS("Assistência hospitalar");
                InclusaoPrestador.InformarDisponibilidadeServico_ANS("Parcial");
                InclusaoPrestador.InformarTipoContratualizacao_ANS("Direta");
                CTF.inserirImagem(Browser.Print());

                //Todo: Consertar Erros Nessa Parte de Informar Data, pq quando salva informando a data ele perde a informação do resto
                //InclusaoPrestador.InformarDataCredenciamento_ANS("10/05/1995");
                //InclusaoPrestador.InformarDataContratualizacao_ANS(Utils.dataHoje());



                //Alterar Tipo Estabelecimento
                //Selecionado
                InclusaoPrestador.AcessarMenu("Tipo Estabelecimento");
                AlteracaoPrestador.SelecionarTipoEstabelecimento(0, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(1, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(2, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(3, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(4, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(5, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(6, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(7, 0);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(8, 0);
                //Principal?
                AlteracaoPrestador.SelecionarTipoEstabelecimento(0, 3);
                //Divulgação?
                AlteracaoPrestador.SelecionarTipoEstabelecimento(0, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(1, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(2, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(3, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(4, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(5, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(6, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(7, 4);
                AlteracaoPrestador.SelecionarTipoEstabelecimento(8, 4);
                CTF.inserirImagem(Browser.Print());

                //Validar Alterações
                AlteracaoPrestador.Clicar_Botao_Alterar();


                ////Dados de pagamento
                InclusaoPrestador.AcessarMenu("Dados Pagamento");
                InclusaoPrestador.InformarDataVigenciaDe("01/01/2016");
                InclusaoPrestador.InformarDataVigenciaAte("01/01/2017");
                InclusaoPrestador.InformarFormaPagamentoDasContas("Dinheiro / Cheque");
                InclusaoPrestador.SelecionarEmiteNotaFiscal("Sim");
                InclusaoPrestador.SelecionarMomentoApresentacao("Na entrega da remessa");
                CTF.inserirImagem(Browser.Print());


                ////Salvar Alterações
                AlteracaoPrestador.Clicar_Botao_Alterar();


                InclusaoPrestador.AcessarMenu("Rede Atendimento");
                AlteracaoPrestador.Clicar_Botao_AdicionarRedeAtendimento();
                AlteracaoPrestador.PesquisarRedeAtendimento();

                AlteracaoPrestador.RedeAtendimentoSelect(0, 0);
                AlteracaoPrestador.RedeAtendimentoSelect(1, 0);
                AlteracaoPrestador.RedeAtendimentoSelect(2, 0);
                AlteracaoPrestador.RedeAtendimentoSelect(3, 0);
                AlteracaoPrestador.RedeAtendimentoSelect(4, 0);

                CTF.inserirImagem(Browser.Print());

                AlteracaoPrestador.Clicar_Botao_Alterar();
                
                //Dados Cadastrais
                InclusaoPrestador.AcessarMenu("Informações Complementares");
                InclusaoPrestador.InformarOperadora("1 - SEPACO AutoGestão");
                InclusaoPrestador.InformarDataCredenciamento_ANS(Utils.dataHoje());
                InclusaoPrestador.InformarDataContratualizacao_ANS(Utils.dataHoje());
                CTF.inserirImagem(Browser.Print());

                AlteracaoPrestador.Clicar_Botao_Alterar();


                //Informar Endereços Atendimento
                InclusaoPrestador.AcessarMenu("Endereços Atendimento");
                AlteracaoPrestador.Clicar_Botao_AdicionarEnderecoAtendimento();
                //Informar CEP
                AlteracaoPrestador.InformarCepAtendimento(Pessoa.CEP);
                //Selecionar Tipo Logradouro (Dropdown)
                //Informar Logradouro Nome
                //Informar Numero
                AlteracaoPrestador.InformarNumeroEnderecoAtendimento(Utils.GerarNumeroAleatorio(1, 1000));
                //Informar Numero Municipio
                CTF.inserirImagem(Browser.Print());

                
                AlteracaoPrestador.Clicar_Botao_Alterar();

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
