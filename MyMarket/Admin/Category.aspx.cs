using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        }

        protected void btnAddOrUpdate_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            string imagePath = string.Empty; 
            string fileExtension = string.Empty;
            bool isValid = false;
            int categoryId = Convert.ToInt32(hfCategoryId.Value);
            conn = new SqlConnection();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}