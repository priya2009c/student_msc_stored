using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace student_msc_stored
{
    public partial class register : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["my_db"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindgrid();
            }
        }

        protected void txt_age0_TextChanged(object sender, EventArgs e)
        {

        }

        private void bindgrid()
        {
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = new SqlCommand("register_crud");
            cmd.Parameters.AddWithValue("@action", "SELECT");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection=con;
            sda.SelectCommand = cmd;
            DataSet dt = new DataSet();
            sda.Fill(dt);
            gv_stud.DataSource = dt;
            gv_stud.DataBind();
            con.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string name = txt_name.Text;
            string email = txt_age.Text;
            string password = txt_pass.Text;
            string gender = rbtn_g.SelectedValue;
            string subject = ch_btn.SelectedValue;
            string course = ddl_course.SelectedValue;
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("register_crud"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "INSERT");
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@subject", subject);
                    cmd.Parameters.AddWithValue("@course", course);


                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("inserted successfully");
                    con.Close();
                }
                this.bindgrid();
            }
        }

       

        protected void gv_stud_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_stud.EditIndex = e.NewEditIndex;
            this.bindgrid();
        }

        protected void gv_stud_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_stud.EditIndex = -1;
            this.bindgrid();
        }

        protected void gv_stud_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gv_stud.Rows[e.RowIndex];
            int id = Convert.ToInt32(gv_stud.DataKeys[e.RowIndex].Values[0]);
            string name = (row.Cells[2].Controls[0] as TextBox).Text;
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("register_crud"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "UPDATE");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                   
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("updated successfully");
                    con.Close();
                }
            }
            gv_stud.EditIndex = -1;
            this.bindgrid();
        }

        protected void gv_stud_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gv_stud.DataKeys[e.RowIndex].Values[0]);
            using (SqlConnection con = new SqlConnection(str)) 
            {
                using (SqlCommand cmd = new SqlCommand("register_crud"))
                {
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@action", "DELETE");
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection=con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                this.bindgrid();
            }
        }
    }
}