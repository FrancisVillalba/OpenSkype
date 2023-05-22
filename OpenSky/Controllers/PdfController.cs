using OpenSky.Clases;
using OpenSky.Models;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Controllers
{
    public class PdfController : Controller
    {
        PdfBLL pdf = new PdfBLL();

        [HttpGet]
        public ActionResult GeneratePdf(string processNumber)
        {
            try
            {
                var bytesStream = pdf.pdfInvoices("", Guid.Parse(processNumber));
                return File(bytesStream.Result, "application/pdf");
            }
            catch (Exception ex)
            {
                return null;
            } 
        }

    }
}