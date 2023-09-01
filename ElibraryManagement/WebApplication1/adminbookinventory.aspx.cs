using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public partial class adminbookinventory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            fillAuthorPublisherValues();
            GridView1.DataBind();
        }

        // Go button 
        protected void LinkButton4_Click(object sender, EventArgs e)
        {

        }

        // Add button
        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        // Update button
        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        // Delete button
        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        // user defined functions
        void fillAuthorPublisherValues()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strcon);
                if(conn.State==ConnectionState.Closed)
                {
                    conn.Open();
                }

                string sql1 = "SELECT author_name FROM author_master_tbl;";
                string sql2 = "SELECT publisher_name FROM publisher_master_table;";
                SqlCommand cmd1 = new SqlCommand(sql1,conn);
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                DropDownList3.DataSource = dt1;
                DropDownList3.DataValueField = "author_name";
                DropDownList3.DataBind();

                SqlCommand cmd2 = new SqlCommand(sql2,conn);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                DropDownList2.DataSource = dt2;
                DropDownList2.DataValueField = "publisher_name";
                DropDownList2.DataBind();

            }
            catch(Exception ex)
            {

            }
        }

        void addNewBook()
        {

        }
    }
}