using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //On page load, will need to load picture that is being displayed
        //Will allow user to see image that they are downloading or deleting
    }

    protected void DownloadPicture(object sender, EventArgs e)
    {
        
    }
    /*
    public void SaveImage(string filename, ImageFormat imgFormat) 
    {
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(imageUrl);
        Bitmap bitmap;  bitmap = new Bitmap(stream);

        if (bitmap != null) 
            bitmap.Save(filename, imgFormat);

        stream.Flush();
        stream.Close();
        client.Dispose();
    }
     */
}