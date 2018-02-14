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
    public partial class Delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ButtonDelete.Enabled = false;
                UpdateGridview();
            }

            DropDownListShippers.AutoPostBack = true;
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
                
                //Display
                dt = ds.Tables["MyShippers"];

                GridViewShippers.DataSource = dt;
                GridViewShippers.DataBind();

                //DropDown
                DropDownListShippers.DataSource = dt;
                DropDownListShippers.DataTextField = "CompanyName";
                DropDownListShippers.DataValueField = "ShipperID";
                DropDownListShippers.DataBind();
                DropDownListShippers.Items.Insert(0, "Select a shipper");
            }
            catch (Exception ex)
            {
                LabelMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();   // SqlDataAdapter closes connextion by itself; but can fail in case of errors
            }
        }

        //if an index is chosen
        protected void DropDownListShippers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListShippers.SelectedIndex != 0)
            {
                LabelMessage.Text = "You chose ShipperID " + DropDownListShippers.SelectedValue;
                ButtonDelete.Enabled = true;
            }
            else
            {
                LabelMessage.Text = "You chose none";
                ButtonDelete.Enabled = false;
            }
        }

        //DELETE
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"data source = .\sqlexpress; integrated security = true; database = northwind");
            SqlDataAdapter da = null;
            SqlCommandBuilder cb = null;
            DataSet ds = null;
            DataTable dt = null;
            string sqlsel = "select * from shippers";

            try
            {
                //conn.Open();
                da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(sqlsel, conn); 

                cb = new SqlCommandBuilder(da);

                ds = new DataSet();
                da.Fill(ds, "MyShippers");
           
                dt = ds.Tables["Myshippers"];

                //FOREACH LOOP
                foreach (DataRow row in dt.Select("ShipperID = " + Convert.ToInt32(DropDownListShippers.SelectedValue)))
                {
                    row.Delete();
                }

                //UPDATE
                da.Update(ds, "MyShippers");

                ButtonDelete.Enabled = false;
                LabelMessage.Text = "Shipper " + DropDownListShippers.SelectedValue + " has been deleted";
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