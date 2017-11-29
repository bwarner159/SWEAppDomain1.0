using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Markup;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;


public partial class _Default : System.Web.UI.Page
{
    protected void GoToUpload(object sender, EventArgs e)
    {
        Response.Redirect("UploadPictures.aspx");
    }

    private static string ConnectionString = "Data Source=BRETTSPC\\SQLEXPRESS;Initial Catalog=PhotoSec;Integrated Security=True";

    private const int keySize = 256;

    private const int DerivationIterations = 1000;

    public void viewPicturesButton(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Select Password FROM Users Where UserID = " + Session["UserID"]);
            conn.Open();
            cmd.Connection = conn;
            SqlDataReader dr = cmd.ExecuteReader();
            {
                while (dr.Read())
                {

                    System.Web.UI.WebControls.Image dbImages = new System.Web.UI.WebControls.Image();

                    dbImages.ImageUrl = string.Format("ImageHandler.ashx?Id={1}", dr.GetInt32(0));

                    Id1.Controls.Add(dbImages);
                }
            }
            
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    
}