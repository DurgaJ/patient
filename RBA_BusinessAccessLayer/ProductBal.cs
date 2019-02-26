using RBA_DataAccessLayer;
using RBA_Entities;
using RBA_Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RBA_BusinessAccessLayer
{
    public class Class1
    {
        StringBuilder sb = new StringBuilder();
        private bool ValidateProduct(Entity pro)
        {


            bool IsValidProduct = true;

            if (!Regex.Match(pro.ProductName, @"^[A-Z][a-z]*$").Success)
            {
                IsValidProduct = false;
                sb.Append(Environment.NewLine + "Product Name should contain Characters and must begin with a capital letter!");
            }
            if (!(Regex.IsMatch(pro.UnitPrice.ToString(), @"[0-9]$")))
            {
                IsValidProduct = false;
                sb.Append(Environment.NewLine + "Product Price must contain digits only");
            }
            if (pro.UnitPrice.ToString().Equals(string.Empty))
            {
                IsValidProduct = false;
                sb.Append("Product Price cannot be blank " + Environment.NewLine);

            }

            return IsValidProduct;
        }
        //Display product
        public DataTable DisplayProductBal()
        {
            try
            {
                ProductDal sd = new ProductDal();
                DataTable dtProduct = sd.DisplayProductDal();
                if (dtProduct.Rows.Count <= 0)
                {
                    throw new ProductException("No Student Available");
                }
                return dtProduct;
            }
            catch (ProductException se)
            { throw se; }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception e)
            { throw e; }
        }
        //Add product
        public int AddProductBAL(Entity pobj)
        {
            try
            {
                int pid = 0;
                ProductDal pd = new ProductDal();
                if (ValidateProduct(pobj))
                {
                    pid = pd.AddProductDal(pobj);
                }
                else
                    throw new ProductException(sb.ToString());

                return pid;
            }
            catch (ProductException)
            {
                throw;
            }
        }
    }
}
