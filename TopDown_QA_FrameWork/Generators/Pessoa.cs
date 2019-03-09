using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDown_QA_FrameWork.Geradores
{
    public static class Pessoa
    {
        public static string Nome { get; set; }
        public static string Sexo { get; set; }
        public static string CodCidade { get; set; } //pelo Sistema varia de 1 a 9813
        public static string DataNascimento { get; set; }
        public static string CPF { get; set; }
        public static string CEP { get; set; }

        public static void gerar()
        {
            Logger.escrever("[UTILITÁRIOS] Gerando Nome Pessoa");
            Driver.openAuxDriver("http://pt.fakenamegenerator.com/gen-random-br-br.php");

            System.Threading.Thread.Sleep(3000);


            //Localizar imagem para saber sexo
            By procura_Img = By.XPath("html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[1]/div/div[1]/img");
            Driver.auxWait.Until(ExpectedConditions.ElementIsVisible(procura_Img));
            IWebElement img = Driver.auxWebDriver.FindElement(procura_Img);

            Pessoa.Sexo= img.GetAttribute("alt");


            //Localizar Texto para saber Nome
            By procura_Nome = By.XPath("html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[2]/div/div[1]/h3");
            Driver.auxWait.Until(ExpectedConditions.ElementIsVisible(procura_Nome));
            IWebElement nomeElement = Driver.auxWebDriver.FindElement(procura_Nome);


            Pessoa.Nome = nomeElement.Text;

            

            Pessoa.CodCidade = Utils.GerarNumeroAleatorio(1, 9813);

            //Data de Nascimento
            //Lei de formação
            string mes = Utils.GerarNumeroAleatorio (1,12);
            string ano = Utils.GerarNumeroAleatorio (1940,1996);
            string dia = Utils.GerarNumeroAleatorio(1, DateTime.DaysInMonth(Convert.ToInt32(ano), Convert.ToInt32(mes)));
            if (Convert.ToInt32(mes) < 10)
            {
                mes = "0" + mes;
            }
            if (Convert.ToInt32(dia) < 10)
            {
                dia = "0" + dia;
            }
            Pessoa.DataNascimento = dia + "/" + mes + "/" + ano;

            //By procura_CPF = By.XPath("html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[2]/div/div[2]/dl[2]/dd");
            //Browser.auxWait.Until(ExpectedConditions.ElementIsVisible(procura_CPF));
            //IWebElement cpfElement = Browser.auxWebDriver.FindElement(procura_CPF);
            //Pessoa.CPF = cpfElement.Text;
            Pessoa.CPF = Utils.GerarCpf();



            Driver.closeAuxDriver();


            Pessoa.CEP = Utils.geraCep();
            Logger.escrever("[UTILITÁRIOS] Nome Pessao Gerada: " + Pessoa.Nome);
            Logger.escrever("[UTILITÁRIOS] Sexo Pessao Gerada: " + Pessoa.Sexo);
            Logger.escrever("[UTILITÁRIOS] Data de Nascimento Pessao Gerada: " + Pessoa.DataNascimento);
            Logger.escrever("[UTILITÁRIOS] CPF Pessao Gerada: " + Pessoa.CPF);
            Logger.escrever("[UTILITÁRIOS] CEP Pessao Gerada: " + Pessoa.CEP);


           
        }

    }   

}
