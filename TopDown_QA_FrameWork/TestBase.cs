using TopDown_QA_FrameWork.Geradores;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TopDown_QA_FrameWork
{
    [TestFixture]
    public class TestBase
    {
        public static int WAIT_TIME = 1; 

        [OneTimeSetUp]
        public static void Initialize(int COD_BROWSER, string IE_DRIVER_PATH, string CHROME_DRIVER_PATH, string FIREFOX_DRIVER_PATH, string SAFARI_DRIVER_PATH, string EDGE_DRIVER_PATH, string OPERA_DRIVER_PATH)
        {
            Browser.Initialize("http://10.10.100.147", COD_BROWSER, IE_DRIVER_PATH, CHROME_DRIVER_PATH, FIREFOX_DRIVER_PATH, SAFARI_DRIVER_PATH, EDGE_DRIVER_PATH, OPERA_DRIVER_PATH);
           // UserGenerator.Initialize();

        }

        [OneTimeTearDown]
        public static void TestFixtureTearDown()
        {
           //Browser.Close();
           //Logger.abrir();
        }

        [TearDown]
        public static void TearDown()
        {
            //Logger.abrir();
            //Retorno = "";
            //var elements = Browser.Driver.FindElements(By.ClassName("tab"));
            //foreach (var webElement in elements)
            //{
            //    Retorno += webElement.Text;
            //}
            //Browser.Driver.FindElement(By.CssSelector("img[title='Pesquisar Grupo Estatístico']"));
            //Browser.Action.SendKeys(OpenQA.Selenium.Keys.Tab).Build().Perform();
            //var janelas = Browser.Driver.WindowHandles;
            //var janelaSecundaria = Browser.Driver.CurrentWindowHandle;
            //foreach (var janela in janelas)
            //{
            //    if (janela != Browser.Driver.CurrentWindowHandle)
            //    {
            //        janelaSecundaria = janela;
            //    }
            //}
            //Browser.Driver.SwitchTo().Window(janelaSecundaria);
            //Browser.Wait.Until(ExpectedConditions.ElementExists(By.Name("")));
            //Browser.Action.SendKeys(element, Parametro).Build().Perform();
            //Browser.Action.Click(element).Build().Perform();
            //Browser.Driver.FindElement(By.Name("Teste")).Clear();
            //Browser.Driver.FindElement(By.TagName("a"));
            //Browser.Wait.Until(ExpectedConditions.AlertIsPresent());
            //Browser.Wait.Until(ExpectedConditions.ElementIsVisible());
            //Browser.Wait.Until(ExpectedConditions.ElementSelectionStateToBe(,true);
            //Browser.Wait.Until(ExpectedConditions.ElementToBeClickable())
            //Browser.Driver.Navigate().GoToUrl("");
            //Browser.Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("idElemento")));
        }

    }
}
