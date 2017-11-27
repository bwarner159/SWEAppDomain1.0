using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
//using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Markup;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    protected void GoToUpload(object sender, EventArgs e)
    {
        Server.Transfer("UploadPictures.aspx" ,true);
    }

    private static string ConnectionString = "Data Source=BRETTSPC\\SQLEXPRESS;Initial Catalog=PhotoSec;Integrated Security=True";

    private const int keySize = 256;

    private const int DerivationIterations = 1000;

    private ImageList imageList1 = new ImageList();
    

    protected void Page_Load(object sender, EventArgs e)
    {
        // Session.Add["password"];

        SqlConnection con = new SqlConnection(ConnectionString);
        con.Open();
        SqlCommand passCmd = new SqlCommand("Select Password FROM Users Where UserName = " + User.Identity.Name, con);
        string password = passCmd.Parameters.ToString();
        con.Close();

        imageList1.ImageSize = new Size(100, 100);

        using (SqlConnection conn = new SqlConnection(ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("Select Images FROM Images where Images = @Images", conn);
            conn.Open();
            using (SqlDataReader imgReader = cmd.ExecuteReader())
            {
                while(imgReader.Read())
                {
                    string cipherText = imgReader[0].ToString();
                    string decryptedImgs = Decrypt(cipherText , password);
                    byte[] images = System.Text.Encoding.UTF8.GetBytes(decryptedImgs);
                   // imageList1.Images.Add(byteArrayToImage(images));
                }
            
            }
        }    
    }

    public static string Decrypt(string cipherText, string passPhrase)
        {
            
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand getPassword = new SqlCommand("Select Password from Users Where @Password=password");

            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(keySize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(keySize / 8).Take(keySize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((keySize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((keySize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(keySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

    public Image byteArrayToImage(byte[] byteArrayIn)
    {
        MemoryStream ms = new MemoryStream(byteArrayIn);
        Image returnImage = Image.FromStream(ms);
        return returnImage;
    }
}