using OpenSky.Clases;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Controllers
{
    public class HomeController : Controller
    {
        LoginBLL login = new LoginBLL();
        public ActionResult Index()
        {
            Session["user_date"] = null;

            return View();
        }
        public async Task<ActionResult> ResetPassword()
        {
            if (Session["username"] == null)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.username = Session["username"].ToString();
            return View();
        }

        public async Task<ActionResult> validate_login(string username, string password)
        {
            Session["username"] = username;
            var executionStatus = new ExecutionStatusLogin();
            
            var passHash = login.GetMD5Hash(password);
            executionStatus = login.validate_pass_and_user(username, passHash);

            if (executionStatus.status == 1)
            {
                var user = await login.get_user_data(username, passHash);
                Session["name_surname"] = user.FirstName + " " + user.FirstSurname;
                Session["user_date"] = user;
                Session["user_ID"] = user.UserId;

                var reset = login.check_reset_password(username);
                if (reset > 0)
                {
                    executionStatus.status = 2;

                    return Json(executionStatus);
                }
            } 

            return Json(executionStatus);
        }

        public async Task<ActionResult> update_password_reset(string username, string new_password)
        {
            if (Session["username"] == null)
            {
                return Redirect("/Home/Index");
            }

            var passHash = login.GetMD5Hash(new_password);
            var reset = login.update_password_reset(username, passHash);
              
            return Json(reset);
        }
    }
}