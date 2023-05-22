using OpenSky.Models;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OpenSky.Clases
{
    public class PaymentsBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        public async Task<ExecutionStatus> insert_data_payments(os_payments dto)
        {
            var executionStatus = new ExecutionStatus();
            try
            {
                dbCrmContext.os_payments.Add(dto);
                await dbCrmContext.SaveChangesAsync();

                executionStatus.status = true;
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return executionStatus;
        }

        public async Task<List<os_payments>> get_payment_invoices_id(Guid invoicesHeaderId)
        {
            return await dbCrmContext.os_payments.Where(u => u.InvoiceHeaderId == invoicesHeaderId).ToListAsync();
        }
    }
}