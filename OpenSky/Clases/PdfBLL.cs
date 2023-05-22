using iTextSharp.text;
using iTextSharp.text.pdf;
using OpenSky.Models;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Clases
{
    public class PdfBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        InvoicesBLL invoices = new InvoicesBLL();
        NumberToLetterBLL letter = new NumberToLetterBLL();
        ThousandsSeparatorBLL thousandsSeparator = new ThousandsSeparatorBLL();

        public async Task<byte[]> pdfInvoices(string typeDocument, Guid invoiceHeaderId)
        {
            var dtoHeader = invoices.get_invoices_id(invoiceHeaderId);
            var dtoDetail = invoices.get_invoices_detail_id(invoiceHeaderId);



            MemoryStream ms = new MemoryStream();
            Document document = new Document();
            document.SetPageSize(PageSize.A4.Rotate());
            document.SetMargins(15, 15, 15, 15);
            PdfWriter pw = PdfWriter.GetInstance(document, ms); 

            //document.Open();

            BaseFont _titulo = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font titulo = new iTextSharp.text.Font(_titulo, 8f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            BaseFont _h0_negrita = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font h0_negrita = new iTextSharp.text.Font(_h0_negrita, 7f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            BaseFont _h1_negrita = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font h1_negrita = new iTextSharp.text.Font(_h1_negrita, 8f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            BaseFont _h2_negrita = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font h2_negrita = new iTextSharp.text.Font(_h2_negrita, 9f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            BaseFont _h4_negrita = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font h4_negrita = new iTextSharp.text.Font(_h4_negrita, 10f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));

            BaseFont _h1_normal = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font h1_normal = new iTextSharp.text.Font(_h1_normal, 7f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            BaseFont _h2_normal = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font h2_normal = new iTextSharp.text.Font(_h2_normal, 8f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            BaseFont _h4_normal = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font h4_normal = new iTextSharp.text.Font(_h4_normal, 10f, iTextSharp.text.Font.NORMAL, new BaseColor(0, 0, 0));

            BaseFont _letraGrande = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1250, true);
            iTextSharp.text.Font letraGrande = new iTextSharp.text.Font(_letraGrande, 18f, iTextSharp.text.Font.BOLD, new BaseColor(0, 0, 0));



            // Create a new PdfWriter object, specifying the output stream
            var output = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, output);

            document.Open();
            foreach (DataRow dto in dtoHeader.Rows)
            {
                try
                { 
                    #region 001 
                    document.Add(Chunk.NEWLINE);

                    var tblPrincipal = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal01 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal02 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal03 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal04 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal05 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal07 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal08 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal09 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal10 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };
                    var tblPrincipal11 = new PdfPTable(new float[] { 49f, 2f, 49f }) { WidthPercentage = 100 };

                    var tblHeader = new PdfPTable(new float[] { 65f, 33f }) { WidthPercentage = 100 };


                    var tblBody01 = new PdfPTable(new float[] { 20f, 15f, 65f }) { WidthPercentage = 100 };
                    var tblBody02 = new PdfPTable(new float[] { 12f, 53f, 15f, 20f }) { WidthPercentage = 100 };
                    var tblBody03 = new PdfPTable(new float[] { 13f, 52f, 15f, 20f }) { WidthPercentage = 100 };
                    var tblBody04 = new PdfPTable(new float[] { 15f, 85f }) { WidthPercentage = 100 };
                    var tblBody05 = new PdfPTable(new float[] { 6f, 45f, 10f, 39f }) { WidthPercentage = 100 };
                    var tblBody06 = new PdfPTable(new float[] { 13f, 13f, 13f }) { WidthPercentage = 100 };
                    var tblBody07 = new PdfPTable(new float[] { 6f, 45f, 10f, 13f, 13f, 13f }) { WidthPercentage = 100 };
                    var tblBody08 = new PdfPTable(new float[] { 61f, 13f, 13f, 13f }) { WidthPercentage = 100 };
                    var tblBody09 = new PdfPTable(new float[] { 20f, 67f, 13f }) { WidthPercentage = 100 };
                    var tblBody10 = new PdfPTable(new float[] { 33f, 14f, 8f, 14f, 18f, 14f }) { WidthPercentage = 100 };
                    var tblBody11 = new PdfPTable(new float[] { 100f }) { WidthPercentage = 100 };

                    //Cabecera
                    tblHeader.AddCell(new PdfPCell(new Phrase("Adolfo Villalba & Cía.", letraGrande)) { Border = 0, Rowspan = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase("TIMBRADO N°:" + dto["Timbrado"].ToString(), h1_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase("Inicio Vigencia: " + dto["StartDateValidityTimbrado"].ToString(), h1_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase("Fin Vigencia: " + dto["EndDateValidityTimbrado"].ToString(), h1_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase("FACTURA", titulo)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase("PULIDO CLASIFICADOS DE DUPLEX DE PISOS DE PARQUET-COLOCACIÓN Y REPARACIÓN", h1_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase(dto["InvoicesNumber"].ToString(), h4_negrita)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase("Av. Mariscal López & Av Gral M Santos - ASUNCIÓN", h1_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase("RUC : 14654654-6", h1_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase(" ", h1_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblHeader.AddCell(new PdfPCell(new Phrase(dto["ProcessNumber"].ToString(), h1_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });


                    tblPrincipal.AddCell(new PdfPCell(tblHeader));
                    tblPrincipal.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal.AddCell(new PdfPCell(tblHeader));

                    document.Add(tblPrincipal);
                    ////Cuerpo
                    tblBody01.AddCell(new PdfPCell(new Phrase(" FECHA EMISION: ", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tblBody01.AddCell(new PdfPCell(new Phrase(dto["DateInvoices"].ToString(), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    string ventaContado = "";
                    string ventaCredito = "";
                    if (dto["ConditionSales"].ToString() == "Contado")
                    {
                        ventaContado = "X";
                    }
                    else
                    {
                        ventaCredito = "X";
                    }

                    tblBody01.AddCell(new PdfPCell(new Phrase(" CONDICION DE VENTA: CONTADO    " + ventaContado + "    CREDITO    " + ventaCredito + "    ", h1_negrita)) { Border = 4, HorizontalAlignment = Element.ALIGN_LEFT });

                    tblPrincipal01.AddCell(new PdfPCell(tblBody01));
                    tblPrincipal01.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal01.AddCell(new PdfPCell(tblBody01));

                    document.Add(tblPrincipal01);

                    ////Cuerpo
                    tblBody02.AddCell(new PdfPCell(new Phrase(" NOMBRE: ", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tblBody02.AddCell(new PdfPCell(new Phrase(dto["CustomerName"].ToString(), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tblBody02.AddCell(new PdfPCell(new Phrase("RUC/CI:", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tblBody02.AddCell(new PdfPCell(new Phrase(dto["DocumentNumber"].ToString(), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    tblPrincipal02.AddCell(new PdfPCell(tblBody02));
                    tblPrincipal02.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal02.AddCell(new PdfPCell(tblBody02));

                    document.Add(tblPrincipal02);

                    ////Cuerpo
                    tblBody03.AddCell(new PdfPCell(new Phrase(" CIUDAD: ", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody03.AddCell(new PdfPCell(new Phrase(dto["CityName"].ToString(), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tblBody03.AddCell(new PdfPCell(new Phrase("TEL:", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tblBody03.AddCell(new PdfPCell(new Phrase(dto["PhoneNumber"].ToString(), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    tblPrincipal03.AddCell(new PdfPCell(tblBody03));
                    tblPrincipal03.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal03.AddCell(new PdfPCell(tblBody03));

                    document.Add(tblPrincipal03);

                    ////Cuerpo
                    tblBody04.AddCell(new PdfPCell(new Phrase(" DIRECCION: ", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody04.AddCell(new PdfPCell(new Phrase(dto["Address"].ToString(), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    tblPrincipal04.AddCell(new PdfPCell(tblBody04));
                    tblPrincipal04.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal04.AddCell(new PdfPCell(tblBody04));

                    document.Add(tblPrincipal04);


                    ////Cuerpo 
                    tblBody06.AddCell(new PdfPCell(new Phrase("EXENTAS", h0_negrita)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tblBody06.AddCell(new PdfPCell(new Phrase("5%", h0_negrita)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tblBody06.AddCell(new PdfPCell(new Phrase("10%", h0_negrita)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    tblBody05.AddCell(new PdfPCell(new Phrase("CANT", h0_negrita)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER, });
                    tblBody05.AddCell(new PdfPCell(new Phrase("DESCRIPCION", h0_negrita)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblBody05.AddCell(new PdfPCell(new Phrase("PRECIO UNITARIO", h0_negrita)) { Rowspan = 2, HorizontalAlignment = Element.ALIGN_CENTER });
                    tblBody05.AddCell(new PdfPCell(new Phrase("VALOR DE VENTA I.V.A. INCLUIDO", h0_negrita)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tblBody05.AddCell(new PdfPCell(tblBody06) { HorizontalAlignment = Element.ALIGN_CENTER });


                    tblPrincipal05.AddCell(new PdfPCell(tblBody05));
                    tblPrincipal05.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal05.AddCell(new PdfPCell(tblBody05));

                    document.Add(tblPrincipal05);



                    //PdfContentByte pcb = pw.DirectContentUnder;
                    //Image image = Image.GetInstance("C:/MIOS/OpenSky/OpenSky/Content/images/imagena.png");
                    //image.SetAbsolutePosition(100f, 100f);
                    //PdfGState pdfG = new PdfGState();
                    //pdfG.FillOpacity = 0.3f;
                    //pcb.SetGState(pdfG);
                    //pcb.AddImage(image);
                    //pcb.addt


                    //tblPrincipal11.AddCell(pcb.AddImage(image));
                    //tblPrincipal11.AddCell(new PdfPCell() { Border = 0 });
                    //tblPrincipal11.AddCell(new PdfPCell(new Phrase("DUPLICADO", h4_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    //document.Add(tblPrincipal11); 

                    // CurrencyTypeId
                    //////Cuerpo    
                    foreach (DataRow dtoDetails in dtoDetail.Rows)
                    {
                        tblBody07.AddCell(new PdfPCell(new Phrase(dtoDetails["AmountArticles"].ToString() == "" ? " " :  thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dtoDetails["AmountArticles"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_CENTER });
                        tblBody07.AddCell(new PdfPCell(new Phrase(dtoDetails["NumberArticles"].ToString(), h2_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_LEFT });
                        tblBody07.AddCell(new PdfPCell(new Phrase(dtoDetails["PriceUnit"].ToString() == "" ? " " : thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dtoDetails["PriceUnit"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        tblBody07.AddCell(new PdfPCell(new Phrase(dtoDetails["Exenta"].ToString() == "" ? " " : thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dtoDetails["Exenta"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        tblBody07.AddCell(new PdfPCell(new Phrase(dtoDetails["Five_percent"].ToString() == "" ? " " : thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dtoDetails["Five_percent"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                        tblBody07.AddCell(new PdfPCell(new Phrase(dtoDetails["Ten_percent"].ToString() == "" ? " " : thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dtoDetails["Ten_percent"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 4, HorizontalAlignment = Element.ALIGN_CENTER });
                    }

                    tblPrincipal07.AddCell(new PdfPCell(tblBody07));
                    tblPrincipal07.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal07.AddCell(new PdfPCell(tblBody07));
                    document.Add(tblPrincipal07);

                    ////Cuerpo
                    tblBody08.AddCell(new PdfPCell(new Phrase(" SUBTOTALES: ", h1_negrita)) { HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody08.AddCell(new PdfPCell(new Phrase(dto["Totalexenta"].ToString() == "" ? " " : thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dto["Totalexenta"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tblBody08.AddCell(new PdfPCell(new Phrase(dto["Total_five"].ToString() == "" ? " " : thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dto["Total_five"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tblBody08.AddCell(new PdfPCell(new Phrase(dto["Total_ten"].ToString() == "" ? " " : thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dto["Total_ten"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    tblPrincipal08.AddCell(new PdfPCell(tblBody08));
                    tblPrincipal08.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal08.AddCell(new PdfPCell(tblBody08));

                    document.Add(tblPrincipal08);

                    ////Cuerpo
                    tblBody09.AddCell(new PdfPCell(new Phrase(" TOTAL A PAGAR:", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody09.AddCell(new PdfPCell(new Phrase(letter.convert_number_to_letter(dto["TotalInvoiceAmount"].ToString(), true, dto["CurrencyTypeId"].ToString()), h1_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, }); ;
                    tblBody09.AddCell(new PdfPCell(new Phrase(dto["TotalInvoiceAmount"].ToString() == "" ? " " :  thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dto["TotalInvoiceAmount"].ToString()), dto["CurrencyTypeId"].ToString()), h2_negrita)) { HorizontalAlignment = Element.ALIGN_CENTER });

                    tblPrincipal09.AddCell(new PdfPCell(tblBody09));
                    tblPrincipal09.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal09.AddCell(new PdfPCell(tblBody09));

                    document.Add(tblPrincipal09);

                    ////Cuerpo
                    tblBody10.AddCell(new PdfPCell(new Phrase(" LIQUIDACIÓN DE IVA: (5%):", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody10.AddCell(new PdfPCell(new Phrase(dto["Five_percent_total"].ToString() == "" ? " " :  thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dto["Five_percent_total"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody10.AddCell(new PdfPCell(new Phrase("(10%)", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });
                    tblBody10.AddCell(new PdfPCell(new Phrase(dto["Ten_percent_total"].ToString() == "" ? " " :  thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dto["Ten_percent_total"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody10.AddCell(new PdfPCell(new Phrase("TOTAL IVA:", h1_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT, });
                    tblBody10.AddCell(new PdfPCell(new Phrase(dto["Total_percent_iva"].ToString() == "" ? " " :  thousandsSeparator.ThousandsSeparator(Convert.ToDouble(dto["Total_percent_iva"].ToString()), dto["CurrencyTypeId"].ToString()), h2_normal)) { Border = 0, HorizontalAlignment = Element.ALIGN_LEFT });

                    tblPrincipal10.AddCell(new PdfPCell(tblBody10));
                    tblPrincipal10.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal10.AddCell(new PdfPCell(tblBody10));

                    //PdfContentByte pcb = pw.DirectContentUnder;
                    //Image image = Image.GetInstance("C:/MIOS/OpenSky/OpenSky/Content/images/imagena.png");
                    //image.SetAbsolutePosition(100f, 100f);
                    //PdfGState pdfG = new PdfGState();
                    //pdfG.FillOpacity = 0.3f;
                    //pcb.SetGState(pdfG);
                    //pcb.AddImage(image);
                    //pcb.addt 
                    document.Add(tblPrincipal10);

                    tblPrincipal11.AddCell(new PdfPCell(new Phrase("ORIGINAL", h4_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });
                    tblPrincipal11.AddCell(new PdfPCell() { Border = 0 });
                    tblPrincipal11.AddCell(new PdfPCell(new Phrase("DUPLICADO", h4_negrita)) { Border = 0, HorizontalAlignment = Element.ALIGN_RIGHT });

                    document.Add(tblPrincipal11);
                    #endregion
                }
                catch (Exception ex)
                {
                }
            }
            document.Close();

            byte[] bytesStream = ms.ToArray();

            return bytesStream;
        }

    }
}