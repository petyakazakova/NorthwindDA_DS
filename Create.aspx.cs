using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

namespace NorthwindDA
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //ON CLICK
        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            //Connection string
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = northwind");
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            SqlCommand cmd = null;
            //queries
            string sqlsel = "select * from shippers";
            string sqlins = "insert into shippers values (@CompanyName, @Phone)";

            try
            {
                //connOpen();

                //Create da + SelectCommand property of da
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                //Create and fill ds in table:
                ds = new DataSet();
                da.Fill(ds, "MyShippers"); 
                
                //Display data
                dt = ds.Tables["MyShippers"];

                //Add an empty row
                DataRow newrow = dt.NewRow();
                newrow["CompanyName"] = TextBoxCompanyName.Text;
                newrow["Phone"] = TextBoxPhone.Text;
                dt.Rows.Add(newrow); //attach back to table

                //Create command
                cmd = new SqlCommand(sqlins, conn);
                //Map params
                cmd.Parameters.Add("@CompanyName", SqlDbType.Text, 50, "CompanyName"); //pass @param value to db column
                cmd.Parameters.Add("@Phone", SqlDbType.Text, 50, "Phone");

                //Insert into db table
                da.InsertCommand = cmd;
                da.Update(ds, "MyShippers"); 

                LabelMessage.Text = "Shipper added";
            }
            catch (Exception ex)  
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                //Close 
                conn.Close();
            }
        }
    }
}