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
    public class ArticlesBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();

        public async Task<os_articles> get_by_id(Guid id)
        {
            return await dbCrmContext.os_articles.Where(u => u.Id == id).SingleOrDefaultAsync();
        }
        public async Task<List<Articles>> get_list_articles()
        {
            var list = new List<Articles>();
            list = await (from d in dbCrmContext.os_articles
                    join a in dbCrmContext.os_articles_categories on d.CategorieId equals a.Id
                    join i in dbCrmContext.os_iva on d.IvaId equals i.Id
                    select new Articles
                    {
                        Id = d.Id,
                        Name = d.Name,
                        InitialAmount = d.InitialAmount,
                        Iva_Id = i.Id,
                        Iva_name = i.Name,
                        Price = d.Price,
                        ReferenceInternal = d.ReferenceInternal,
                        MinimumAmount = d.MinimumAmount,
                        Categorie = a.Name,
                        Status = d.Status == "A" ? true : false
                    }).ToListAsync();

            return list;
        }
        public async Task<ExecutionStatus> update_status_articles(ArticlesDto dto)
        {
            var executionStatus = new ExecutionStatus();
            try
            {
                var entities = await dbCrmContext.os_articles.SingleOrDefaultAsync(x => x.Id == dto.Id);
                entities.Status = dto.Status;
                dbCrmContext.SaveChanges();

                executionStatus.status = true;
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return executionStatus;
        }
        public async Task<Articles> get_articles_filter_id(Guid id)
        {
            var list = new Articles();
            list = await (from d in dbCrmContext.os_articles
                    join a in dbCrmContext.os_articles_categories on d.CategorieId equals a.Id
                    join i in dbCrmContext.os_iva on d.IvaId equals i.Id
                    where d.Id == id
                    select new Articles
                    {
                        Id = d.Id,
                        Name = d.Name,
                        InitialAmount = d.InitialAmount,
                        Iva_name = i.Name,
                        Iva_Id = i.Id,
                        Price = d.Price,
                        ReferenceInternal = d.ReferenceInternal,
                        MinimumAmount = d.MinimumAmount,
                        Categorie = a.Name,
                        Categorie_id = a.Id,
                        Status = d.Status == "A" ? true : false
                    }).SingleOrDefaultAsync();

            return list;
        }
        public async Task<ExecutionStatus> insert_data_articles(os_articles dto)
        {
            var executionStatus = new ExecutionStatus();
            try
            {
                dbCrmContext.os_articles.Add(dto);
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
        public async Task<List<SelectListItem>> list_iva()
        {
            var list = new List<SelectListItem>();
            list = await (from d in dbCrmContext.os_iva
                    select new SelectListItem
                    {
                        Text = d.Name,
                        Value = d.Id.ToString()
                    }).ToListAsync();

            return list;
        }
        public async Task<ExecutionStatus> update_datos_articles(os_articles dto)
        {
            var executionStatus = new ExecutionStatus();
            try
            {
                var entities = await dbCrmContext.os_articles.SingleOrDefaultAsync(x => x.Id == dto.Id);
                entities.Name = dto.Name;
                entities.CategorieId = dto.CategorieId;
                entities.InitialAmount = dto.InitialAmount;
                entities.IvaId = dto.IvaId;
                entities.MinimumAmount = dto.MinimumAmount;
                entities.Price = dto.Price;
                entities.ReferenceInternal = dto.ReferenceInternal;
                entities.DateUpdate = dto.DateUpdate;
                entities.UserUpdate = dto.UserUpdate;

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
        public async Task<List<SelectListItem>> list_articles()
        {
            var list = new List<SelectListItem>();
            list = await (from d in dbCrmContext.os_articles
                    select new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.ReferenceInternal + " - " + d.Name
                    }).ToListAsync();

            return list;
        }
        public async Task<List<os_articles>> get_by_name(string name)
        { 
            return await dbCrmContext.os_articles.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToListAsync(); 
        }
    }
}