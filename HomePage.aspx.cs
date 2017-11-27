using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void GoToSignUpPage(object sender, EventArgs e)
    {
        Server.Transfer("SignUp.aspx", true);
    }
    protected void GoToLoginPage(object sender, EventArgs e)
    {
        Server.Transfer("Login.aspx", true);
    }
}