using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_Home : System.Web.UI.Page
{
    int userID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
        else userID = (int) Session["userID"];
    }


    protected void deleteAccountButton_Click(object sender, EventArgs e)
    {
        GamesTable gameTable = new GamesTable(new DatabaseConnection());

        //Check if deleteable
        if (gameTable.getGMGames(userID).Count != 0 || gameTable.getPlayerGames(userID).Count != 0)
        {
            angryLabel.ForeColor = System.Drawing.Color.Red;
            angryLabel.Text = "You cannot delete your account whilst you are still in games.";
            return;
        }

        //Delete the user
        UsersTable userTable = new UsersTable(new DatabaseConnection());
        userTable.deleteUser(userID);

        //Return to login
        Session.Clear();
        Response.Redirect("Login.aspx");

    }
}