using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    // Need: need to add test to see if any usernames are already taken in db
    // Need: should add encryption here or login for passwords
    // Want: Add Map to give give specific error code 

    private static string connectionstring = "Data Source=BRETTSPC\\SQLEXPRESS;Initial Catalog=PhotoSec;Integrated Security=True";

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsername.Focus();
        errorLbl.Visible = false;
    }

    public bool IsValid(char value)
    {
        if (value >= 48 && value <= 57)
            return true; //0-9
        if (value >= 65 && value <= 90)
            return true; //A-Z
        if (value >= 97 && value <= 122)
            return true; //a-z
        if (value == 32 || value == 46)
            return true; // space and period.
        return false;
    }

    public void submit(object sender, EventArgs e)
    {
        // need to add test to see if any usernames are already taken in db
        // should add encryption

        string userName = txtUsername.Text;
        string password = txtPassword.Text;
        


        foreach (char c in userName)
        {
            if (!IsValid(c))
            {
                userName = null;
            }
        }

        foreach (char c in password)
        {
            if (!IsValid(c))
            {
                password = null;
            }
        }


        if(userName == null || password == null || txtUsername.Text.Length < 1 || txtPassword.Text.Length < 1)
        {
            errorLbl.Visible = true;
            errorLbl.Text = "Please enter the correct information";
        }
        else
        {
            UserDatabase.AddUser(userName, password);
            Server.Transfer("HomePage.aspx", true);
        }
    }
}