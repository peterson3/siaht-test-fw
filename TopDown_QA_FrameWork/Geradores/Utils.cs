using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotCEP;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace TopDown_QA_FrameWork.Geradores
{
    public static class Utils
    {
        public static String GerarCpf()
        {
            Logger.escrever("[UTILITÁRIOS] Gerando CPF");

            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

        //public static string geraCEP()
        //{
        //    Endereco end = new Endereco();
            
        //}

        public static string geraCep()
        {
            Logger.escrever("[UTILITÁRIOS] Gerando CEP");
            Browser.openAuxDriver("http://4devs.com.br/gerador_de_cep");

            
            
            //Localizar Botão Gera CEP
            By procura_btnGeraCEP = By.Id("btn_gerar_cep");
            Browser.auxWait.Until(ExpectedConditions.ElementToBeClickable((procura_btnGeraCEP)));
            IWebElement btnCEPElementInput = Browser.auxWebDriver.FindElement(procura_btnGeraCEP);

            //Clicar no Botão
            btnCEPElementInput.Click();


            System.Threading.Thread.Sleep(3000);


            //Localizar o campo do cep
            By procura_CEP_field = By.Id("cep");
            Browser.auxWait.Until(ExpectedConditions.ElementToBeClickable(procura_CEP_field));
            IWebElement cepField = Browser.auxWebDriver.FindElement(procura_CEP_field);

            string cep = cepField.GetAttribute("value");

            Browser.closeAuxDriver();

            return cep;
            // nomeElementInput.SendKeys(nome);
        }

        /// <summary>
        ///
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GerarNomePessoa()
        {
            Logger.escrever("[UTILITÁRIOS] Gerando Pessoa");
            Browser.openAuxDriver("http://pt.fakenamegenerator.com/gen-random-br-br.php");

            System.Threading.Thread.Sleep(3000);


            //Localizar imagem para saber sexo
            By procura_Img = By.XPath("html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[1]/div/div[1]/img");
            Browser.auxWait.Until(ExpectedConditions.ElementIsVisible(procura_Img));
            IWebElement img = Browser.auxWebDriver.FindElement(procura_Img);

            string sexo = img.GetAttribute("alt");


            //Localizar imagem para saber sexo
            By procura_Nome = By.XPath("html/body/div[2]/div/div/div[1]/div/div[3]/div[2]/div[2]/div/div[1]/h3");
            Browser.auxWait.Until(ExpectedConditions.ElementIsVisible(procura_Nome));
            IWebElement nomeElement = Browser.auxWebDriver.FindElement(procura_Nome);


            string nome = nomeElement.Text;

            Browser.closeAuxDriver();

            return nome;
            // nomeElementInput.SendKeys(nome);
        }

        /// <summary>
        /// Gerar um número aleatório entre os parametros init e fim (valores também inclusos)
        /// </summary>
        /// <param name="init"></param>
        /// <param name="fim"></param>
        /// <returns></returns>
        public static string GerarNumeroAleatorio(int init, int fim)
        {
            Random aleatorio = new Random();
            return aleatorio.Next(init, fim + 1).ToString() ;
        }

        public static string GerarNumeroAleatorio(long init, long fim)
        {
            Random rand = new Random();
            long result = rand.Next((Int32)(init >> 32), (Int32)(fim >> 32));
            result = (result << 32);
            result = result | (long)rand.Next((Int32)init, (Int32)fim);
            return result.ToString();
        }

        public static string dataHoje()
        {
            return String.Format("{0:dd/MM/yyyy}", DateTime.Today);
        }

        public static string recuperarAletorio(string[] vector)
        {
            Random r = new Random();
            return vector[r.Next(0, vector.Length)];
        }


        public static bool validaCNS(double valorCNS)
        {
            double soma;
            double resto;
            double dv;
            double pis;
            
            string resultado;
            int tamanhoCNS = valorCNS.ToString().Length;

            if (tamanhoCNS != 15)
            {
                //Número CNS inválido pelo tamanho
                return false;
            }

            pis = Convert.ToDouble(valorCNS.ToString().Substring(0, 11));
            soma =(
                (Convert.ToDouble(pis.ToString().Substring(0, 1)) * 15) +
                (Convert.ToDouble(pis.ToString().Substring(1, 2)) * 14) +
                (Convert.ToDouble(pis.ToString().Substring(2, 3)) * 13) +
                (Convert.ToDouble(pis.ToString().Substring(3, 4)) * 12) +
                (Convert.ToDouble(pis.ToString().Substring(4, 5)) * 11) +
                (Convert.ToDouble(pis.ToString().Substring(5, 6)) * 10) +
                (Convert.ToDouble(pis.ToString().Substring(6, 7)) * 9) +
                (Convert.ToDouble(pis.ToString().Substring(7, 8)) * 8) +
                (Convert.ToDouble(pis.ToString().Substring(8, 9)) * 7) +
                (Convert.ToDouble(pis.ToString().Substring(9, 10)) * 6) +
                (Convert.ToDouble(pis.ToString().Substring(10, 11)) * 5) 
                );


            resto = soma % 11;
            dv = 11 - resto;

            if (dv == 11)
            {
                dv = 0;
            }

            if (dv == 10)
            {
                soma = (
                (Convert.ToDouble(pis.ToString().Substring(0, 1)) * 15) +
                (Convert.ToDouble(pis.ToString().Substring(1, 2)) * 14) +
                (Convert.ToDouble(pis.ToString().Substring(2, 3)) * 13) +
                (Convert.ToDouble(pis.ToString().Substring(3, 4)) * 12) +
                (Convert.ToDouble(pis.ToString().Substring(4, 5)) * 11) +
                (Convert.ToDouble(pis.ToString().Substring(5, 6)) * 10) +
                (Convert.ToDouble(pis.ToString().Substring(6, 7)) * 9) +
                (Convert.ToDouble(pis.ToString().Substring(7, 8)) * 8) +
                (Convert.ToDouble(pis.ToString().Substring(8, 9)) * 7) +
                (Convert.ToDouble(pis.ToString().Substring(9, 10)) * 6) +
                (Convert.ToDouble(pis.ToString().Substring(10, 11)) * 5) + 2
                );

                resto = soma % 11;
                dv = 11 - resto;

                resultado = pis.ToString() + "001" + dv.ToString();

            }
            else
            {
                resultado = pis.ToString() + "000" + dv.ToString(); 
            }

            if (valorCNS.ToString() != resultado)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string geraCNS()
        {
            bool cnsInvalido = true;
            string CNS = "";

            while (cnsInvalido)
            {
                try
                {

                    Random r = new Random();
                    double temp = (r.NextDouble() * 3) + 1;
                    double gera0 = Math.Floor(temp);


                    if (gera0 == 3)
                    {
                        temp = (r.NextDouble() * 3) + 7;
                        gera0 = Math.Floor(temp);
                    }

                    //gera0 = Math.Abs(gera0);


                    temp = (r.NextDouble() * 99999) + 1;
                    double gera1 = (Math.Floor(temp));

                    temp = (r.NextDouble() * 99999) + 1;
                    double gera2 = (Math.Floor(temp));


                    CNS = gera0.ToString() + (("0" + gera1.ToString()).Substring(gera1.ToString().Length - 4)) + (("0" + gera2.ToString()).Substring(gera1.ToString().Length - 4));


                    long soma = (
                          (Convert.ToInt64(CNS.ToString().Substring(0, 1)) * 15) +
                          (Convert.ToInt64(CNS.ToString().Substring(1, 1)) * 14) +
                          (Convert.ToInt64(CNS.ToString().Substring(2, 1)) * 13) +
                          (Convert.ToInt64(CNS.ToString().Substring(3, 1)) * 12) +
                          (Convert.ToInt64(CNS.ToString().Substring(4, 1)) * 11) +
                          (Convert.ToInt64(CNS.ToString().Substring(5, 1)) * 10) +
                          (Convert.ToInt64(CNS.ToString().Substring(6, 1)) * 9) +
                          (Convert.ToInt64(CNS.ToString().Substring(7, 1)) * 8) +
                          (Convert.ToInt64(CNS.ToString().Substring(8, 1)) * 7) +
                          (Convert.ToInt64(CNS.ToString().Substring(9, 1)) * 6) +
                          (Convert.ToInt64(CNS.ToString().Substring(10, 1)) * 5)
                         );




                    long resto = Convert.ToInt64(soma % 11);
                    long dv = 11 - resto;


                    dv = (dv == 1) ? 0 : dv;

                    if (dv == 10)
                    {
                        soma = (
                             (Convert.ToInt64(CNS.ToString().Substring(0, 1)) * 15) +
                             (Convert.ToInt64(CNS.ToString().Substring(1, 1)) * 14) +
                             (Convert.ToInt64(CNS.ToString().Substring(2, 1)) * 13) +
                             (Convert.ToInt64(CNS.ToString().Substring(3, 1)) * 12) +
                             (Convert.ToInt64(CNS.ToString().Substring(4, 1)) * 11) +
                             (Convert.ToInt64(CNS.ToString().Substring(5, 1)) * 10) +
                             (Convert.ToInt64(CNS.ToString().Substring(6, 1)) * 9) +
                             (Convert.ToInt64(CNS.ToString().Substring(7, 1)) * 8) +
                             (Convert.ToInt64(CNS.ToString().Substring(8, 1)) * 7) +
                             (Convert.ToInt64(CNS.ToString().Substring(9, 1)) * 6) +
                             (Convert.ToInt64(CNS.ToString().Substring(10, 1)) * 5) + 2
                            );

                        resto = soma % 11;
                        dv = 11 - resto;
                        CNS += "001" + dv.ToString();
                    }
                    else
                    {
                        CNS += "000" + dv.ToString();
                    }

                    if (CNS.Length == 15)
                    {
                        cnsInvalido = false;
                    }
                    else
                    {
                        cnsInvalido = true;
                    }


                }
                catch (Exception ex)
                {


                }

            }
            return CNS;

        }


        /* Rotina de validação de Números que iniciam com “7”, “8” ou “9”


            function ValidaCNS_PROV(Obj)
            {
                var pis;
                var resto;
                var dv;
                var soma;
                var resultado;
                var result;
                result = 0;

	            pis = Obj.value.substring(0,15);

	            if (pis == "")
	               {
	                  return false
	               }
	    
	            if ( (Obj.value.substring(0,1) != "7")  && (Obj.value.substring(0,1) != "8") && (Obj.value.substring(0,1) != "9") )
	               {
                          alert("Atenção! Número Provisório inválido!");
                          return false
  	               }
 
 	            soma = (   (parseInt(pis.substring( 0, 1),10)) * 15)
			            + ((parseInt(pis.substring( 1, 2),10)) * 14)
			            + ((parseInt(pis.substring( 2, 3),10)) * 13)
			            + ((parseInt(pis.substring( 3, 4),10)) * 12)
			            + ((parseInt(pis.substring( 4, 5),10)) * 11)
			            + ((parseInt(pis.substring( 5, 6),10)) * 10)
			            + ((parseInt(pis.substring( 6, 7),10)) * 9)
			            + ((parseInt(pis.substring( 7, 8),10)) * 8)
			            + ((parseInt(pis.substring( 8, 9),10)) * 7)
			            + ((parseInt(pis.substring( 9,10),10)) * 6)
			            + ((parseInt(pis.substring(10,11),10)) * 5)
			            + ((parseInt(pis.substring(11,12),10)) * 4)
			            + ((parseInt(pis.substring(12,13),10)) * 3)
			            + ((parseInt(pis.substring(13,14),10)) * 2)
			            + ((parseInt(pis.substring(14,15),10)) * 1);

	            resto = soma % 11;
	
	            if (resto == 0)
	               {
	                 return true;
	               }
	            else
	               {
                     alert("Atenção! Número Provisório inválido!");
                     return false;  
	               }   
            }



            Observações:

            1) Não existe máscara para o CNS nem para o Número Provisório. O número que aparece no cartão de forma separada (898  0000  0004  3208) deverá ser digitado sem as separações.
            2) O 16º número que aparece no Cartão é o número da via do cartão, não é deverá ser digitado.
         */
    }
}
