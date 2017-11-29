<%@ WebHandler Language="C#" Class="ImageHandler" %>

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
using System.Text;


public class ImageHandler : IHttpHandler {

    private static string connectionstring = "Data Source=BRETTSPC\\SQLEXPRESS;Initial Catalog=PhotoSec;Integrated Security=True";

    private const int keySize = 256;

    private const int DerivationIterations = 1000;
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand getPassCmd = new SqlCommand("Select Password FROM Users Where UserID = " + Session["UserID"], conn);
            string password = getPassCmd.Parameters.ToString();
            conn.Close();

            if (context.Request.QueryString["Id"] != null)
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                string query = "Select Images from Images where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(context.Request.QueryString["Id"]);
                DataTable dt = GetData(cmd.ToString());
                if (dt != null)
                {
                    byte[] byteOfImage = (Byte[])dt.Rows[0]["Images"];
                    byte[] cipherText = byteOfImage;
                    byte[] decryptedImages = Decrypt(cipherText, password);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.GetBytes(0, 0, decryptedImages, 0, int.MaxValue);
                    
                    context.Response.Buffer = true;
                    context.Response.Charset = "";
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    context.Response.ContentType = "image/jpg";                                 
                    context.Response.BinaryWrite(decryptedImages);                 
                    context.Response.End();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    DataTable GetData(String queryString)
    {
        String connectionString = connectionstring;

        DataTable ds = new DataTable();

        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
            // Fill the DataSet.
            adapter.Fill(ds);
        }
        catch (Exception ex)
        {
        }
        return ds;
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
 
    public bool IsReusable
    {
        get 
        {
            return false;
        }
    }

}