//CheckBox Template
string opt = "";
//Colocar elementos referentes a ordem
if (Parametro.ToUpper() == "AMBULATÓRIO / CONSULTÓRIO")
opt = "regime_1";
if (Parametro.ToUpper() == "SADT")
opt = "regime_2";
if (Parametro.ToUpper() == "INTERNAÇÃO HOSPITALAR")
opt = "regime_3";
if (Parametro.ToUpper() == "HOSPITAL DIA")
opt = "regime_4";
//Seletor Do Elemento
By modoDeProcura = By.Name(opt);
Browser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));
IWebElement checkBox_Element = Browser.Driver.FindElement(modoDeProcura);
checkBox_Element.Click();
Passou = true;