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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //On page load, will need to load picture that is being displayed
        //Will allow user to see image that they are downloading or deleting
    }

    public static byte[] GetRandomBytes()
    {
        int saltLength = GetSaltLength();
        byte[] ba = new byte[saltLength];
        RNGCryptoServiceProvider.Create().GetBytes(ba);
        return ba;
    }

    public static int GetSaltLength()
    {
        return 8;
    }

    public static byte[] Decrypt(byte[] input)
    {
        Rfc2898DeriveBytes rfc =
          new Rfc2898DeriveBytes("bwarner159", GetRandomBytes()); // Change this
        MemoryStream ms = new MemoryStream();
        Aes aes = new AesManaged();
        aes.Key = rfc.GetBytes(aes.KeySize / 8);
        aes.IV = rfc.GetBytes(aes.BlockSize / 8);
        CryptoStream cs = new CryptoStream(ms,
          aes.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(input, 0, input.Length);
        cs.Close();
        return ms.ToArray();
    }

    protected void DownloadPicture(object sender, EventArgs e)
    {
        string filePath = "C:\\Users\\Brett\\Pictures\\petland.jpg";

        Bitmap bitmap = new Bitmap(filePath);
        bitmap.Save("C:\\Users\\Brett\\Pictures\\Camera Roll\\petland.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        bitmap.Dispose();
    }

    //protected void deleteImage(object sender, EventArgs e)
    //{
        //Retrieve Image from Database -- May have to do this by retrieving username/password etc
        //Remove image from Database
        //Close connection to Database

       // SqlConnection connection = new SqlConnection();
        //try {
        //connection.Open();
       // SqlCommand command = new SqlCommand("SELECT Picture FROM Images WHERE username=''" /*username*/);
        //SqlDataReader reader = new command.ExecuteReader();
            
        //} 

 
    //}
}