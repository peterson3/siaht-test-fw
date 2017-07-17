using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDown_QA_FrameWork.Geradores
{
    public static class Logger
    {
        public static void escrever(string text)
        {

            //Estrutura do Nome do arquivo 
            //tdfu.2016.05.23.log

            //Log POR DIA
            //Recuperar o Dia de HOJE
            int ano = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;
            string logName = "QAFrameWork." + ano.ToString() + "." + mes.ToString() + "." + dia.ToString() + ".log";

            //Abre e Escreve no fim (append)
            //Se o arquivo não existir, é criado
            StreamWriter arq = new StreamWriter("Files/" + logName, true);
            arq.WriteLine("[" + DateTime.Now.ToString() + "] " + text);
            arq.Close();

        }

        public static bool abrir()
        {
            int ano = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;
            string logName = "QAFrameWork." + ano.ToString() + "." + mes.ToString() + "." + dia.ToString() + ".log";

            if (File.Exists("Files\\" + logName))
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.UseShellExecute = true;
                processInfo.FileName = logName;
                processInfo.WorkingDirectory = "Files\\";
                Process.Start(processInfo);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
