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
        // setup client-side input processing
        loginCaptcha.UserInputID = captchaTextBox.ClientID;
    }

    //Attempts login of the user
    protected void loginButton_Click(object sender, EventArgs e)
    {
        UsersTable userTable = new UsersTable(new DatabaseConnection());
        string salt = userTable.getUserSalt(usernameTextBox.Text);

        //No user with that username
        if (salt == null)
        {
            angryLabel.Text = "Not found";
            return;
        }

        User loginUser = new User();
        loginUser.UserName = usernameTextBox.Text;
        loginUser.Password = Security.encrypt(passwordTextBox.Text, salt);

        loginUser = userTable.authenticateUser(loginUser);

        //Deny entry
        if(loginUser == null) angryLabel.Text = "Not found";
        else if(loginUser.IsConfirmed == false) angryLabel.Text = "You must confirm your email before proceeding.";
        //Login to Home Page
        else
        {
            this.Session["userID"] = loginUser.UserID;
            userTable.updateUserLoginDate(loginUser);

            //Load Main page
            Response.Redirect("Home");
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
        
        if(registerPasswordTextBox.Text != registerConfirmPasswordTextBox.Text)
        {
            angryLabel.Text = "User registration failed.  Passwords do not match!";
            return;
        }

        UsersTable dbTable = new UsersTable(new DatabaseConnection());

        //Check if username alrdy taken
        if (dbTable.checkUsername(registerUsrNmTextBox.Text))
        {
            angryLabel.Text = "User registration failed.  Username already taken.";
            return;
        }

        string salt = Security.getSalt();

        User registerUser = new User();
        registerUser.UserName = registerUsrNmTextBox.Text;
        registerUser.Password = Security.encrypt(registerPasswordTextBox.Text, salt);
        registerUser.Email = registerEmailTextBox.Text;

        dbTable.insertUser(registerUser, salt);
        registerUser = dbTable.authenticateUser(registerUser);
        if(registerUser == null)
        {
            angryLabel.Text = "User registration unsuccessful.  Please contact the system administrator.";
        }
        else
        {
            string confirmationID = Guid.NewGuid().ToString()+"-"+Guid.NewGuid().ToString();

            string body = "Greetings, and let me be the first to welcome you to DungeonBuddy!";
            body += "\n\n Please click on this link to continue your journey!  https://dungeonbuddy.net/Confirm?confirm=" + confirmationID;

            Email.sendEmail(registerUser.Email, "Welcome to DungeonBuddy!", body);

            dbTable.insertConfirmationID(registerUser.UserID, confirmationID);
            angryLabel.Text = "User registration successful!  A confirmation email has been sent to your account with further instructions.";
        }
    }
}