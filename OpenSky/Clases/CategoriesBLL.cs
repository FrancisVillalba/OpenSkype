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
    public class CategoriesBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        public DataTable get_articles_categories()
        {
            DataTable dt = new DataTable();
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_article_categories", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@nomInterfaz", nomInterfaz);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public DataTable get_articles_categories_filter_id(Guid id)
        {
            DataTable dt = new DataTable();
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_filter_id_article_categories", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dt.Load(rdr);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public async Task<ExecutionStatus> insert_articles_categories(CategoriesDto dto)
        {
            var executionStatus = new ExecutionStatus();

            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_insert_articles_categories", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", dto.Name);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    executionStatus.status = true;
                }
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }
            return executionStatus;
        }
        public async Task<ExecutionStatus> update_status_articles_categories(CategoriesDto dto)
        {
            var executionStatus = new ExecutionStatus();

            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_update_status_article_catergorie", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", dto.Status);
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    executionStatus.status = true;
                }
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }
            return executionStatus;
        }
        public async Task<ExecutionStatus> update_datos_articles_categories(CategoriesDto dto)
        {
            var executionStatus = new ExecutionStatus();

            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_update_datos_article_catergorie", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", dto.Name);
                    cmd.Parameters.AddWithValue("@Id", dto.Id);
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                    executionStatus.status = true;
                }
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }
            return executionStatus;
        }
        public async Task<List<SelectListItem>> list_categories()
        {
            var list = new List<SelectListItem>();
            list = await (from d in dbCrmContext.os_articles_categories
                    select new SelectListItem
                    {
                        Text = d.Name,
                        Value = d.Id.ToString()
                    }).ToListAsync();

            return list; 
        }
    }
}