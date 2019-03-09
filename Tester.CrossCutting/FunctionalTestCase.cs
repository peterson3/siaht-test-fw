using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.CrossCutting
{
    public static class FunctionalTestCase
    {
        public static int imgsQtd = 0;
        public static int passo = 0;
        public static int semiPasso = 1; 
        public static string currentFile;
        public static int currentRow;

        public static void Iniciar(string nome)
        {
            FunctionalTestCase.imgsQtd = 0;
            FunctionalTestCase.passo = 0; //Inicial passo =1
            FunctionalTestCase.semiPasso = 1;
            FunctionalTestCase.currentFile = nome;
            FunctionalTestCase.currentRow = 12;    //Initial Row = 12
            XLSUtils.InitializeWorkbook();
            XLSUtils.WriteToFile(currentFile);
            ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
            sheet1.IsSelected = true;
            //Resetar informacoes do XLS Utils tb
            //Iniciar Arquivo CTF com nome "nome"
            //Arquivo Possui um template específico
            //Caso o CTF já exista, Nova ABA
            //Zerar o Contador de Prints
            //Write the stream data of workbook to the root directory
        }

        public static void InformacoesIniciais(string modulo, string funcao, string preCond, string posCond, string ambiente,string versao, string sac, string responsavel, string data)
        {
             ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
            //módulo
            sheet1.GetRow(4).GetCell(2).SetCellValue(modulo);
            //funcao
            sheet1.GetRow(4).GetCell(6).SetCellValue(funcao);
            //preCond
            sheet1.GetRow(5).GetCell(2).SetCellValue(preCond);
            //posCond
            sheet1.GetRow(5).GetCell(6).SetCellValue(posCond);
            //ambiente
            sheet1.GetRow(6).GetCell(2).SetCellValue(ambiente);
            //versao
            sheet1.GetRow(9).GetCell(0).SetCellValue(versao);
            //sac
            sheet1.GetRow(9).GetCell(1).SetCellValue(sac);
            //responsavel
            sheet1.GetRow(9).GetCell(5).SetCellValue(responsavel);
            //data
            sheet1.GetRow(9).GetCell(7).SetCellValue(DateTime.Today.ToString(@"dd/MM/yyyy"));

            sheet1.ForceFormulaRecalculation = true;
            //informar dados de cabeçalho

            XLSUtils.WriteToFile(currentFile);


        }

        public static void Finalizar()
        {
            //Zerar Informações
            //Caso de Teste Encerrado -> Fecha o Arquivo
        }

        public static void inserirImagem(string imgPath)
        {
            //Wait the page to complete
            //Browser.Wait.Until(wd => Browser.JSexec.ExecuteScript("return document.readyState") == "complete"); 

            ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Telas");
            HSSFPatriarch patriarch = (HSSFPatriarch)sheet1.CreateDrawingPatriarch();
            //create the anchor
            HSSFClientAnchor anchor;

            int larguraImg=2*9;
            int alturaImg = 2*16;
            //anchor = new HSSFClientAnchor(0, (alturaImg * imgsQtd) + 3, larguraImg, (alturaImg * (imgsQtd + 1)) + 3, 0, (alturaImg * imgsQtd) + 3, larguraImg, (alturaImg * (imgsQtd + 1)) + 3);
            anchor = new HSSFClientAnchor(0, 0, larguraImg, alturaImg, 0, alturaImg * imgsQtd, larguraImg, alturaImg * (imgsQtd + 1));
            anchor.Row1 = anchor.Row1 + 3;
            anchor.AnchorType = AnchorType.MoveAndResize;
            //load the picture and get the picture index in the workbook
            HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, XLSUtils.LoadImage(imgPath, XLSUtils.hssfworkbook));
            //Reset the image to the original size.
            //picture.Resize();   //Note: Resize will reset client anchor you set.
            picture.LineStyle = LineStyle.DashDotGel;
            FunctionalTestCase.imgsQtd++;
            sheet1.CreateRow(anchor.Row1 - 2).CreateCell(0).SetCellValue("Tela " + imgsQtd.ToString("00"));

            XLSUtils.WriteToFile(currentFile);

            registrarTela();
        }

        public static void inserirImagemErro(string imgPath)
        {
            //Wait the page to complete
            //Browser.Wait.Until(wd => Browser.JSexec.ExecuteScript("return document.readyState") == "complete"); 

            ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Telas");
            HSSFPatriarch patriarch = (HSSFPatriarch)sheet1.CreateDrawingPatriarch();
            //create the anchor
            HSSFClientAnchor anchor;

            int larguraImg = 2 * 9;
            int alturaImg = 2 * 16;
            //anchor = new HSSFClientAnchor(0, (alturaImg * imgsQtd) + 3, larguraImg, (alturaImg * (imgsQtd + 1)) + 3, 0, (alturaImg * imgsQtd) + 3, larguraImg, (alturaImg * (imgsQtd + 1)) + 3);
            anchor = new HSSFClientAnchor(0, 0, larguraImg, alturaImg, 0, alturaImg * imgsQtd, larguraImg, alturaImg * (imgsQtd + 1));
            anchor.Row1 = anchor.Row1 + 3;
            anchor.AnchorType = AnchorType.MoveAndResize;
            //load the picture and get the picture index in the workbook
            HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, XLSUtils.LoadImage(imgPath, XLSUtils.hssfworkbook));
            //Reset the image to the original size.
            //picture.Resize();   //Note: Resize will reset client anchor you set.
            picture.LineStyle = LineStyle.DashDotGel;
            FunctionalTestCase.imgsQtd++;
            sheet1.CreateRow(anchor.Row1 - 2).CreateCell(0).SetCellValue("TELA DE ERRO");

            XLSUtils.WriteToFile(currentFile);

            registrarTela();
        }

        public static void irParaFuncionalidade(string funcionalidade)
        {
                passo++;
                ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
                sheet1.CreateRow(currentRow).Height = 600 ;
                //Setting Style
                ICellStyle style = XLSUtils.hssfworkbook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Justify;
                style.VerticalAlignment = VerticalAlignment.Center;
                style.BorderBottom = BorderStyle.Thin;
                style.BottomBorderColor = HSSFColor.Blue.Index;
                style.BorderLeft = BorderStyle.Thin;
                style.LeftBorderColor = HSSFColor.Blue.Index;
                style.BorderTop = BorderStyle.Thin;
                style.TopBorderColor = HSSFColor.Blue.Index;
                style.BorderRight = BorderStyle.Thin;
                style.RightBorderColor = HSSFColor.Blue.Index;
                //
                //Setting Font
                IFont font = XLSUtils.hssfworkbook.CreateFont();
                font.Boldweight = (short)FontBoldWeight.Bold;
                font.IsBold = true;
                //
                //Attach Font to Style
                style.SetFont(font);
                //
                //Create Row and Coluns
                for (int i = 0; i < 8; i++)
                {
                    ICell cell = sheet1.GetRow(currentRow).CreateCell(i);
                    cell.CellStyle = style;
                }
            //P/ o Estilo (Mesclar as Célular 5 e 6
            sheet1.AddMergedRegion(new CellRangeAddress(currentRow, currentRow, 5, 6));

            //passo
            sheet1.GetRow(currentRow).GetCell(0).SetCellValue(passo);
            //ator
            sheet1.GetRow(currentRow).GetCell(1).SetCellValue("Usuário");
            //acao
            sheet1.GetRow(currentRow).GetCell(2).SetCellValue("Acessar a funcionalidade: " + funcionalidade);

            currentRow++;
            semiPasso = 1;
            sheet1.ForceFormulaRecalculation = true;

            //informar dados de cabeçalho

            XLSUtils.WriteToFile(currentFile);
        }

        public static void inserirComando(string ator, string acao, string dadoInserido, string resultadoEsperado, string descricao, string resultadoObtido)
        {
                ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
                sheet1.CreateRow(currentRow);
                //Setting Style
                ICellStyle style = XLSUtils.hssfworkbook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Justify;
                style.BorderBottom = BorderStyle.Thin;
                style.BottomBorderColor = HSSFColor.Blue.Index;
                style.BorderLeft = BorderStyle.Thin;
                style.LeftBorderColor = HSSFColor.Blue.Index;
                style.BorderTop = BorderStyle.Thin;
                style.TopBorderColor = HSSFColor.Blue.Index;
                style.BorderRight = BorderStyle.Thin;
                style.RightBorderColor = HSSFColor.Blue.Index;
                //
                //Setting Font
                IFont font = XLSUtils.hssfworkbook.CreateFont();
                //
                //Attach Font to Style
                style.SetFont(font);
                //
                //Create Row and Coluns
                for (int i = 0; i < 8; i++)
                {
                    ICell cell = sheet1.GetRow(currentRow).CreateCell(i);
                    cell.CellStyle = style;
                }
            //P/ o Estilo (Mesclar as Célular 5 e 6
            sheet1.AddMergedRegion(new CellRangeAddress(currentRow, currentRow, 5, 6));
            //passo
            sheet1.GetRow(currentRow).GetCell(0).SetCellValue(passo.ToString() +"."+ semiPasso.ToString());
            //ator
            sheet1.GetRow(currentRow).GetCell(1).SetCellValue(ator);
            //acao
            sheet1.GetRow(currentRow).GetCell(2).SetCellValue(acao);
            //dado inserido
            sheet1.GetRow(currentRow).GetCell(3).SetCellValue(dadoInserido);
            //resultado esperado
            sheet1.GetRow(currentRow).GetCell(4).SetCellValue(resultadoEsperado);
            //descricao
            sheet1.GetRow(currentRow).GetCell(5).SetCellValue(descricao);
            //resultado obtido
            sheet1.GetRow(currentRow).GetCell(7).SetCellValue(resultadoObtido);

            currentRow++;
            semiPasso++;
            sheet1.ForceFormulaRecalculation = true;
            //informar dados de cabeçalho

            XLSUtils.WriteToFile(currentFile);
        }

        public static void registrarErro()
        {
            ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
            sheet1.CreateRow(currentRow);
            //Setting Style
            ICellStyle style = XLSUtils.hssfworkbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Blue.Index;
            style.BorderLeft = BorderStyle.Thin;
            style.LeftBorderColor = HSSFColor.Blue.Index;
            style.BorderTop = BorderStyle.Thin;
            style.TopBorderColor = HSSFColor.Blue.Index;
            style.BorderRight = BorderStyle.Thin;
            style.RightBorderColor = HSSFColor.Blue.Index;


            style.FillForegroundColor = HSSFColor.Red.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //style.FillBackgroundColor = HSSFColor.DarkRed.Index;

            //
            //Setting Font
            IFont font = XLSUtils.hssfworkbook.CreateFont();
            //
            //Attach Font to Style
            style.SetFont(font);
            //
            //Create Row and Coluns
            for (int i = 0; i < 8; i++)
            {
                ICell cell = sheet1.GetRow(currentRow).CreateCell(i);
                cell.CellStyle = style; 
            }
            //P/ o Estilo (Mesclar as Célular 5 e 6
            sheet1.AddMergedRegion(new CellRangeAddress(currentRow, currentRow, 0, 7));
            
            
           //resultado obtido
            sheet1.GetRow(currentRow).GetCell(0).SetCellValue("ERRO");
            sheet1.GetRow(6).GetCell(6).CellStyle = style;
            sheet1.GetRow(6).GetCell(6).SetCellValue("Erro");

            sheet1.ForceFormulaRecalculation = true;
            //informar dados de cabeçalho

            XLSUtils.WriteToFile(currentFile);
        }

        public static void registrarSucesso()
        {
            ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
            sheet1.CreateRow(currentRow);
            //Setting Style
            ICellStyle style = XLSUtils.hssfworkbook.CreateCellStyle();

            style.Alignment = HorizontalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Blue.Index;
            style.BorderLeft = BorderStyle.Thin;
            style.LeftBorderColor = HSSFColor.Blue.Index;
            style.BorderTop = BorderStyle.Thin;
            style.TopBorderColor = HSSFColor.Blue.Index;
            style.BorderRight = BorderStyle.Thin;
            style.RightBorderColor = HSSFColor.Blue.Index;


            style.FillForegroundColor = HSSFColor.Green.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //style.FillBackgroundColor = HSSFColor.Green.Index;

            //
            //Setting Font
            IFont font = XLSUtils.hssfworkbook.CreateFont();
            //
            //Attach Font to Style
            style.SetFont(font);
            //
            //Create Row and Coluns
            for (int i = 0; i < 8; i++)
            {
                ICell cell = sheet1.GetRow(currentRow).CreateCell(i);
                cell.CellStyle = style;
            }
            //P/ o Estilo (Mesclar as Célular 5 e 6
            sheet1.AddMergedRegion(new CellRangeAddress(currentRow, currentRow, 0, 7));


            //resultado obtido
            sheet1.GetRow(currentRow).GetCell(0).SetCellValue("SUCESSO");
            sheet1.GetRow(6).GetCell(6).CellStyle = style;
            sheet1.GetRow(6).GetCell(6).SetCellValue("Aprovado");

            sheet1.ForceFormulaRecalculation = true;
            //informar dados de cabeçalho

            XLSUtils.WriteToFile(currentFile);
        }

        public static void registrarTela()
        {
            ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
            sheet1.CreateRow(currentRow);
            //Setting Style
            ICellStyle style = XLSUtils.hssfworkbook.CreateCellStyle();

            style.Alignment = HorizontalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Blue.Index;
            style.BorderLeft = BorderStyle.Thin;
            style.LeftBorderColor = HSSFColor.Blue.Index;
            style.BorderTop = BorderStyle.Thin;
            style.TopBorderColor = HSSFColor.Blue.Index;
            style.BorderRight = BorderStyle.Thin;
            style.RightBorderColor = HSSFColor.Blue.Index;


            style.FillForegroundColor = HSSFColor.Yellow.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //style.FillBackgroundColor = HSSFColor.Green.Index;

            //
            //Setting Font
            IFont font = XLSUtils.hssfworkbook.CreateFont();
            //
            //Attach Font to Style
            style.SetFont(font);
            //
            //Create Row and Coluns
            for (int i = 0; i < 8; i++)
            {
                ICell cell = sheet1.GetRow(currentRow).CreateCell(i);
                cell.CellStyle = style;
            }
            //P/ o Estilo (Mesclar as Célular 5 e 6
            sheet1.AddMergedRegion(new CellRangeAddress(currentRow, currentRow, 0, 7));


            //resultado obtido
            sheet1.GetRow(currentRow).GetCell(0).SetCellValue("Tela " + FunctionalTestCase.imgsQtd.ToString("00"));
            //sheet1.GetRow(6).GetCell(6).CellStyle = style;
            //sheet1.GetRow(6).GetCell(6).SetCellValue("Aprovado");

            sheet1.ForceFormulaRecalculation = true;
            //informar dados de cabeçalho
            currentRow++;

            XLSUtils.WriteToFile(currentFile);
        }

    }
}
