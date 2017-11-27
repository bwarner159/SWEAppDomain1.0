using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Providers.Entities;

public partial class Default2 : System.Web.UI.Page
{
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

    protected void submit(object sender, EventArgs e)
    {
        string password = txtPassword.Text;
        string username = txtUsername.Text;
        foreach (char c in username)
        {
            if (!IsValid(c))
            {
                username = null;
            }
        }

        foreach (char c in password)
        {
            if (!IsValid(c))
            {
                password = null;
            }
        }


        int userId = UserDatabase.GetUserIdByUsernameAndPassword(username, password);
        if (userId > 0)
        {
            // Now put userid in a session-variable
            // and redirect the user to the protected area of your website.
            loginresult.Text = string.Format("You are userId : {0}", userId);
            Session["userId"] = userId;
            Response.Redirect("ViewPictures.aspx" + Session["UserID"]);
        }
        else
        {
            loginresult.Text = "Wrong username or password";
        }
    }
}
