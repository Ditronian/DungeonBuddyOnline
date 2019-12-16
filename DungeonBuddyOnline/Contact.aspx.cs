using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contact : System.Web.UI.Page
{
    int userID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login");
        else userID = (int)Session["userID"];
    }

    //Sends email to dungeonbuddy inbox
    protected void sendMessageButton_Click(object sender, EventArgs e)
    {
        UsersTable userTable = new UsersTable(new DatabaseConnection());
        string username = "test";

        if(messageTextField.Text == "")
        {
            angryLabel.ForeColor = System.Drawing.Color.Red;
            angryLabel.Text = "No message provided.";
            return;
        }

        string contactType = contactTypeList.SelectedValue;
        string message = $"Username: {userID.ToString()}\n" +
            $"User Email: Not Done Yet.\n" +
            $"Contact Type: {contactType}\n\n" +
            $"Message:\n";
        message += messageTextField.Text;

        bool isSuccessful = Email.sendEmail("inbox@dungeonbuddy.net", "New Contact Form Submission", message, username);

        if (isSuccessful)
        {
            angryLabel.ForeColor = System.Drawing.Color.Green;
            angryLabel.Text = "Message Sent!";
            messageTextField.Text = "";
        }
        else
        {
            angryLabel.ForeColor = System.Drawing.Color.Red;
            angryLabel.Text = "Uh oh, something went wrong.  I'd tell you to contact the administrator but that's what you just failed to do...";
        }
    }
}