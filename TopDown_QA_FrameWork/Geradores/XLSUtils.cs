using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;

namespace TopDown_QA_FrameWork.Geradores
{
    public class XLSUtils
    {
        public static HSSFWorkbook hssfworkbook;

        //public static void WriteToFile()
        //{
        //    //Write the stream data of workbook to the root directory
        //    FileStream file = new FileStream(@"ctf\created\CTF_teste.xls", FileMode.Create);
        //    hssfworkbook.Write(file);
        //    file.Close();
        //}

        public static void WriteToFile(string fileName)
        {
            //Write the stream data of workbook to the root directory
                
                FileStream file = new FileStream(@"ctf\created\" + fileName + ".xls", FileMode.Create);
                hssfworkbook.Write(file);
                file.Close();

            //Exceção possível lançada : CTF aberto
            
        
        }

       public static void InitializeWorkbook()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            FileStream file = new FileStream(@"ctf\template\CTF_template.xls", FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;
        }

       public static int LoadImage(string path, HSSFWorkbook wb)
       {
           FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
           byte[] buffer = new byte[file.Length];
           file.Read(buffer, 0, (int)file.Length);
           return wb.AddPicture(buffer, PictureType.JPEG);

       }
    }
}
