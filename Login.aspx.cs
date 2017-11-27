﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.Providers.Entities;

public partial class Default2 : System.Web.UI.Page
{ 
    protected void submit(object sender, EventArgs e)
    {
        string password = txtPassword.Text;
        string username = txtUsername.Text;
        int userId = UserDatabase.GetUserIdByUsernameAndPassword(username, password);
        if (userId > 0)
        {
            // Now put userid in a session-variable
            // and redirect the user to the protected area of your website.
            loginresult.Text = string.Format("You are userId : {0}", userId);
            Session["userId"]=userId;
            Response.Redirect("ViewPictures.aspx");
        }
        else
        {
            loginresult.Text = "Wrong username or password";
        }
    }
}