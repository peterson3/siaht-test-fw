using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace UI_test_player_TD
{
    public class CodeHelper
    {
        public static object HelperFunction(String classCode, String mainClass, Object[] requiredAssemblies)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });

            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = false,       // Create a dll
                GenerateInMemory = true,          // Create it in memory
                WarningLevel = 2,                 // Default warning level
                CompilerOptions = "/optimize",    // Optimize code
                TreatWarningsAsErrors = false     // Better be false to avoid break in warnings
            };

            //----------------
            // Add basic referenced assemblies
            parameters.ReferencedAssemblies.Add("system.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");


            //Person
            //using System.Runtime.InteropServices;
            //using TopDown_QA_FrameWork.Geradores;
            //using OpenQA.Selenium;
            //using OpenQA.Selenium.Support.PageObjects;
            //using OpenQA.Selenium.Support.UI;
            //using System;
            //using System.Collections.Generic;
            //using TopDown_QA_FrameWork.Paginas;
            //using System.ComponentModel;
            //using System.Collections.ObjectModel;
            parameters.ReferencedAssemblies.Add("TopDown_QA_FrameWork.dll");
            parameters.ReferencedAssemblies.Add("nunit.framework.dll");
            parameters.ReferencedAssemblies.Add("Selenium.WebDriverBackedSelenium.dll");
            parameters.ReferencedAssemblies.Add("ThoughtWorks.Selenium.Core.dll");
            parameters.ReferencedAssemblies.Add("WebDriver.Support.dll");
            parameters.ReferencedAssemblies.Add("WebDriver.dll");


            //----------------
            // Add all extra assemblies required
            foreach (var extraAsm in requiredAssemblies)
            {
                parameters.ReferencedAssemblies.Add(extraAsm as string);
            }

            //--------------------
            // Try to compile the code received
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, classCode);

            //--------------------
            // If the compilation returned error, then return the CompilerErrorCollection class with the errors to the caller
            if (results.Errors.Count != 0)
            {
                return results.Errors;
            }

            //--------------------
            // Return the created class instance to caller
            return results.CompiledAssembly.CreateInstance(mainClass); ;
        }
    }
}
