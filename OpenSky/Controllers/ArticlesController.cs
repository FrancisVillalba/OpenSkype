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
    public class ArticlesController : Controller
    {
        // GET: Articulos
        ArticlesBLL articles = new ArticlesBLL();
        CategoriesBLL categories = new CategoriesBLL();
        ThousandsSeparatorBLL thousands = new ThousandsSeparatorBLL();


        TaxesBLL taxes = new TaxesBLL();
        public async Task<ActionResult> Articles()
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var list = articles.get_list_articles();

            return View(list);
        }
        public async Task<ActionResult> Update_status_articles(Guid id, string status)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var dto = new ArticlesDto()
            {
                Id = id,
                Status = status
            };

            var list = articles.update_status_articles(dto);

            return Json(list);
        }
        public async Task<ActionResult> Insert_articles()
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var listCategories = await categories.list_categories();
            ViewBag.listCategories = listCategories;

            var listTaxes = taxes.list_taxes();
            ViewBag.listTaxes = listTaxes;

            return View();
        }
        public async Task<ActionResult> Insert_data_articles(ArticlesResponseDto dto)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var executionStatus = new ExecutionStatus();
            try
            {

                var articlesDto = new os_articles
                {
                    Id = Guid.NewGuid(),
                    CategorieId = dto.categorie_id,
                    InitialAmount = dto.InitialAmount,
                    Name = dto.Name,
                    Price = dto.Price,
                    IvaId = dto.Iva,
                    Status = "A",
                    UserCreated = Guid.Parse(Session["user_ID"].ToString()),
                    DateCreated = DateTime.Now,
                    ReferenceInternal = dto.ReferenceInternal,
                    MinimumAmount = dto.MinimumAmount
                };

                executionStatus = await articles.insert_data_articles(articlesDto);
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return Json(executionStatus);
        }
        public async Task<ActionResult> Update_articles(Guid Id)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var dto = await articles.get_articles_filter_id(Id);

            var listCategories = await categories.list_categories();
            ViewBag.categories = listCategories;

            var listIva = await articles.list_iva();
            ViewBag.iva = listIva;

            return View(dto);
        }
        public async Task<ActionResult> update_datos_articles(ArticlesResponseDto dto)
        {
            var executionStatus = new ExecutionStatus();
            try
            {
                var dtoArticles = new os_articles
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    CategorieId = dto.categorie_id,
                    InitialAmount = dto.InitialAmount,
                    IvaId = dto.Iva,
                    MinimumAmount = dto.MinimumAmount,
                    Price = dto.Price,
                    ReferenceInternal = dto.ReferenceInternal,
                    DateUpdate = DateTime.Now,
                    UserUpdate = Guid.Parse(Session["user_ID"].ToString())
                };

                executionStatus = await articles.update_datos_articles(dtoArticles);
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return Json(executionStatus);
        }
        public async Task<JsonResult> Get_search_by_name(string name, int? cant)
        {
            var listDto = new List<ArticlesListDto>();
            try
            {
                var list = await articles.get_by_name(name);
                foreach (var dto in list)
                {
                    listDto.Add(new ArticlesListDto
                    {
                        Id = dto.Id,
                        Name = dto.Name
                    });
                }
                return Json(listDto, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Get_articles_by_id(Guid Id)
        {
            try
            { 
                var article = await articles.get_by_id(Id);

                var dto = new ArticlesPriceResponseDto()
                {
                    Price =  article.Price.ToString()
                };

                return Json(dto, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}