using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenSky.Models.Dtos
{
    public class PaymentsRequestDto
    {
        public Guid Id { get; set; }
        public Guid InvoiceHeaderId { get; set; }
        public string PaymentTypeId { get; set; }
        public double AmountPaid { get; set; }
        public double Change { get; set; }
        public string VoucherNumber { get; set; }
    }
}