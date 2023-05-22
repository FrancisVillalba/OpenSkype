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
    public class SalesBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        OpenSkySecurityEntities dbSecurityContext = new OpenSkySecurityEntities();

        public DataTable get_list_sales(string filter, string data)
        {
            DataTable dt = new DataTable();
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_list_invoices", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@filter", filter);
                    cmd.Parameters.AddWithValue("@data", data);
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
        public int get_max_invoices_id(Guid branchId)
        {
            int count = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_max_invoices_number_range", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@branchCompanyId", branchId);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        count = rdr.GetInt32(0);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

            return count;
        }
        public async Task<os_invoices_header> insert_invoices_header_data(os_invoices_header dto)
        {
            dbCrmContext.os_invoices_header.Add(dto);
            await dbCrmContext.SaveChangesAsync();

            return dto;
        }
        public async Task<os_invoices_detail> insert_invoices_detail_data(os_invoices_detail dto)
        {
            dbCrmContext.os_invoices_detail.Add(dto);
            await dbCrmContext.SaveChangesAsync();

            return dto;
        }
        public async Task<List<SelectListItem>> list_sales_conditions()
        {
            var list = new List<SelectListItem>();
            list = await (from d in dbCrmContext.os_sales_conditions
                          select new SelectListItem
                          {
                              Text = d.Name,
                              Value = d.Id.ToString()
                          }).ToListAsync();

            return list;
        }
        public int get_max_invoices_details_id(Guid invoicesHeaderId)
        {
            int count = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_max_invoices_detail_item_number", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@invoicesHeaderId", invoicesHeaderId);
                    conexion.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        count = rdr.GetInt32(0);
                    }

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

            return count;
        }
        public async Task<double?> get_total_invoices_price(Guid invoicesHeaderId)
        {
            return await dbCrmContext.os_invoices_detail.Where(u => u.InvoicesHeaderId == invoicesHeaderId).Select(u => u.PriceTotal).SumAsync();
        }
        public async Task<List<os_invoices_detail>> get_invoices_details_id(Guid invoicesHeaderId)
        {
            return await dbCrmContext.os_invoices_detail.Where(u => u.InvoicesHeaderId == invoicesHeaderId).ToListAsync();
        }
        public async Task<List<os_invoices_header>> get_invoices_header_id(Guid invoicesHeaderId)
        {
            return await dbCrmContext.os_invoices_header.Where(u => u.Id == invoicesHeaderId).ToListAsync();
        }
        public DataTable get_data_invoices_details_id(Guid invoicesHeaderId)
        {
            DataTable dt = new DataTable();

            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_detail_filter_id", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@invoicesHeaderId", invoicesHeaderId);
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
        public async Task<os_invoices_header> update_total_invoice_amount(Guid invoicesHeaderId, float totalInvoiceAmount)
        {
            try
            {
                var entities = dbCrmContext.os_invoices_header.FirstOrDefault(x => x.Id == invoicesHeaderId);
                entities.TotalInvoiceAmount = totalInvoiceAmount;

                await dbCrmContext.SaveChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                return null;
            }
        } 
        public async Task<os_invoices_detail> delete_detail_invoicet(DeleteDetailInvoicesRequest dto)
        {
            try
            {
                var entities = dbCrmContext.os_invoices_detail.FirstOrDefault(x => x.InvoicesHeaderId == dto.InvoicesHeaderId && x.NumberItems == dto.ItemDetail);

                dbCrmContext.os_invoices_detail.Remove(entities);
                await dbCrmContext.SaveChangesAsync();
                return entities;
            }
            catch (Exception ex)
            {
                return null;
            }
        } 
    }
}