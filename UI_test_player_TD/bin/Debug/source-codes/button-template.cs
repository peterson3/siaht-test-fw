//Button Template
By modoDeProcura = By.Id("elementId");
Browser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));
IWebElement btn_Element = Browser.Driver.FindElement(modoDeProcura);
btn_Element.Click();
Passou = true;
