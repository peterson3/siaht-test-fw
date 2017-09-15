//Alerta
Browser.Wait.Until(ExpectedConditions.AlertIsPresent());
Browser.Driver.SwitchTo().Alert().Accept();