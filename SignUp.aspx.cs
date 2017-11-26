using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    // Need: need to add test to see if any usernames are already taken in db
    // Need: should add encryption here or login for passwords
    // Want: Add Map to give give specific error code 

    protected void Page_Load(object sender, EventArgs e)
    {
        firstNameTxt.Focus();
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

    public bool isValidEmail(char value)
    {
        if (value >= 48 && value <= 57)
            return true; //0-9
        if (value >= 65 && value <= 90)
            return true; //A-Z
        if (value >= 97 && value <= 122)
            return true; //a-z
        return false;
    }

    public bool samePasswordAndEmail()
    {
        string password = passWordTxt.Text;
        string confirmPassword = confirmPassWordTxt.Text;
        string email = emailTxt.Text;
        string confirmEmail = confirmEmailTxt.Text;
        if (confirmEmail == email)
            return true;
        else if (confirmPassword == password)
            return true;
        else 
            return false;
    }

    public bool profileTester()
    {
        string firstName = firstNameTxt.Text;
        string lastName = lastNameTxt.Text;
        string userName = userNameTxt.Text;
        string password = passWordTxt.Text;
        string email = emailTxt.Text;

        foreach (char c in firstName)
        {
            if (!IsValid(c))
            {
                return false;
            }
        }

        foreach (char c in lastName)
        {
            if (!IsValid(c))
            {
                return false;
            }
        }
 
        foreach (char c in userName)
        {
            if (!IsValid(c))
            {
                return false;
            }
        }

        foreach (char c in password)
        {
            if (!IsValid(c))
            {
                return false;
            }
        }

        foreach (char c in email)
        {
            if (!isValidEmail(c))
            {
                if (!email.Contains("@"))
                {
                    if (!email.Contains(".com") && !email.Contains(".edu") && !email.Contains(".org"))
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public bool confirmEmailTester()
    {
        string confirmEmail = confirmEmailTxt.Text;
        foreach (char c in confirmEmail)
        {
            if (!isValidEmail(c))
            {
                if (!confirmEmail.Contains("@"))
                {
                    if (!confirmEmail.Contains(".com") && !confirmEmail.Contains(".edu") && !confirmEmail.Contains(".org"))
                    {
                        return false;
                    }
                }
            }
            
        }
        return true;
    }

    public bool confirmPasswordTester()
    {
        string confirmPassword = confirmPassWordTxt.Text;
        foreach (char c in confirmPassword)
        {
            if (IsValid(c))
            {
                return true;
            }
        }
        return false;
    }



    public void submit(object sender, EventArgs e)
    {
        // need to add test to see if any usernames are already taken in db
        // should add encryption
        profileTester();
        confirmEmailTester();
        samePasswordAndEmail();
        if(confirmEmailTester() == false || samePasswordAndEmail() == false 
           || profileTester() == false || firstNameTxt.Text.Length < 1
           || lastNameTxt.Text.Length < 1 || emailTxt.Text.Length < 1 
           || userNameTxt.Text.Length < 1 || confirmEmailTxt.Text.Length < 1
           || passWordTxt.Text.Length < 1)
        {
            errorLbl.Visible = true;
            errorLbl.Text = "Please enter the correct information";
        }
        else
        {
            Server.Transfer("HomePage.aspx", true);
        }
    }
}