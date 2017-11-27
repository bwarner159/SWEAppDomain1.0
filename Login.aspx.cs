using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        userNameTxt.Focus();
    }

    protected void submit(object sender, EventArgs e)
    {
        string username = userNameTxt.Text;
        foreach (char c in username)
        {
            if (!IsValid(c))
            {
                username = null;
            }
        }

        string password = passWordTxt.Text;
        foreach (char c in password)
        {
            if (!IsValid(c))
            {
                password = null;
            }
        }

        int userId = UserDatabase.GetUserIdByUsernameAndPassword(username, password);

        if (username == null || password == null || userNameTxt.Text.Length < 1 || passWordTxt.Text.Length < 1)
        {
            errLbl.Text = "Please use the correct username or password";
        }
        else
        {
            Server.Transfer("ViewPictures.aspx", true);
        }
    }
}