using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_test_player_TD.Model;
using ScintillaNET;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using AutocompleteMenuNS;
using System.IO;
using System.Diagnostics;


namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for EditAcaoChildUC.xaml
    /// </summary>
    public partial class EditAcaoChildUC : System.Windows.Controls.UserControl
    {
        private EditAcaoChildWindow editAcaoChildWindow;
        private AcoesView parentWindow;
        private TestCaseView testParentWindow;
        private MainWindow mainWindow;
        private AcaoDyn acao { get; set; }

        public EditAcaoChildUC(EditAcaoChildWindow editAcaoChildWindow, AcoesView parentWindow, MainWindow mainWindow, AcaoDyn acao)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.editAcaoChildWindow = editAcaoChildWindow;
            this.parentWindow = parentWindow;
            this.mainWindow = mainWindow;
            this.acao = acao;
            refresh();
        }

        public EditAcaoChildUC(EditAcaoChildWindow editAcaoChildWindow, TestCaseView testParentWindow, MainWindow mainWindow, AcaoDyn acao)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.editAcaoChildWindow = editAcaoChildWindow;
            this.testParentWindow = testParentWindow;
            this.mainWindow = mainWindow;
            this.acao = acao;
            refresh();
        }

        public void refresh()
        {
            nomeAcaoTxt.Text = acao.Nome;
            tooltipTxt.Text = acao.Tooltip;
            requerParametroCheck.IsChecked = acao.requerParametro;
  


            scintilla = new ScintillaNET.Scintilla();
            scintilla.StyleResetDefault();
            scintilla.Styles[ScintillaNET.Style.Default].Font = "Monaco";
            scintilla.Styles[ScintillaNET.Style.Default].Size = 10;
            scintilla.StyleClearAll();
            scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
            scintilla.Dock = DockStyle.Fill;


            // Configure the CPP (C#) lexer styles
            scintilla.Styles[ScintillaNET.Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = Color.Blue;
            scintilla.Styles[ScintillaNET.Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[ScintillaNET.Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[ScintillaNET.Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            scintilla.Lexer = Lexer.Cpp;


            // Set the keywords
            scintilla.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            scintilla.SetKeywords(1, "var bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
            scintilla.Text = acao.CodeScript;
            winFormHost.Child = scintilla;

            AutocompleteMenu menu = new AutocompleteMenu();
            menu.TargetControlWrapper = new ScintillaWrapper(scintilla);

            List<string> arqs = Directory.GetFiles("source-codes").ToList();


            List<string> arqsContent = new List<string>();

            foreach (var arq in arqs)
            {
                arqsContent.Add(File.ReadAllText(arq,Encoding.Default));
            }

            string[] snippets = arqsContent.ToArray();
            //string[] snippets = {   "//Input Template\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nBy modoDeProcura = By.Name(\"elementName\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement inputElement = Browser.Driver.FindElement(modoDeProcura);\ninputElement.SendKeys(Parametro);\nPassou = true;",
            //                        "//Button Template\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"toolbar\"));\nBy modoDeProcura = By.Id(\"elementId\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement btn_Element = Browser.Driver.FindElement(modoDeProcura);\nbtn_Element.Click();\nPassou = true;", 
            //                        "//RadioButton Template\n//Mudar para o Frame que contém o elemento\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nint opt = 0;\n//Colocar elementos referentes a ordem\nif (Parametro.ToUpper() == \"MÉDICO\")\nopt = 0;\nif (Parametro.ToUpper() == \"ODONTOLÓGICO\")\nopt = 1;\n//Seletor Do Elemento\nBy modoDeProcura = By.Name(\"elementName\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement radioBtn_Element = Browser.Driver.FindElements(modoDeProcura)[opt];\nradioBtn_Element.Click();\nPassou = true;",
            //                        "//CheckBox Template\n //Mudar para o Frame que contém o elemento\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nstring opt = \"\";\n//Colocar elementos referentes a ordem\nif (Parametro.ToUpper() == \"AMBULATÓRIO / CONSULTÓRIO\")\nopt = \"regime_1\";\nif (Parametro.ToUpper() == \"SADT\")\nopt = \"regime_2\";\nif (Parametro.ToUpper() == \"INTERNAÇÃO HOSPITALAR\")\nopt = \"regime_3\";\nif (Parametro.ToUpper() == \"HOSPITAL DIA\")\nopt = \"regime_4\";\n//Seletor Do Elemento\nBy modoDeProcura = By.Name(opt);\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement checkBox_Element = Browser.Driver.FindElement(modoDeProcura);\ncheckBox_Element.Click();\nPassou = true;",
            //                        "//Dropdown Template\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nBy modoDeProcura = By.Name(\"element_name\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nvar dropDownElement = new SelectElement(Browser.Driver.FindElement(modoDeProcura));\ndropDownElement.SelectByText(Parametro);\nPassou = true;",
            //                        "//Javascript Command\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"menu\"));\nBrowser.Sleep(3);\n//Comando javascript referente ao acesso da função (verificar menu)\nBrowser.JSexec.ExecuteScript(@\"\nSelecionarMenu('../../ans/asp/ans1025a.asp?cod_funcao=F&pt=Finalizar\nReferência&pprf=ADMIN&pprm=N,N,N,N,N,S&pcf=ANS20.4&pm=2&pr=S', '');\n\");\nBrowser.Sleep(3);\nPassou = true;",
            //                        "//Alerta\nBrowser.Wait.Until(ExpectedConditions.AlertIsPresent());\nBrowser.Driver.SwitchTo().Alert().Accept();",
            //                        "//VERIFICADOR\ntry\n{\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"menu\"));\nPassou = true;\n}\ncatch (Exception ex)\n{\nPassou = false;\n}\n//Se o resultado esperado é a FALHA, então inverta o sucesso.\nif (Parametro.ToUpper() == \"SIM\")\n{\nPassou = Passou;\n}\nelse //\"NÃO\"\n{\nPassou = !Passou;\n}",
            //                        File.ReadAllText("source-codes\\alterar-janela.cs"),
            //                        File.ReadAllText("source-codes\\by-cssSelector.cs")
            //                   };
        
            //string[] snippets = { "if(^)\n{\n}", "if(^)\n{\n}\nelse\n{\n}", "for(^;;)\n{\n}", "while(^)\n{\n}", "do${\n^}while();", "switch(^)\n{\n\tcase : break;\n}" }; 
            menu.Items = snippets; 

        }

        private void validarEdicao(object sender, RoutedEventArgs e)
        {
            acao.Nome = nomeAcaoTxt.Text;
            //acao.CodeScript = new TextRange(CodeTxt.Document.ContentStart, CodeTxt.Document.ContentEnd).Text;
            acao.CodeScript = scintilla.Text;
            acao.Tooltip = tooltipTxt.Text;
            acao.requerParametro = requerParametroCheck.IsChecked.Value;
            acao.Update();
            if (parentWindow != null)
            {
                this.parentWindow.refresh();
            }
            if (testParentWindow != null)
            {                
                testParentWindow.refresh(); 
            }
            this.editAcaoChildWindow.Close();
            mainWindow.FlyOutFeedBack("Alterações Salvas");

        }

        private void cancelar(object sender, RoutedEventArgs e)
        {
            //if (parentWindow != null)
            //{
            //    this.parentWindow.refresh();
            //}
            //if (testParentWindow != null)
            //{
            //    testParentWindow.refresh();
            //}
            this.editAcaoChildWindow.Close();
        }

        private void abrirDoc(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Settings.docPath))
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.UseShellExecute = true;
                processInfo.FileName = Settings.docPath;
                Process.Start(processInfo);
            }
            else
            {

            }
        }
    }
}
