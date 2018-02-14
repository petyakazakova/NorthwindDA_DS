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
    public partial class ReadUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ButtonUpdate.Enabled = false; //cannot click at page load
            }

            UpdateGridview();
        }

        public void UpdateGridview()
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = northwind");
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            string sqlsel = "select * from shippers";

            try
            {
                //conn.Open();   SqlDataAdapter opens the connextion itself

                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                ds = new DataSet();
                da.Fill(ds, "MyShippers");

                //Display data
                dt = ds.Tables["MyShippers"];

                GridViewShippers.DataSource = dt;
                GridViewShippers.DataBind(); //otherwise it won't display
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();   // SqlDataAdapter closes connection by itself; but can fail in case of errors
            }
        }

        //Select from Grid
        protected void GridViewShippers_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxCompany.Text = GridViewShippers.SelectedRow.Cells[2].Text;
            TextBoxPhone.Text = GridViewShippers.SelectedRow.Cells[3].Text;
            LabelMessage.Text = "You chose shipperID " + GridViewShippers.SelectedRow.Cells[1].Text;
            ButtonUpdate.Enabled = true;
        }

        //UPDATE
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = northwind");
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            SqlCommand cmd = null;
            string sqlsel = "select * from shippers";
            string sqlupd = "update shippers set CompanyName = @CompanyName, Phone = @Phone where ShipperId = @ShipperID";

            try
            {
                da = new SqlDataAdapter();
                //Select command
                da.SelectCommand = new SqlCommand(sqlsel, conn);

                //update LOCAL VERSION
                ds = new DataSet();
                da.Fill(ds, "MyShippers");
       
                //Display
                dt = ds.Tables["MyShippers"];

                dt.Rows[GridViewShippers.SelectedIndex]["CompanyName"] = TextBoxCompany.Text;  //rows, cols name
                dt.Rows[GridViewShippers.SelectedIndex]["Phone"] = TextBoxPhone.Text;

                //Update command
                cmd = new SqlCommand(sqlupd, conn);
                cmd.Parameters.Add("@CompanyName", SqlDbType.Text, 50, "CompanyName");
                cmd.Parameters.Add("@Phone", SqlDbType.Text, 50, "Phone");
                SqlParameter parm = cmd.Parameters.Add("@ShipperID", SqlDbType.Int, 4, "ShipperID");
                parm.SourceVersion = DataRowVersion.Original;  // Good habit if someone has changed the primary key

                //Update in DB
                da.UpdateCommand = cmd;
                da.Update(ds, "MyShippers");

                LabelMessage.Text = "Shipper has been updated";
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            UpdateGridview();
        }
    }
}