using OpenSky.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Clases
{
    public class PaymentTypeBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        public async Task<List<SelectListItem>> list_payment_type()
        {
            var list = new List<SelectListItem>();
            list = await (from d in dbCrmContext.os_payment_types
            select new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            }).ToListAsync();

            return list;
        }
    }
}