using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class ExecutionStatus
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class ExecutionStatusLogin
    {
        public int status { get; set; }
        public string message { get; set; }
    }

    public class ExecutionStatusInvoices
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Guid InvoicesHeaderId { get; set; }
        public string TotalInvoiceAmount { get; set; }
    }
}