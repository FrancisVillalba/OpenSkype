using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class InvoicesDataPdfDto
    {
        public string Process_number { get; set; }
        public string DateInvoices { get; set; }
        public string ConditionSales { get; set; }
        public string CustomerName { get; set; }
        public string DocumentNumber { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Timbrado { get; set; }
        public string StartDateValidityTimbrado { get; set; }
        public string EndDateValidityTimbrado { get; set; }
        public string InvoicesNumber { get; set; }
        public string Totalexenta { get; set; }
        public string Total_five { get; set; }
        public string Total_ten { get; set; }
        public string Five_percent_total { get; set; }
        public string Ten_percent_total { get; set; }  
        public string TotalInvoiceAmount { get; set; }
        public string CurrencyTypeId { get; set; }
        public string Total_percent_iva { get; set; }
    }

    public class InvoicesDetailDataPdfDto
    {
        public string NumberItems { get; set; }
        public string AmountArticles { get; set; }
        public string NumberArticles { get; set; }
        public string PriceUnit { get; set; }
        public string Exenta { get; set; }
        public string Five_percent { get; set; }
        public string Ten_percent { get; set; } 
    }
}