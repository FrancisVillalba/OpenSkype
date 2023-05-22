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
    public class TimbradoBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        OpenSkySecurityEntities dbSecurityContext = new OpenSkySecurityEntities();
        public async Task<os_timbrado_data> get_timbrado_branch_id (Guid branchCompanyId)
        {
            return await dbCrmContext.os_timbrado_data.Where(u => u.BranchCompanyId == branchCompanyId).SingleOrDefaultAsync();
        }
    }
}