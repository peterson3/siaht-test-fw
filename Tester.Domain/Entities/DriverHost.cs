using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;



namespace TopDown_QA_FrameWork
{

    public class DriverHost
    {
        public const int IE_BROWSER = 1;
        public const int CHROME_BROWSER = 2;
        public const int FIREFOX_BROWSER = 3;
        public const int SAFARI_BROWSER = 4;
        public const int EDGE_BROWSER = 5;
        public const int OPERA_BROWSER = 6;


        private static string baseUrl; // = "http://10.10.100.147";
        private static InternetExplorerOptions opcoesIE;
        private static ChromeOptions opcoesChrome;
        private static FirefoxOptions opcoesFirefox;
        private static SafariOptions opcoesSafari;
        private static EdgeOptions opcoesEdge;
        private static OperaOptions opcoesOpera;

        private static IWebDriver webDriver;
        public static IWebDriver auxWebDriver;

        private static InternetExplorerDriverService ieService;

        private static WebDriverWait wait;
        public static WebDriverWait auxWait;
        private static Actions acoes;
        public static Actions auxAcoes;
        private static IJavaScriptExecutor executor;
        //private const string IE_DRIVER_PATH = @"D:\Peterson\Projetos\QADynamicFramework\TopDown_QA_FrameWork\WebDriver\IEDriverServer_Win32_2.53.1\IEDriverServer.exe";
        //private const string IE_DRIVER_PATH = @"D:\Peterson\Projetos\TopDown_QA_FrameWork\TopDown_QA_FrameWork\bin\Debug\WebDriver\IEDriverServer_Win32_2.53.1";
        //private  string IE_DRIVER_PATH = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"\WebDriver\IEDriverServer_Win32_2.53.1\");
        private static int PRINT_CONT;
        private static bool isWebDriverOpen = false;

