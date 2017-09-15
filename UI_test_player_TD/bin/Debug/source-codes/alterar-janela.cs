//Alterar Foco Janela
Retorno = "";
Browser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("iframeasp"));
Browser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("principal2"));

//Retorno += Browser.Driver.Title + ";";
//Retorno += Browser.Driver.Url + ";";

//Clicar na Lupa
Browser.Wait.Until(ExpectedConditions.ElementExists(By.CssSelector("img[title='Pesquisar Grupo Estatístico']")));
IWebElement btn_Element =  Browser.Driver.FindElement(By.CssSelector("img[title='Pesquisar Grupo Estatístico']"));
//Browser.Action.MoveToElement(btn_Element).Click().Build().Perform();
btn_Element.Click();


//Browser.Driver.SwitchTo().DefaultContent();

//Retorno += Browser.Driver.Title + ";";
//Retorno += Browser.Driver.Url + ";";



//Entrar no Contexto da Nova Janela
var janelas = Browser.Driver.WindowHandles;

var framePrincipal2 = Browser.Driver.CurrentWindowHandle;
Browser.Driver.SwitchTo().DefaultContent();
var janelaPrincipal = Browser.Driver.CurrentWindowHandle;
var janelaSecundaria = Browser.Driver.CurrentWindowHandle;


foreach (var janela in janelas)
{

    if ((janela != framePrincipal2) && (janela != janelaPrincipal))
    {
        //Janela Secundária
        janelaSecundaria = janela;
    }
}

Browser.Driver.SwitchTo().Window(janelaSecundaria);
//Retorno += Browser.Driver.Title; //Prova de que está na janela

//Clicar no Botão Continuar da Nova Janela
Browser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("toolbar2"));
Browser.Wait.Until(ExpectedConditions.ElementExists(By.Id("btn_acao_continuar")));
IWebElement btn_Element2 = Browser.Driver.FindElement(By.Id("btn_acao_continuar"));
btn_Element2.Click();

Browser.Driver.SwitchTo().DefaultContent();

//Selecionar o link que possui o mesmo text do parametro
Browser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("principal2"));
Browser.Wait.Until(ExpectedConditions.ElementExists(By.ClassName("grid_left")));
var elements = Browser.Driver.FindElements(By.ClassName("grid_left"));
Retorno = "";
foreach (var webElement in elements)
{
    //Retorno += webElement.Text.Trim().ToUpper() + "-";
    if (webElement.Text.Trim().ToUpper() == Parametro.Trim().ToUpper())
    {
      webElement.Click();
      break;
    }   
}

//Entrar no Contexto da Janela Principal
Browser.Driver.SwitchTo().Window(janelaPrincipal);

 
Passou = true;