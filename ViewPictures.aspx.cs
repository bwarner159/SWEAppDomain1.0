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
        Response.Redirect("UploadPictures.aspx" + Session["UserID"]);
    }

    private static string ConnectionString = "Data Source=BRETTSPC\\SQLEXPRESS;Initial Catalog=PhotoSec;Integrated Security=True";

    private const int keySize = 256;

    private const int DerivationIterations = 1000;


    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConnectionString);        
        conn.Open();
        SqlCommand getPassCmd = new SqlCommand("Select Password FROM Users Where UserID = " + Session["UserID"], conn);
        string password = getPassCmd.Parameters.ToString();
        conn.Close();

        if (Request.QueryString["Id"] != null)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            string query = "Select Images from Images where Id=@Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["Id"]);
            DataTable dt = GetData(cmd.ToString());
            if(dt != null)
            {
                byte[] byteOfImage = (Byte[])dt.Rows[0]["Images"];
                string cipherText = Encoding.ASCII.GetString(byteOfImage);
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                string decryptedImages = Decrypt(cipherText, password);
                Response.Write(decryptedImages);
                Response.Flush();
                Response.End();
            }

        }
    }

    public static string Decrypt(string cipherText, string passPhrase)
    {

        // SqlConnection conn = new SqlConnection(ConnectionString);
        //conn.Open();
        //SqlCommand getPassword = new SqlCommand("Select Password from Users Where @Password=password");

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

    DataTable GetData(String queryString)
    {

        // Retrieve the connection string stored in the Web.config file.
        String connectionString = ConnectionString;

        DataTable ds = new DataTable();

        try
        {
            // Connect to the database and run the query.
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
}