using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NUnit.Framework;
using System;
using System.IO;
using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;

namespace Testes_TopDown
{
    public class Testes_CTF_Utils
    {
        //[Test]
        //[Description("Testando App Excel - Criando um Vazio")]
        //public void escreverCTF()
        //{

        //    HSSFWorkbook hssfworkbook = new HSSFWorkbook();

        //    ////create a entry of DocumentSummaryInformation
        //    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        //    dsi.Company = "NPOI Team";
        //    hssfworkbook.DocumentSummaryInformation = dsi;

        //    ////create a entry of SummaryInformation
        //    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        //    si.Subject = "NPOI SDK Example";
        //    hssfworkbook.SummaryInformation = si;

        //    //here, we must insert at least one sheet to the workbook. otherwise, Excel will say 'data lost in file'
        //    //So we insert three sheet just like what Excel does
        //    hssfworkbook.CreateSheet("Sheet1");
        //    hssfworkbook.CreateSheet("Sheet2");
        //    hssfworkbook.CreateSheet("Sheet3");
        //    hssfworkbook.CreateSheet("Sheet4");

        //    ((HSSFSheet)hssfworkbook.GetSheetAt(0)).AlternativeFormula = false;
        //    ((HSSFSheet)hssfworkbook.GetSheetAt(0)).AlternativeExpression = false;

        //    //Write the stream data of workbook to the root directory
        //    FileStream file = new FileStream(@"D:\test.xls", FileMode.Create);
        //    hssfworkbook.Write(file);
        //    file.Close();
        //}

        //[Test]
        //[Description("Testando App Excel - Criando um CTF do Template")]
        //public void escreverCTFDoTemplate()
        //{

        //    XLSUtils.InitializeWorkbook();

        //    ISheet sheet1 = XLSUtils.hssfworkbook.GetSheet("Test Cases");
        //    ISheet sheet2 = XLSUtils.hssfworkbook.GetSheet("Telas");
        //    sheet1.ForceFormulaRecalculation = true;
        //    sheet2.ForceFormulaRecalculation = true;

        //    XLSUtils.WriteToFile();
        //}

        //[Test]
        //[Description("Cria dropdown cell no XLS")]
        //public void createDropDownInXls()
        //{
        //    XLSUtils.InitializeWorkbook();

        //    ISheet sheet1 = XLSUtils.hssfworkbook.CreateSheet("Sheet1");
        //    ISheet sheet2 = XLSUtils.hssfworkbook.CreateSheet("Sheet2");
        //    //create three items in Sheet2
        //    IRow row0 = sheet2.CreateRow(0);
        //    ICell cell0 = row0.CreateCell(4);
        //    cell0.SetCellValue("Product1");

        //    row0 = sheet2.CreateRow(1);
        //    cell0 = row0.CreateCell(4);
        //    cell0.SetCellValue("Product2");

        //    row0 = sheet2.CreateRow(2);
        //    cell0 = row0.CreateCell(4);
        //    cell0.SetCellValue("Product3");


        //    CellRangeAddressList rangeList = new CellRangeAddressList();

        //    //add the data validation to the first column (1-100 rows) 
        //    rangeList.AddCellRangeAddress(new CellRangeAddress(1, 100, 0, 0));
        //    DVConstraint dvconstraint = DVConstraint.CreateFormulaListConstraint("Sheet2!$E1:$E3");
        //    HSSFDataValidation dataValidation = new
        //            HSSFDataValidation(rangeList, dvconstraint);
        //    //add the data validation to sheet1
        //    ((HSSFSheet)sheet1).AddValidationData(dataValidation);

        //    XLSUtils.WriteToFile();
        //}

        //[Test]
        //[Description("Inserir Figuras no XLS")]
        //public void InserirFigurasNoXLS()
        //{
        //    XLSUtils.InitializeWorkbook();
        //    ISheet sheet1 = XLSUtils.hssfworkbook.CreateSheet("PictureSheet");


        //    HSSFPatriarch patriarch = (HSSFPatriarch)sheet1.CreateDrawingPatriarch();
        //    //create the anchor
        //    HSSFClientAnchor anchor;
        //    anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, 0, 2 * 9, 2 * 16);
        //    anchor.AnchorType = AnchorType.MoveAndResize;
        //    //load the picture and get the picture index in the workbook
        //    HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, XLSUtils.LoadImage(@"D:\Peterson\Prints\screenshot.3.jpg", XLSUtils.hssfworkbook));
        //    //Reset the image to the original size.
        //    //picture.Resize();   //Note: Resize will reset client anchor you set.
        //    picture.LineStyle = LineStyle.DashDotGel;

        //    XLSUtils.WriteToFile();
        //}
        //#region TestTemplate
        //[Test]
        //[Description("Descrição do Teste")]
        //[Property("Nome", "Nome do Teste (também nome do arquivo)")]
        //[Property("Módulo", "Autenticação")]
        //[Property("Função", "Login")]
        //[Property("Pré Condição", "")]
        //[Property("Pós Condição", "Usuário Garante Acesso Ao Sistema")]
        //[Property("Ambiente", "Browser:" + "IE10" + "\t" + "Web:" + "10.10.100.147" + "\t" + "BD:" + "Homo_Med")]
        //[Property("Versão", "11")]
        //[Property("SAC", "N/A")]
        //[Property("Responsável", "PETERSON ANDRADE")]
        //public static void testFunction()
        //{


        //    #region Cabeçalho CTF
        //    CTF.Iniciar(TestContext.CurrentContext.Test.Properties.Get("Nome").ToString());
        //    CTF.InformacoesIniciais(
        //        TestContext.CurrentContext.Test.Properties.Get("Módulo").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Função").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Pré Condição").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Pós Condição").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Ambiente").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Versão").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("SAC").ToString(),
        //        TestContext.CurrentContext.Test.Properties.Get("Responsável").ToString(),
        //        DateTime.Today.ToString(@"DD/MM/YYYY"));
        //    //
        //    #endregion

        //    try
        //    {
        //        #region Passos do Caso de Teste

        //        CTF.inserirImagem(@"D:\Peterson\Prints\screenshot.3.jpg");
        //        CTF.inserirImagem(Browser.Print());
        //        CTF.inserirImagem(@"D:\Peterson\Prints\screenshot.3.jpg");
        //        CTF.inserirImagem(Browser.Print());


        //        #endregion

        //        CTF.registrarSucesso();
        //    }
        //    catch (Exception ex)
        //    {
        //        CTF.registrarErro();
        //        throw ex;
        //    }


        //}
        //#endregion
    }


}
