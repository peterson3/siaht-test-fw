using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;
using TopDown_QA_FrameWork.Paginas.Contratos_e_Beneficiarios.Movimentacao_Operadora;

namespace Testes_TopDown
{
    public class Testes_Contratos_e_Beneficiarios : TestBase
    {
        [Test]
        [Description("O teste deve conseguir incluir um Beneficiário (titular)")]
        [Property("Nome", "inclusao-de-titular")]
        [Property("Módulo", "Contratos e Beneficiários")]
        [Property("Função", "Movimentação Operadora > Inclusão de Titular")]
        [Property("Pré Condição", "Usuário Está Autenticado no Sistema")]
        [Property("Pós Condição", "Beneificiário Incluído")]
        [Property("Ambiente", "Browser:IE10\tWeb:10.10.100.147\tBD:Homo_Med")]
        [Property("Versão", "11")]
        [Property("SAC", "N/A")]
        [Property("Responsável", "PETERSON ANDRADE")]
        public void IncluirNovoBeneficiarioTitular()
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
                string[] contratosAtivos = { "8138", "8156", "8116", "9315", "9427", "8101", "6994", "7493", "8175", "541624" };
                Tela_Inclusao_de_Titular.IrPara("HOMO_MED", "admin", "topdown");
                Tela_Inclusao_de_Titular.InformarContrato(Utils.recuperarAletorio(contratosAtivos));
                CTF.inserirImagem(PrintUtils.takeSS());
                Pessoa.gerar();
                //Dados Cadastrais
                Tela_Inclusao_de_Titular.InformarCPF(Pessoa.CPF);
                Tela_Inclusao_de_Titular.InformarNome(Pessoa.Nome);
                Tela_Inclusao_de_Titular.InformarNomeCartao(Pessoa.Nome);
                Tela_Inclusao_de_Titular.InformarDataInclusao(Utils.dataHoje());
                Tela_Inclusao_de_Titular.InformarSexo(Pessoa.Sexo);
                Tela_Inclusao_de_Titular.InformarDataNascimento(Pessoa.DataNascimento);
                Tela_Inclusao_de_Titular.InformarNacionalidade("Brasileira");
                Tela_Inclusao_de_Titular.InformarMae("MARIA DA SILVA");
                Tela_Inclusao_de_Titular.InformarPai("JOAO DA SILVA");
                Tela_Inclusao_de_Titular.InformarEstadoCivil("Solteiro");
                Tela_Inclusao_de_Titular.InformarPlano();
                Tela_Inclusao_de_Titular.InformarMunicipioResidencia("7043");
                CTF.inserirImagem(PrintUtils.takeSS());

                //Documentos
                Tela_Inclusao_de_Titular.InformarNumeroDocIdentidade("5454826");
                Tela_Inclusao_de_Titular.InformarDataEmissaoIdentidade("12/12/2000");
                Tela_Inclusao_de_Titular.InformarOrgaoDeEmissaoIdentidade("MINISTÉRIO DA MARINHA");
                Tela_Inclusao_de_Titular.InformarPIS_PASEP_NIT("");
                CTF.inserirImagem(PrintUtils.takeSS());

                //Informações Complementares
                //Dados de Reembolso
                //Endereço 1
                Tela_Inclusao_de_Titular.InformarTipoEndereco("Residência");
                Tela_Inclusao_de_Titular.InformarPaisEndereco("Brasil");
                Tela_Inclusao_de_Titular.InformarCEPEndereco("24422020");
                Tela_Inclusao_de_Titular.InformarNumeroEndereco("160");
                
                //Dados Empresa
                Tela_Inclusao_de_Titular.InformarMatriculaFuncional("65454854542");
                Tela_Inclusao_de_Titular.InformarDataAdmissao("27/11/2016");
                //Clicar em Incluir
                Tela_Inclusao_de_Titular.moverBarraLateralParaInicio();
                Tela_Inclusao_de_Titular.ClicarEmIncluir();
                //Tela_Inclusao_de_Titular.aguardarInclusao();
                Assert.IsTrue(Tela_Inclusao_de_Titular.verificarSucessoInclusao());
                CTF.inserirImagem(PrintUtils.takeSS());

                //Aguardar a Inclusao
                //Verificar Numero de Inclusao

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
