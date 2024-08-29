using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyMarket.Admin
{
    public partial class Category : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            getCategories();
        }

        void getCategories()
        {
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Category_Proc", conn);
            cmd.Parameters.AddWithValue("@Action" , "GETALL");
            cmd.CommandType = CommandType.StoredProcedure;
            sda= new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            rCategory.DataSource = dt;
            rCategory.DataBind();
        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            string imagePath = string.Empty;
            string fileExtension = string.Empty;
            bool isValid = false;
            int categoryId = Convert.ToInt32(hfCategoryId.Value);
            conn = new SqlConnection(Utils.getConnection());
            cmd = new SqlCommand("Category_Proc", conn);
            cmd.Parameters.AddWithValue("@Action", categoryId == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
            cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);
            if (fuCategoryImage.HasFiles || fuCategoryImage.HasFiles)
            {
                if (Utils.isValidExtension(fuCategoryImage.FileName))
                {
                    string newImageName = Utils.getUniqueId();
                    fileExtension = Path.GetExtension(fuCategoryImage.FileName);
                    imagePath = "Images/Category/" + newImageName.ToString() + fileExtension;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + newImageName.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@CategoryImageUrl", imagePath);
                    isValid = true;
                }
                else
                {
                    lblMessage.Visible = false;
                    lblMessage.Text = "Please selct .jpg, .png or .jpeg image";
                    lblMessage.CssClass = "alert alert-danger";
                    isValid = false;
                }
            }
            else
            {
                isValid = true;
            }
            if (isValid)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    actionName = categoryId == 0 ? "inserted" : "Successfull";
                    lblMessage.Visible = true;
                    lblMessage.Text = "Category " + actionName + " successfully!";
                    lblMessage.CssClass = "alert alert-success";
                }
                catch (Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error-" + ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        void clear()
        {
            txtCategoryName.Text = string.Empty;
            cbIsActive.Checked = false;
            hfCategoryId.Value = "0";
            btnAddOrUpdate.Text = "Add";
            ImagePreview.ImageUrl = string.Empty;
        }

        //EditEventmethod
        protected void rCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lblMessage.Visible=false;
            if(e.CommandName=="edit")
            {
                conn = new SqlConnection(Utils.getConnection());
                cmd = new SqlCommand("Category_Proc", conn);
                cmd.Parameters.AddWithValue("@Action", "GETBYID");
                cmd.Parameters.AddWithValue("@CategoryId", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                txtCategoryName.Text = dt.Rows[0]["CategoryName"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                ImagePreview.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["CategoryImageUrl"].ToString()) ? "../Images/NoImage.png" : "../" + dt.Rows[0]["CategoryImageUrl"].ToString();
                ImagePreview.Height = 200;
                ImagePreview.Width = 200;
            }
        }

        //protected void btnAdd_Click(object sender, EventArgs e)
        //{

        //}
    }
}