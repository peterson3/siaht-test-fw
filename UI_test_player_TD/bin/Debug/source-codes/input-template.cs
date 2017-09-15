//Input Template
By modoDeProcura = By.Name("elementName");
Browser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));
IWebElement inputElement = Browser.Driver.FindElement(modoDeProcura);
inputElement.SendKeys(Parametro);
Passou = true;
