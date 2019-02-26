using RBA_DataAccessLayer;
using RBA_Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RBA_BusinessAccessLayer;
using RBA_Exceptions;

namespace MainWindow
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
        SqlConnection conObj = new SqlConnection();
        SqlCommand cmdObj;
        SqlParameter parmObj;
        SqlDataReader rdrStudent = null;
        DataTable dtStudent = new DataTable();

        public AddWindow()
        {
            InitializeComponent();
        }

        private void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Entity p = new Entity
            {
                ProductName = txt_Pname.Text,
                Description= txt_Desc.Text,
                UnitPrice = decimal.Parse(txt_Price.Text)
            };

            Class1 pb = new Class1 ();
            int pid = pb.AddProductBAL(p);
            MessageBox.Show(string.Format("New Product Added.\nProduct Id: {0}", pid),
                "Product Management System");
        }
            catch (ProductException ex)
            {
                MessageBox.Show(ex.Message, "Product Management System");
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Product Management System");
            }
        }
        //For ID auto incriment
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conObj.ConnectionString = connStr;
            cmdObj = new SqlCommand("select IDENT_CURRENT('durga_168233.Product')+IDENT_INCR('durga_168233.Product')", conObj);
            try
            {
                conObj.Open();
                object nxId = cmdObj.ExecuteScalar();
                txt_Pid.Text = nxId.ToString();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conObj.Close();
            }
        }

        private void Btn_Display_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Class1 st = new Class1();
                DataTable dt = st.DisplayProductBal();
                dt_Display.ItemsSource = dt.DefaultView;


            }
            catch (ProductException ex)
            {
                MessageBox.Show(ex.Message, "employee Management System");
            }
            catch (SqlException se)
            {

                MessageBox.Show(se.Message.ToString());
            }
            finally
            {

            }

        }
        private void Dg_display_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
    

