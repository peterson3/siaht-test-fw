//Dropdown Template
By modoDeProcura = By.Name("element_name");
Browser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));
var dropDownElement = new SelectElement(Browser.Driver.FindElement(modoDeProcura));
dropDownElement.SelectByText(Parametro);
Passou = true;