        public static void Initialize(string baseUrl, int COD_BROWSER, string IE_DRIVER_PATH, string CHROME_DRIVER_PATH, string FIREFOX_DRIVER_PATH, string SAFARI_DRIVER_PATH, string EDGE_DRIVER_PATH, string OPERA_DRIVER_PATH)
        {
            //Overall Config
            isWebDriverOpen = true;
            TopDown_QA_FrameWork.Driver.baseUrl = baseUrl;
            PRINT_CONT = 0;

            switch (COD_BROWSER)
            {
                case IE_BROWSER:
                    //IE Configurações
                    opcoesIE = new InternetExplorerOptions();
                    //new
                    //opcoesIE.RequireWindowFocus = true;
                    //opcoesIE.PageLoadStrategy = InternetExplorerPageLoadStrategy.Eager;
                    //opcoesIE.EnablePersistentHover = true;
                    //opcoesIE.ElementScrollBehavior = InternetExplorerElementScrollBehavior.Top;
                    //
                    //opcoesIE.ForceCreateProcessApi = true;
                    opcoesIE.EnableNativeEvents = true;
                    opcoesIE.EnsureCleanSession = true;
                   //opcoesIE.RequireWindowFocus = true;
                   opcoesIE.PageLoadStrategy = InternetExplorerPageLoadStrategy.Normal;
                   opcoesIE.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieService = InternetExplorerDriverService.CreateDefaultService(IE_DRIVER_PATH);
                    //ieService = InternetExplorerDriverService.CreateDefaultService(@"D:\Peterson\Projetos\QADynamicFramework\UI_test_player_TD\bin\Debug\WebDriver\IEDriverServer_Win32_2.53.1");
                    //ieService = InternetExplorerDriverService.CreateDefaultService();
                    ieService.HideCommandPromptWindow = true;
                    webDriver = new InternetExplorerDriver(ieService, opcoesIE);
                    break;

                case CHROME_BROWSER:
                    //// Chrome 
                    opcoesChrome = new ChromeOptions();
                    ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(CHROME_DRIVER_PATH);
                    chromeDriverService.HideCommandPromptWindow = true;
                    webDriver = new ChromeDriver(chromeDriverService, opcoesChrome);
                    break;

                case FIREFOX_BROWSER:
                    //Firefox Configurações
                    opcoesFirefox = new FirefoxOptions();
                    FirefoxDriverService firefoxDriverService = FirefoxDriverService.CreateDefaultService(FIREFOX_DRIVER_PATH);
                    firefoxDriverService.HideCommandPromptWindow = true;
                    FirefoxProfile ffProfile = new FirefoxProfile();
                    ffProfile.EnableNativeEvents = true;
                    webDriver = new FirefoxDriver(firefoxDriverService);
                    break;
                    
                case SAFARI_BROWSER:
                    opcoesSafari = new SafariOptions();
                    SafariDriverService safariDriverservice = SafariDriverService.CreateDefaultService();
                    webDriver = new SafariDriver();
                    //Logger.abrir();
                    //DesiredCapabilities caps = new DesiredCapabilities();
                    //caps.SetCapability("browserName", "safari");
                    //caps.SetCapability("plataform", "WINDOWS");
                    //webDriver = new RemoteWebDriver(new Uri("http://google.com"), caps);
                    break;

                case EDGE_BROWSER:
                    opcoesEdge = new EdgeOptions();
                    EdgeDriverService edgeDriverService = EdgeDriverService.CreateDefaultService(EDGE_DRIVER_PATH);
                    opcoesEdge.AddAdditionalCapability("nativeEvents", true);
                    edgeDriverService.HideCommandPromptWindow = true;
                    webDriver = new EdgeDriver(edgeDriverService, opcoesEdge);
                    break;

                case OPERA_BROWSER:
                    opcoesOpera = new OperaOptions();
                    OperaDriverService operaDriverService = OperaDriverService.CreateDefaultService(OPERA_DRIVER_PATH);
                    opcoesOpera.AddAdditionalCapability("nativeEvents", true);
                    operaDriverService.HideCommandPromptWindow = true;
                    webDriver = new OperaDriver(operaDriverService, opcoesOpera);
                    break;

            }

            //Post Configuration
            wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 30));
            acoes = new Actions(webDriver);
            webDriver.Navigate().GoToUrl(baseUrl);
            webDriver.Manage().Window.Maximize();
            //webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            
        }

        /// <summary>
        /// Teste
        /// </summary>
        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static Actions Action
        {
            get { return acoes; }
        }

        public static WebDriverWait Wait
        {
            get { return wait; }
        }

        public static IWebDriver Driver
        {
            get { return webDriver; }
        }

        public static IJavaScriptExecutor JSexec
        {
            get { return (IJavaScriptExecutor)TopDown_QA_FrameWork.Driver.Driver; }
        }

        public static void Goto(string url)
        {
            webDriver.Url = baseUrl + url;
        }

        public static void Close()
        {
            if (isWebDriverOpen)
            {
                try
                {
                    webDriver.Dispose();
                    webDriver.Quit();
                    webDriver.Close();
                }
                catch (Exception ex)
                {
                    //browser teve seu fechamento forçado
                }
                finally
                {
                    isWebDriverOpen = false;
                }

            }
        }

        /// <summary>
        /// Retorna a local onde foi salvo o print
        /// </summary>
        /// <returns></returns>
        public static string Print()
        {
            try
            {

                Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
                PRINT_CONT++;
                ss.SaveAsFile(@"ctf\prints\" + PRINT_CONT.ToString() + ".jpg", ImageFormat.Jpeg);
                TopDown_QA_FrameWork.Driver.Sleep(2);
                return (@"ctf\prints\" + PRINT_CONT.ToString() + ".jpg");
            }
            catch (Exception e)
            {
                Logger.escrever(e.Message);
                throw e;
            }
        }

        //para propósitos de debug
        public static void printElementAttributes(IWebElement element)
        {
            Logger.escrever("CSS - visibility: " + element.GetCssValue("visibility"));
            Logger.escrever("CSS - display: " + element.GetCssValue("display"));
            Logger.escrever("is Displayed: " + element.Displayed.ToString());
            Logger.escrever("is Enabled: " + element.Enabled.ToString());
            Logger.escrever("Location: " + element.Location.ToString());
            Logger.escrever("selected :" + element.Selected.ToString());
            Logger.escrever("TagName: " + element.TagName.ToString());
            Logger.escrever("Size: " + element.Size);
            Logger.escrever("Type Attribute: " + element.GetAttribute("type"));
            Logger.escrever("Text: " + element.Text);
        }

        public static void ClickElementViaActions(IWebElement element)
        {
            TopDown_QA_FrameWork.Driver.Action.MoveToElement(element).Click().Build().Perform();
            //Browser.Action.Click().Build().Perform();
            //Browser.Action.
        }

        public static void getAllIFramesNames()
        {
            TopDown_QA_FrameWork.Driver.Driver.SwitchTo().DefaultContent();

            IReadOnlyCollection<IWebElement> elements = TopDown_QA_FrameWork.Driver.Driver.FindElements(By.TagName("iframe"));
            foreach (IWebElement element in elements)
            {
                Logger.escrever(element.TagName + ":" + element.GetAttribute("name"));
            }
        }

        public static void getAllElementsFromIframe(string iFrame)
        {
            TopDown_QA_FrameWork.Driver.Driver.SwitchTo().DefaultContent();
            TopDown_QA_FrameWork.Driver.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(iFrame));

            TopDown_QA_FrameWork.Driver.Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("*")));


            IReadOnlyCollection<IWebElement> elements = TopDown_QA_FrameWork.Driver.Driver.FindElements(By.XPath("*"));

            Logger.escrever("################     "+iFrame+"     ################");


            foreach (IWebElement element in elements)
            {
                Logger.escrever(element.TagName.ToUpper());
                Logger.escrever("NAME: " + element.GetAttribute("name"));
                Logger.escrever("INNERHTML: " + element.GetAttribute("innerHTML"));
                Logger.escrever("TEXT: " + element.GetAttribute("text"));
                Logger.escrever("\n\n\n");
            }

        }

        public static String generateXPATH(IWebElement childElement, String current)
        {
            String childTag = childElement.TagName;
            if (childTag.Equals("html"))
            {
                return "/html[1]" + current;
            }
            IWebElement parentElement = childElement.FindElement(By.XPath(".."));
            List<IWebElement> childrenElements = parentElement.FindElements(By.XPath("*")).ToList();
            int count = 0;
            for (int i = 0; i < childrenElements.Count; i++)
            {
                IWebElement childrenElement = childrenElements[i];
                String childrenElementTag = childrenElement.TagName;
                if (childTag.Equals(childrenElementTag))
                {
                    count++;
                }
                if (childElement.Equals(childrenElement))
                {
                    return generateXPATH(parentElement, "/" + childTag + "[" + count + "]" + current);
                }
            }
            return null;
        }

        public static void openAuxDriver (string url)
        {
            // Chrome 
            opcoesChrome = new ChromeOptions();
            ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService(@"C:\Users\peterson\Documents\Visual Studio 2013\Projects\TopDown_QA_FrameWork\TopDown_QA_FrameWork\bin\Debug\WebDriver\chromedriver_win32");
            chromeDriverService.HideCommandPromptWindow = true;
            
            auxWebDriver= new ChromeDriver(chromeDriverService, opcoesChrome);
            auxWebDriver.Manage().Window.Position = new System.Drawing.Point(-2000, 0);
            auxWait = new WebDriverWait(auxWebDriver, new TimeSpan(0, 0, 20));
            auxAcoes = new Actions(auxWebDriver);
            auxWebDriver.Navigate().GoToUrl(url);
            //auxWebDriver.Manage().Window.Maximize();
        }

        public static void closeAuxDriver()
        {
            auxWebDriver.Close();
        }
        
        public static void injectJquery()
        {
            //Browser.JSexec.ExecuteScript(File.ReadAllText("jquery-1.12.4.min.js"));
            //Browser.JSexec.ExecuteScript(File.ReadAllText("jquery-migrate-1.4.1.js"));
            //Browser.JSexec.ExecuteScript(File.ReadAllText("jquery-3.1.1.min.js"));
            //Browser.JSexec.ExecuteScript(File.ReadAllText("jquery-migrate-3.0.0.min.js"));
        }

        public static void Sleep(int segundos)
        {
            TimeSpan tempo = new TimeSpan(0,0,segundos);
            System.Threading.Thread.Sleep(tempo);
        }

        public static string getSourceFromFrame(string frameDesejado)
        {
            TopDown_QA_FrameWork.Driver.Driver.SwitchTo().DefaultContent();
            string   s = (string)TopDown_QA_FrameWork.Driver.JSexec.ExecuteScript("function getFrameSrc(){var a,b=document.getElementsByTagName('iframe');for(i=0;i<b.length;i++)'" + frameDesejado + "'==b[i].name&&(a=b[i].contentWindow.location.href);return a}var TESTE_AUTO = getFrameSrc();return TESTE_AUTO;");
            Match match = Regex.Match(s, @"((\w)+\.asp)");
            s = match.Value;
            return s;
        }

        public static void reloadFrame(string frameName)
        {
            TopDown_QA_FrameWork.Driver.JSexec.ExecuteScript("var b=document.getElementsByTagName('iframe');for(i=0;i<b.length;i++)'" + frameName + "'==b[i].name&&b[i].contentWindow.location.reload();");
        }

        

    }
}
