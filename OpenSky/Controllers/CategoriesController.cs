using OpenSky.Clases;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Articulos
        CategoriesBLL bll = new CategoriesBLL();
        public async Task<ActionResult> Categories()
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var models = new List<CategoriesListDto>();
            DataTable dt = bll.get_articles_categories();

            foreach (DataRow row in dt.Rows)
            {
                models.Add(new CategoriesListDto
                {
                    Id = Guid.Parse(row["id"].ToString()),
                    Name = row["name"].ToString(),
                    Status = row["status"].ToString() == "A" ? true : false
                });
            }

            return View(models);
        }
        public async Task<ActionResult> Insert_categories()
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }
        public async Task<ActionResult> Insert_articles_categories(string name)
        {
            var dto = new CategoriesDto();
            dto.Name = name;
            var executionStatus = await bll.insert_articles_categories(dto);

            return Json(executionStatus);
        }
        public async Task<ActionResult> Update_status_articles_categories(Guid id, string status)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var dto = new CategoriesDto()
            {
                Id = id,
                Status = status
            };

            Session["dto"] = dto;

            var executionStatus = await bll.update_status_articles_categories(dto);

            return Json(executionStatus);
        }
        public async Task<ActionResult> update_datos_articles_categories(string name, Guid id)
        {
            var dto = new CategoriesDto();
            dto.Name = name;
            dto.Id = id;
            var executionStatus = await bll.update_datos_articles_categories(dto);

            return Json(executionStatus);
        }
        public async Task<ActionResult> Update_categories(Guid Id)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var dto = new CategoriesDto();
            DataTable dt = bll.get_articles_categories_filter_id(Id);

            foreach (DataRow row in dt.Rows)
            {
                dto.Id = Guid.Parse(row["id"].ToString());
                dto.Name = row["name"].ToString();
                dto.Status = row["status"].ToString();
            }
            return View(dto);
        } 
    }
}