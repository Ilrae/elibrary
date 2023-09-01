using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class adminauthormanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        // add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if(checkIfAuthorExists())
            {
                Response.Write("<script>alert('Author with this ID already Exist. You cannot add another Author with the same Author ID')</script>");
            }
            else
            {
                addNewAuthor();
            }
        }

        // update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if(checkIfAuthorExists())
            {
                updateAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author does not Exist. You cannot Update Author with this Author ID')</script>");
            }
        }

        // delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                deleteAuthor();
            }
            else
            {
                Response.Write("<script>alert('Author does not Exist. You cannot Update Delete this Author ID')</script>");
            }
        }

        // GO button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthorByID();
        }

        // user defined function
        void addNewAuthor()
        {
            SqlConnection conn = new SqlConnection(strcon);
            if(conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl" +
                "(author_id,author_name) VALUES (@author_id,@author_name)", conn);

            cmd.Parameters.AddWithValue("@author_id", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Write("<script>alert('add Author')</script>");
            clearForm();
            GridView1.DataBind();

        }

        void updateAuthor()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strcon);
                if(conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string sql = "UPDATE author_master_tbl SET author_name=@author_name WHERE author_id=" +
                    "'"+TextBox1.Text.Trim()+"' ";
               
                SqlCommand cmd = new SqlCommand(sql,conn);

                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());

                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("<script>alert('Ahthor Update Successfully')</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"')</script>");
            }
        }

        void deleteAuthor()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strcon);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string sql = "DELETE from author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Write("<script>alert('Author Deleted Successfully');</script>");
                clearForm();
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
        bool checkIfAuthorExists()
        {
            try
            {

                SqlConnection conn = new SqlConnection(strcon);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id=" +
                    "'" + TextBox1.Text.Trim() + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
                return false;
            }
            
        }

        void getAuthorByID()
        {
            try
            {

                SqlConnection conn = new SqlConnection(strcon);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id=" +
                    "'" + TextBox1.Text.Trim() + "'", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Author ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");      
            }
        }

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }
    }
}