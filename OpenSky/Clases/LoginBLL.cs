using OpenSky.Models;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OpenSky.Clases
{
    public class LoginBLL
    {
        public string GetMD5Hash(string password)
        {
            string resultado = string.Empty;
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            resultado = sb.ToString();
            return resultado;
        }
        public ExecutionStatusLogin validate_pass_and_user(string user, string pass)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
            var dto = new ExecutionStatusLogin();
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_validate_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@password", pass);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(rdr);
                con.Close();

                foreach (DataRow rowp in dt.Rows)
                {
                    dto.status = rowp["status"].ToString() == "-1" ? 0 : 1;
                    dto.message = rowp["message"].ToString();
                }

            }
            catch (Exception ex)
            {
                con.Close();
            }

            return dto;
        }
        public async Task<User> get_user_data(string user, string pass)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
            var dto = new User();
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_get_user_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@password", pass);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(rdr);
                con.Close();

                foreach (DataRow rowp in dt.Rows)
                {
                    dto.UserId = Guid.Parse(rowp["UserId"].ToString());
                    dto.FirstName = rowp["FirstName"].ToString();
                    dto.SecondName = rowp["SecondName"].ToString();
                    dto.FirstSurname = rowp["FirstLastName"].ToString();
                    dto.SecondSurname = rowp["SecondLastName"].ToString();
                    dto.ChargesId = Guid.Parse(rowp["ChargesId"].ToString());
                    dto.ProfileId = Guid.Parse(rowp["ProfileId"].ToString());
                    dto.BranchCompanyId = Guid.Parse(rowp["BranchId"].ToString());
                } 
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return dto;
        }

        public int check_reset_password(string user)
        {
            int count = 0;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_check_reset_password", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user", user);
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

            }

            return count;
        }
        public async Task<ExecutionStatus> update_password_reset(string username, string newPassword)
        {
            var executionStatus = new ExecutionStatus();

            try
            {

                string connectionString = ConfigurationManager.ConnectionStrings["conStringSecurity"].ConnectionString;
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_update_password_reset", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@passwordNew", newPassword);
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
    }
}