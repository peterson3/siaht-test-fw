//RadioButton Template
int opt = 0;
//Colocar elementos referentes a ordem
if (Parametro.ToUpper() == "M�DICO")
opt = 0;
if (Parametro.ToUpper() == "ODONTOL�GICO")
opt = 1;
//Seletor Do Elemento
By modoDeProcura = By.Name("elementName");
Browser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));
IWebElement radioBtn_Element = Browser.Driver.FindElements(modoDeProcura)[opt];
radioBtn_Element.Click();
Passou = true;
