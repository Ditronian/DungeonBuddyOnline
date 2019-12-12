using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    //Attempts login of the user
    protected void loginButton_Click(object sender, EventArgs e)
    {
        User loginUser = new User();
        loginUser.UserName = usernameTextBox.Text;
        loginUser.Password = Security.encrypt(passwordTextBox.Text);

        UsersTable userTable = new UsersTable(new DatabaseConnection());
        int userID = userTable.authenticateUser(loginUser);

        //Deny entry
        if(userID == 0)
        {
            angryLabel.Text = "Not found";
        }

        //Login to Home Page
        else
        {
            this.Session["userID"] = userID;

            //Load Main page
            Response.Redirect("Home.aspx");
        }
    }

    //Registers a new User
    protected void registerButton_Click(object sender, EventArgs e)
    {
        if (!this.IsValid)
        {
            angryLabel.Text = "User registration failed.  Captcha verification failed.";
            return;
        }
        UsersTable dbTable = new UsersTable(new DatabaseConnection());

        //Check if username alrdy taken
        if (dbTable.checkUsername(registerUsernameTextBox.Text))
        {
            angryLabel.Text = "User registration failed.  Username already taken.";
            return;
        }

        User registerUser = new User();
        registerUser.UserName = registerUsernameTextBox.Text;
        registerUser.Password = Security.encrypt(registerPasswordTextBox.Text);

        dbTable.insertUser(registerUser);
        int userID = dbTable.authenticateUser(registerUser);
        if(userID == 0)
        {
            angryLabel.Text = "User registration unsuccessful.  Please contact the system administrator.";
        }
        else
        {
            angryLabel.Text = "User registration successful!";
        }
    }
}