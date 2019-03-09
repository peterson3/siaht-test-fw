using System;
using System.CodeDom.Compiler;
using System.Text;

namespace UI_test_player_TD.Model
{
    public class AcaoDyn
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Screen TelaPai { get; set; }
        public string Tooltip { get; set; }
        public bool requerParametro { get; set; }
        public string CodeScript { get; set; }


        public AcaoDyn(string Nome, Screen TelaPai)
        {
            this.Nome = Nome;
            this.TelaPai = TelaPai;

            //Salvar No Banco E Receber Novo Id
        }

        public void Salvar()
        {
           // AcaoDyn_DAO.Salvar(this);

        }

        public AcaoDynResult Executar(string Parametro)
        {
            AcaoDynResult Result = new AcaoDynResult();

            //MessageBox.Show("COdeScript:" + CodeScript);

            StringBuilder sb = new StringBuilder();

            //-----------------
            // Create the class as usual
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Windows.Forms;");
            sb.AppendLine("using System.Collections;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Text.RegularExpressions;");
            sb.AppendLine("using System.Collections.ObjectModel;");
            sb.AppendLine("using TopDown_QA_FrameWork;");
            sb.AppendLine("using System.Runtime.InteropServices;");
            sb.AppendLine("using OpenQA.Selenium;");
            sb.AppendLine("using OpenQA.Selenium.Support.PageObjects;");
            sb.AppendLine("using OpenQA.Selenium.Support.UI;");
            sb.AppendLine("using System.ComponentModel;");

            sb.AppendLine();
            sb.AppendLine("namespace TestFrameWork");
            sb.AppendLine("{");

            sb.AppendLine("      public class ActionToRun");
            sb.AppendLine("      {");

            // My pre-defined class named FilterCountries that receive the sourceListBox
            sb.AppendLine("            public void RunAction(string Parametro, out bool? Passou, out string Retorno)");
            sb.AppendLine("            {");
            sb.AppendLine("Browser.Driver.SwitchTo().DefaultContent();");
           // sb.AppendLine("Passou = null;");
            sb.AppendLine("Retorno = \"\";");
            sb.AppendLine(CodeScript);
            sb.AppendLine("            }");
            sb.AppendLine("            public void IR_PARA_CONTEUDO_PRINCIPAL() {Browser.Driver.SwitchTo().DefaultContent();}");
            sb.AppendLine("            public void MUDAR_PARA_FRAME(string frameContainer) {Browser.Wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameContainer));}");
            //sb.AppendLine("            public void IR_PARA_CONTEUDO_PRINCIPAL() {Browser.Driver.SwitchTo().DefaultContent();}");
            //sb.AppendLine("            public void IR_PARA_CONTEUDO_PRINCIPAL() {Browser.Driver.SwitchTo().DefaultContent();}");
            //sb.AppendLine("            public void IR_PARA_CONTEUDO_PRINCIPAL() {Browser.Driver.SwitchTo().DefaultContent();}");

            sb.AppendLine("      }");
            sb.AppendLine("}");

            //-----------------
            // The finished code
            String classCode = sb.ToString();

            //-----------------
            // Dont need any extra assemblies
            Object[] requiredAssemblies = new Object[] { "TopDown_QA_FrameWork.dll" };

            dynamic classRef;
            try
            {
                //txtErrors.Clear();

                //------------
                // Pass the class code, the namespace of the class and the list of extra assemblies needed
                //--classRef = CodeHelper.HelperFunction(classCode, "TestFrameWork.ActionToRun", requiredAssemblies);

                //-------------------
                // If the compilation process returned an error, then show to the user all errors
                if (classRef is CompilerErrorCollection)
                {
                    StringBuilder sberror = new StringBuilder();

                    foreach (CompilerError error in (CompilerErrorCollection)classRef)
                    {
                        sberror.AppendLine(string.Format("{0}:{1} {2} {3}", error.Line, error.Column, error.ErrorNumber, error.ErrorText));
                    }

                    //txtErrors.Text = sberror.ToString();
                    MessageBox.Show("ERRO DE CÓDIGO: " + sberror.ToString());
                    Result.erro = sberror.ToString();
                    return Result;
                }
            }
            catch (Exception ex)
            {
                // If something very bad happened then throw it
                //MessageBox.Show(ex.Message);
                Result.erro = ex.Message;
                throw;
            }

            bool? passou;
            string retornoInformacao;
            //-------------
            // Finally call the class to filter the countries with the specific routine provided
            classRef.RunAction(Parametro, out passou, out retornoInformacao);

            Result.passou = passou;
            Result.retornoInformacao = retornoInformacao;

            //MessageBox.Show(Result.passou.ToString() + Result.retornoInformacao);
            //List<string> targetValues = classRef.FilterCountries(lstSource);
            //List<string> targetValues = new List<String>();
            ////-------------
            //// Move the result to the target listbox
            //lstTarget.Items.Clear();
            //lstTarget.Items.AddRange(targetValues.ToArray());

            return Result;
        }

        public void Update()
        {
            AcaoDyn_DAO.Update(this);
        }

        public void Deletar()
        {
            AcaoDyn_DAO.Deletar(this);
        }
    }
}
