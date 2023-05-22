using OpenSky.Models;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Clases
{
    public class InvoicesBLL
    {
        OpenSkyCrmEntities dbCrmContext = new OpenSkyCrmEntities();
        OpenSkySecurityEntities dbSecurityContext = new OpenSkySecurityEntities();

        public DataTable get_invoices_id(Guid Id)
        {
            DataTable dt = new DataTable();
            var list = new List<InvoicesDataPdfDto>();
            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_data_invoices", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@invoiceHeaderId", Id);
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
        public DataTable get_invoices_detail_id(Guid Id)
        {
            DataTable dt = new DataTable();
            var list = new List<InvoicesDetailDataPdfDto>();
            try
            { 
                string connectionString = ConfigurationManager.ConnectionStrings["conStringCrm"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_get_data_detail_invoices", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@invoiceHeaderId", Id);
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
    }
}