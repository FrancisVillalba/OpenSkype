using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Controllers
{
    public class MainBoardController : Controller
    {
        // GET: Principal
        public async Task<ActionResult> MainBoard()
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            } 
            return View();
        } 
    }
}