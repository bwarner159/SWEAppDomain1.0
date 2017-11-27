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
        bool check = false;
        Guid userGuid = System.Guid.NewGuid();

        SqlConnection con = new SqlConnection(connectionstring);
        string hashedPassword = Hashing.HashSHA256(password + userGuid.ToString());
        using (SqlCommand sqlCommand = new SqlCommand("SELECT * from [User] where username like @username AND password like @password", con))
        {
            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@password", hashedPassword);
            con.Open();
            SqlDataReader userCount = sqlCommand.ExecuteReader();
            while (userCount.Read())
            {
                char[] charsToTrim = { '*', ' ', '\'' };
                string dbUserNameCheck = Convert.ToString(userCount["Username"]);
                if (dbUserNameCheck == username)
                {
                    check = true;
                    break;
                }
            }
            if (check == true)
            {
                Console.WriteLine("The user already exist, try again");
                con.Close();
            }
            check = false;
            con.Close();
        }
        using (SqlCommand cmd = new SqlCommand("INSERT INTO [User] VALUES (@username, @password, @UserGuid)", con))
        {
            // Add the input as parameters to avoid sql-injections
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hashedPassword);
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
                char[] charsToTrim = { '*', ' ', '\'' };
                int dbUserId = Convert.ToInt32(dr["UserId"]);
                string dbPassword = Convert.ToString(dr["Password"]);
                string dbUserGuid = Convert.ToString(dr["UserGuid"]);
                string result = dbPassword.Trim(charsToTrim);

                //hash the UserGuid from the database with the password we want to check
                string hashedPassword = Hashing.HashSHA256(password + dbUserGuid);

                // if its correct password the result of the hash is the same as in the database
                if (result == hashedPassword)
                {
                    // The password is correct
                    userId = dbUserId;
                }
            }
            con.Close();
            return userId;
        }
    }
}