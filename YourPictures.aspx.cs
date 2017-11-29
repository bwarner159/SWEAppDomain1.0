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
using System.Text;


public partial class _Default : System.Web.UI.Page
{
    private static string connectionstring = "Data Source=BRETTSPC\\SQLEXPRESS;Initial Catalog=PhotoSec;Integrated Security=True";

    private const int keySize = 256;

    private const int DerivationIterations = 1000;

    protected void Page_Load(object sender, EventArgs e)
    {
        //On page load, will need to load picture that is being displayed
        //Will allow user to see image that they are downloading or deleting
    }

    protected void DownloadPicture(object sender, EventArgs e)
    {

    }

    public static byte[] Decrypt(byte[] cipherText, string passPhrase)
    {
        // Get the complete stream of bytes that represent:
        // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        var cipherTextBytesWithSaltAndIv = cipherText;
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
                            string encodedString = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            return Encoding.ASCII.GetBytes(encodedString);
                        }
                    }
                }
            }
        }
    }

    protected void DeletePicture(object sender, EventArgs e)
    {
        string id = deleteTxt.Text;
        foreach (char c in id)
        {
            if (c >= 48 && c <= 57)
            {
                id = deleteTxt.Text;
            }
            else
            {
                id = null;
                deleteLbl.Text = "Please enter a valid ID.";
            }
        }
        SqlConnection conn = new SqlConnection(connectionstring);
        conn.Open();
        SqlCommand cmd = new SqlCommand("Delete FROM Images Where Id = " + id, conn);
        cmd.ExecuteNonQuery();
        deleteLbl.Text = "You have successfully deleted the image from the database.";
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