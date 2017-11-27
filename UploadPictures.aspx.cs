using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Threading;
using System.Web.Security;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;

//FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
//[UserID] INT   NOT NULL,

public partial class _Default : System.Web.UI.Page
{

    private static string connectionstring = "Data Source=BRETTSPC\\SQLEXPRESS;Initial Catalog=PhotoSec;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        result.Visible = false;
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
    
    /*
    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key,byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;
        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        // Return the encrypted bytes from the memory stream.
        return encrypted;

    }
    */

    public static byte[] EncryptBytesOfPicture(byte[] input)
    {
        Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes("bwarner159", GetRandomBytes());
        MemoryStream ms = new MemoryStream();
        AesManaged aes = new AesManaged();
        aes.Key = rfc.GetBytes(aes.KeySize / 8);
        aes.IV = rfc.GetBytes(aes.BlockSize / 8);
        CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(input, 0, input.Length);
        cs.Close();
        return ms.ToArray();
    }

    public void uploadPicture()
    {
        string fileName;
        SqlConnection con = new SqlConnection(connectionstring);
        if (picFile.HasFile)
        {
            fileName = picFile.PostedFile.FileName;
            using (Aes encrpytion = Aes.Create())
            {
                
                byte[] image = ImageToStream(fileName);
                byte [] encyptedImage = EncryptBytesOfPicture(image);
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert Into Images values (@Images)", con);
                cmd.Parameters.AddWithValue("@Images", encyptedImage);
                cmd.ExecuteNonQuery();
                con.Close();
           }
        }

        else

        {
            result.Visible = true;
            result.Text = "Please upload an image!";
        }
    }

    private byte[] ImageToStream(string FileName) {
        MemoryStream ms = new MemoryStream();
        tryagain:
        try
        {
            Bitmap image = new Bitmap(FileName);
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch(Exception ex)
        {
            goto tryagain;
        }
        return ms.ToArray();
    }
     

    public void submitButton(object sender, EventArgs e)
    {
        uploadPicture();
        result.Visible = true;
        // Server.Transfer("ViewPictures.aspx", true);
    }
}