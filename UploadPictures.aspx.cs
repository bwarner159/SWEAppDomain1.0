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

    

    public static byte[] Encrypt(byte[] plainText, string passPhrase)
    {

        var salt = Generate256BitsOfRandomEntropy();
        var iv = Generate256BitsOfRandomEntropy();
        var plainTextBytes = plainText;

        using (var password = new Rfc2898DeriveBytes(passPhrase, salt, DerivationIterations))
        {
            var keyBytes = password.GetBytes(keySize / 8);
            using (var symmetricKey = new RijndaelManaged())
            {
                symmetricKey.BlockSize = 256;
                symmetricKey.Mode = CipherMode.CBC;
                symmetricKey.Padding = PaddingMode.PKCS7;
                using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, iv))
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
        int userID = (int)Session["UserID"];
    }

    private static byte[] Generate256BitsOfRandomEntropy()
    {
        var randomBytes = new byte[32];
        using (var rngCsp = new RNGCryptoServiceProvider())
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
        SqlCommand getPassCmd = new SqlCommand("Select Password FROM Users Where UserID = " + Session["UserID"], con);
        string password = getPassCmd.Parameters.ToString();
        con.Close();
        string fileName;

        if (picFile.HasFile)
        {
            fileName = picFile.PostedFile.FileName;
            using (Rijndael encryption = RijndaelManaged.Create())
            {

                byte[] image = ImageToBytes(fileName);
                byte[] encyptedImage = Encrypt(image, password);
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert Into Images values (@Images, @UserID)", con);
                cmd.Parameters.AddWithValue("@Images", encyptedImage);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
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


    private byte [] ImageToBytes(string file)
    {
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(picFile.PostedFile.FileName))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();

                return imageBytes;
            }
        }
    }

    public void submitButton(object sender, EventArgs e)
    {
        uploadPicture();
        SqlConnection conn = new SqlConnection(connectionstring);
        conn.Open();       
        SqlCommand cmd = new SqlCommand("Select ID From Images where ID=@Id",conn);
        result.Text = "The photo you submitted has the ID of 1033";
        result.Visible = true;
    }

    public void GoToViewPictures(object sender, EventArgs e)
    {
        Response.Redirect("ViewPictures.aspx");
    }
}