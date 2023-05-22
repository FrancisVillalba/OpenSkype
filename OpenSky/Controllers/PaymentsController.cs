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
    public class PaymentsController : Controller
    {
        PaymentsBLL payments = new PaymentsBLL(); 
        public async Task<ActionResult> Insert_data_payments(PaymentsRequestDto dto)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var executionStatus = new ExecutionStatus();
            try
            {
                var responseDto = new os_payments
                {
                    Id = Guid.NewGuid(),
                    InvoiceHeaderId = dto.InvoiceHeaderId,
                    PaymentTypeId = dto.PaymentTypeId,
                    AmountPaid = dto.AmountPaid,
                    Change = dto.Change,
                    VoucherNumber = dto.VoucherNumber
                };

                executionStatus = await payments.insert_data_payments(responseDto);
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return Json(executionStatus);
        }
    }
}