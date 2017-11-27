using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for UserDatabase
/// </summary>
public class UserDatabase
{
    private static string connectionstring = "Data Source=localhost;Initial Catalog=PhotoSecDatabase;Integrated Security=True";

    public static bool AddUser(string username, string password)
    {
        Guid userGuid = System.Guid.NewGuid();

        SqlConnection con = new SqlConnection(connectionstring);
        using (SqlCommand cmd = new SqlCommand("INSERT INTO [User] VALUES (@username, @password, @UserGuid)", con))
        {
            // Add the input as parameters to avoid sql-injections
            // I'll explain later in this article.
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", Hashing.HashSHA256(password));
            cmd.Parameters.AddWithValue("@userguid", userGuid); 

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        return true;
    }
    public static int GetUserIdByUsernameAndPassword(string username, string password)
    {
        int userId = 0;
        // this is the value we will return

        SqlConnection con = new SqlConnection(connectionstring);
        using (SqlCommand cmd = new SqlCommand("SELECT UserId, Password, UserGuid FROM [User] WHERE username=@username", con))
        {
            cmd.Parameters.AddWithValue("@username", username);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                // dr.Read() = we found user(s) with matching username!
                char[] charsToTrim = { '*', ' ', '\''};
                int dbUserId = Convert.ToInt32(dr["UserId"]);
                string dbPassword = Convert.ToString(dr["Password"]);
                string dbUserGuid = Convert.ToString(dr["UserGuid"]);
                string result = dbPassword.Trim(charsToTrim);

                // Now we hash the UserGuid from the database with the password we wan't to check
                // In the same way as when we saved it to the database in the first place. (see AddUser() function)
                string hashedPassword = Hashing.HashSHA256(password);

                // if its correct password the result of the hash is the same as in the database
                if (result == hashedPassword)
                {
                    // The password is correct
                    userId = dbUserId;
                }
            }
            con.Close();
        }// Return the user id which is 0 if we did not found a user.
        return userId;
    }
}