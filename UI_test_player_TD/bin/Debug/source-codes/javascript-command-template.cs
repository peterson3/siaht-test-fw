//Javascript Command
Browser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("menu"));
Browser.Sleep(3);
//Comando javascript referente ao acesso da fun��o (verificar menu)
Browser.JSexec.ExecuteScript(@"
SelecionarMenu('../../ans/asp/ans1025a.asp?cod_funcao=F&pt=Finalizar
Refer�ncia&pprf=ADMIN&pprm=N,N,N,N,N,S&pcf=ANS20.4&pm=2&pr=S', '');
");
Browser.Sleep(3);
Passou = true;