using OpenSky.Models;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Clases
{
    public class CustomersBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        public async Task<List<SelectListItem>> list_custommer()
        {
            var list = new List<SelectListItem>();
            list = await (from d in dbCrmContext.os_customers
                    select new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.FirstName + " " + d.SecondName + " " + d.FirstLastName + " " + d.SecondLastName + " - " + d.DocumentNumber
                    }).ToListAsync();

            return list;
        } 
    }
}