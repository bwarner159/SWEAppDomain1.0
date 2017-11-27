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

    private const int keySize = 256;

    private const int DerivationIterations = 1000;

    public static byte[] Encrypt(string plainText, string passPhrase)
    {

        var salt = Generate256BitsOfRandomEntropy();
        var iv = Generate256BitsOfRandomEntropy();
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        using(var password = new Rfc2898DeriveBytes(passPhrase, salt, DerivationIterations))
        {
            var keyBytes = password.GetBytes(keySize / 8);
            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.BlockSize = 256;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using(var encryptor = symmetricKey.CreateEncryptor(keyBytes, iv))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(plainTextBytes, 0, plainText.Length);
                            cs.FlushFinalBlock();
                            var cipherTextBytes = salt;
                            cipherTextBytes = cipherTextBytes.Concat(iv).ToArray();
                            cipherTextBytes = cipherTextBytes.Concat(ms.ToArray()).ToArray();
                            ms.Close();
                            cs.Close();
                            return cipherTextBytes;
                        }
                    }
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        result.Visible = false;
    }

    private static byte[] Generate256BitsOfRandomEntropy()
    {
        var randomBytes = new byte[32];
        using(var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetBytes(randomBytes);
        }
        return randomBytes;
    }

    public void uploadPicture()
    {
        //insert sql query to get users password for string passPhrase from Encrypt method
        SqlConnection con = new SqlConnection(connectionstring);
        con.Open();
        SqlCommand getPassCmd = new SqlCommand("Select Password FROM Users Where UserName = " + User.Identity.Name, con);
        string password = getPassCmd.Parameters.ToString();
        con.Close();
        string fileName;
        
        if (picFile.HasFile)
        {
            fileName = picFile.PostedFile.FileName;
            using (Aes encrpytion = Aes.Create())
            {
                
                string image = ImageToString(fileName);
                byte [] encyptedImage = Encrypt(image, password);
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


    private string ImageToString(string file)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(picFile.PostedFile.FileName))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();

                string base64string = Convert.ToBase64String(imageBytes);
                return base64string;
            }
        }
    }
     
    public void submitButton(object sender, EventArgs e)
    {
        uploadPicture();
        result.Visible = true;
        // Server.Transfer("ViewPictures.aspx", true);
    }
}