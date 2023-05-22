using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class SalesListDto
    {
        public string Id { get; set; }
        public string Process_number { get; set; }
        public string Invoices_number { get; set; }
        public string Timbrado { get; set; }
        public string DateInvoices { get; set; }
        public string AmountInvoices { get; set; }
        public string DateCreated { get; set; }
        public string Customer { get; set; }
        public string Branch { get; set; }
        public string User { get; set; }
        public string Status { get; set; }
    }

    public class InvoicesRequestDto
    {
        public Guid CustomerId { get; set; }
        public string InvoicesDate { get; set; }
        public Guid SalesConditionsId { get; set; } 
        public string InvoicesCreated { get; set; }
        public Guid InvoicesHeaderId { get; set; }
        public string CurrencyTypeId { get; set; }
    }

    public class InvoicesDetailRequestDto
    {   
        public Guid ArticlesId { get; set; }
        public int ArticlesAmount { get; set; }
        public string ArticlesPrice { get; set; }
        public Guid InvoicesHeaderId { get; set; }
        public string CurrencyTypeId { get; set; }
    }

    public class InvoicesDetailTableRequestDto
    {
        public Guid InvoicesHeaderId { get; set; }
        public string TotalInvoiceAmount { get; set; }
        public string CurrencyTypeId { get; set; }
    }

    public class SalesDetailListDto
    {
        public string Item { get; set; }
        public string Article { get; set; }
        public string AmountArticles { get; set; }
        public string PriceUnit { get; set; }
        public string Impuesto { get; set; }
        public string TotalPrice { get; set; } 
    }

    public class DeleteDetailInvoicesRequest
    { 
        public int ItemDetail { get; set; } 
        public Guid InvoicesHeaderId { get; set; } 
    }

    public class InvoicesUpdateRequestDto
    {
        public Guid InvoicesHeaderId { get; set; }
        public string ProcessNumber { get; set; }
        public string InvoicesNumber { get; set; }
        public string DateInvoices { get; set; }
        public string SalesConditionId { get; set; }
        public string CustomerId { get; set; }
        public string CurrencyTypeId { get; set; }
        public string TotalInvoiceAmount { get; set; } 
    }  
}