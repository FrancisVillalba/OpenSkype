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
    public class BranchBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        OpenSkySecurityEntities dbSecurityContext = new OpenSkySecurityEntities();
        public async Task<os_branch_company> get_id (Guid Id)
        {
            return await dbSecurityContext.os_branch_company.Where(u => u.Id == Id).SingleOrDefaultAsync();
        }
    }
}