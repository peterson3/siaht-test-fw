using ScintillaNET;
using AutocompleteMenuNS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.DB;
using UI_test_player_TD.Model;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for AddAcaoChildUC.xaml
    /// </summary>
    public partial class AddAcaoChildUC : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        private AddAcaoChildWindow addAcaoChildWindow;
        private AcoesView parentWindow;
        private MainWindow mainWindow;
        public ObservableCollection<Sistema> sistemas { get; set; }
        public ObservableCollection<Tela> telas { get; set; }

        private Sistema _selectedSistema { get; set; }
        public Sistema selectedSistema
        {
            get { return _selectedSistema; }
            set {
                _selectedSistema = value;
                telas = _selectedSistema.telas;
                telasCombo.ItemsSource = telas;
                telasCombo.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Nome", System.ComponentModel.ListSortDirection.Ascending));    
            }
        }


        public Tela selectedTela { get; set; }


        public AddAcaoChildUC(AddAcaoChildWindow addAcaoChildWindow, AcoesView parentWindow, MainWindow mainWindow)
        {
            // TODO: Complete member initialization
            InitializeComponent();


            this.addAcaoChildWindow = addAcaoChildWindow;
            this.parentWindow = parentWindow;
            this.mainWindow = mainWindow;
            this.refresh();

            RoutedCommand saveCmd = new RoutedCommand();
            saveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(saveCmd, adicionarAcao));
        }

        public AddAcaoChildUC(AddAcaoChildWindow addAcaoChildWindow, AcoesView parentWindow, MainWindow mainWindow, Tela selectedTela1, Sistema selectedSistema1) : this(addAcaoChildWindow, parentWindow, mainWindow)
        {
            this.selectedSistema = selectedSistema1;

            foreach (Sistema item in sistemasComboBox.Items)
            {
                if (selectedSistema.Id == item.Id)
                {
                    this.selectedSistema = item;
                }
            }
            this.selectedTela = selectedTela1;
            foreach (Tela item in telasCombo.Items)
            {
                if (selectedTela.Id == item.Id)
                {
                    this.selectedTela = item;
                }
            }
        }

        public AddAcaoChildUC(AddAcaoChildWindow addAcaoChildWindow, TestCaseView parentWindow1, MainWindow mainWindow, Tela selectedTela1, Sistema selectedSistema1)
            : this(addAcaoChildWindow, null, mainWindow)
        {
            this.selectedSistema = selectedSistema1;

            foreach (Sistema item in sistemasComboBox.Items)
            {
                if (selectedSistema.Id == item.Id)
                {
                    this.selectedSistema = item;
                }
            }
            this.selectedTela = selectedTela1;
            foreach (Tela item in telasCombo.Items)
            {
                if (selectedTela.Id == item.Id)
                {
                    this.selectedTela = item;
                }
            }
        } 

        private void adicionarAcao(object sender, RoutedEventArgs e)
        {
            if (selectedTela == null)
            {
                mainWindow.FlyOutFeedBack("Selecione Sistema / Tela");
                return;
            }
            AcaoDyn acaoAdd = new AcaoDyn(nomeAcaoTxt.Text, selectedTela);
            acaoAdd.CodeScript = scintilla.Text;
            acaoAdd.requerParametro = requerParametroCheck.IsChecked.Value;
            acaoAdd.Salvar();
            if (parentWindow != null)
                parentWindow.refresh();
            if (parentWindow1 != null)
            {
                parentWindow1.refresh();
            }

            this.addAcaoChildWindow.Close();
            mainWindow.FlyOutFeedBack("Ação Adicionada");
        }

        private void cancelar(object sender, RoutedEventArgs e)
        {
            parentWindow.refresh();
            this.addAcaoChildWindow.Close();
        }

        public void refresh()
        {
            sistemasComboBox.DataContext = this;
            telasCombo.DataContext = this;
            sistemas = Sistema_DAO.getAllSistemas();
            sistemasComboBox.ItemsSource = sistemas;

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
            winFormHost.Child = scintilla;



            AutocompleteMenu menu = new AutocompleteMenu();
            menu.TargetControlWrapper = new ScintillaWrapper(scintilla);


            List<string> arqs = Directory.GetFiles("source-codes").ToList();


            List<string> arqsContent = new List<string>();

            foreach (var arq in arqs)
            {
                arqsContent.Add(File.ReadAllText(arq, Encoding.Default));
            }

            string[] snippets = arqsContent.ToArray();


            //string[] snippets = { "//Input Template\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nBy modoDeProcura = By.Name(\"elementName\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement inputElement = Browser.Driver.FindElement(modoDeProcura);\ninputElement.SendKeys(Parametro);\nPassou = true;",
            //                        "//Button Template\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"toolbar\"));\nBy modoDeProcura = By.Id(\"elementId\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement btn_Element = Browser.Driver.FindElement(modoDeProcura);\nbtn_Element.Click();\nPassou = true;", 
            //                        "//RadioButton Template\n//Mudar para o Frame que contém o elemento\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nint opt = 0;\n//Colocar elementos referentes a ordem\nif (Parametro.ToUpper() == \"MÉDICO\")\nopt = 0;\nif (Parametro.ToUpper() == \"ODONTOLÓGICO\")\nopt = 1;\n//Seletor Do Elemento\nBy modoDeProcura = By.Name(\"elementName\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement radioBtn_Element = Browser.Driver.FindElements(modoDeProcura)[opt];\nradioBtn_Element.Click();\nPassou = true;",
            //                        "//CheckBox Template\n //Mudar para o Frame que contém o elemento\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nstring opt = \"\";\n//Colocar elementos referentes a ordem\nif (Parametro.ToUpper() == \"AMBULATÓRIO / CONSULTÓRIO\")\nopt = \"regime_1\";\nif (Parametro.ToUpper() == \"SADT\")\nopt = \"regime_2\";\nif (Parametro.ToUpper() == \"INTERNAÇÃO HOSPITALAR\")\nopt = \"regime_3\";\nif (Parametro.ToUpper() == \"HOSPITAL DIA\")\nopt = \"regime_4\";\n//Seletor Do Elemento\nBy modoDeProcura = By.Name(opt);\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nIWebElement checkBox_Element = Browser.Driver.FindElement(modoDeProcura);\ncheckBox_Element.Click();\nPassou = true;",
            //                        "//Dropdown Template\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"principal\"));\nBy modoDeProcura = By.Name(\"element_name\");\nBrowser.Wait.Until(ExpectedConditions.ElementExists(modoDeProcura));\nvar dropDownElement = new SelectElement(Browser.Driver.FindElement(modoDeProcura));\ndropDownElement.SelectByText(Parametro);\nPassou = true;",
            //                        "//Javascript Command\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"menu\"));\nBrowser.Sleep(3);\n//Comando javascript referente ao acesso da função (verificar menu)\nBrowser.JSexec.ExecuteScript(@\"\nSelecionarMenu('../../ans/asp/ans1025a.asp?cod_funcao=F&pt=Finalizar\nReferência&pprf=ADMIN&pprm=N,N,N,N,N,S&pcf=ANS20.4&pm=2&pr=S', '');\n\");\nBrowser.Sleep(3);\nPassou = true;",
            //                        "//Alerta\nBrowser.Wait.Until(ExpectedConditions.AlertIsPresent());\nBrowser.Driver.SwitchTo().Alert().Accept();",
            //                        "//VERIFICADOR\ntry\n{\nBrowser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(\"menu\"));\nPassou = true;\n}\ncatch (Exception ex)\n{\nPassou = false;\n}\n//Se o resultado esperado é a FALHA, então inverta o sucesso.\nif (Parametro.ToUpper() == \"SIM\")\n{\nPassou = Passou;\n}\nelse //\"NÃO\"\n{\nPassou = !Passou;\n}",
            //                        };
            //string[] snippets = { "if(^)\n{\n}", "if(^)\n{\n}\nelse\n{\n}", "for(^;;)\n{\n}", "while(^)\n{\n}", "do${\n^}while();", "switch(^)\n{\n\tcase : break;\n}" }; 
            menu.Items = snippets; 
            
            //scintilla.TextChanged += scintilla_TextChanged;

            //scintilla.ContextMenu = new System.Windows.Forms.ContextMenu();
            //scintilla.ContextMenu.MenuItems.Add(new System.Windows.Forms.MenuItem("Template INPUT"));
        }

        void scintilla_TextChanged(object sender, EventArgs e)
        {
            ////var jolt = new Jolt.XmlDocCommentReader(Assembly.GetExecutingAssembly());
            //var jolt = new Jolt.XmlDocCommentReader(Assembly.LoadFrom(@"D:/Peterson/Projetos/QADynamicFramework/UI_test_player_TD/bin/Debug/TopDown_QA_FrameWork.dll"));
            //scintilla.SetColumnMargins();
            //scintilla.ShowAutoComplete();
            //scintilla.ShowToolTip(jolt);

            ////ScintillaExtender.SetColumnMargins(scintilla);
            ////ScintillaExtender.ShowAutoComplete(scintilla);
            ////ScintillaExtender.ShowToolTip(scintilla, jolt);
        }

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private TestCaseView parentWindow1;
        private Tela selectedTela1;
        private Sistema selectedSistema1;

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
