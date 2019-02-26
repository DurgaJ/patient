using RBA_Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBA_Exceptions;

namespace RBA_DataAccessLayer
{
    public class ProductDal
    {
        static string conStr = string.Empty;
        SqlConnection con = null;
        SqlCommand cmd = null;
        static ProductDal()
        {
            conStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        }
        public ProductDal()
        {
            con = new SqlConnection(conStr);
        }
        //Display
        public DataTable DisplayProductDal()
        {
            DataTable dt = null;


            try
            {

                // con = new SqlConnection();
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
                cmd = new SqlCommand();
                cmd.CommandText = "durga_168233.uspDisplayproduct";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dt = new DataTable();
                    dt.Load(dr);
                }
            }

            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return dt;
        }
        //Add product
        public int AddProductDal(Entity pboj)
        {
            int pid = 0;
            try
            {
                //con = new SqlConnection();
                //con.ConnectionString = conStr; 
                cmd = new SqlCommand();
                cmd.CommandText = "durga_168233.uspAddProduct";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@pId", SqlDbType.Int);
                cmd.Parameters["@pId"].Direction = ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@pName", pboj.ProductName);
                cmd.Parameters.AddWithValue("@descp", pboj.Description);
                cmd.Parameters.AddWithValue("@up", pboj.UnitPrice);
               

                con.Open();
                int noOfRowsAffected = cmd.ExecuteNonQuery();
                pid = int.Parse(cmd.Parameters["@pId"].Value.ToString());
            }
            catch (ProductException) { throw; }
            catch (SqlException)
            {
                throw;
            }
            catch (SystemException)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return pid;
        }
    }
}

