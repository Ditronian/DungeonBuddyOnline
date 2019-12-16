using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_CreateGame : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login");

        //Little tool for relaying messages thru redirects
        if (Session["message"] != null)
        {
            Message message = (Message)Session["message"];
            angryLabel.ForeColor = message.Color;
            angryLabel.Text = message.Text;
            Session.Remove("message");
        }
        else angryLabel.Text = "&nbsp;";
    }

    protected void createGameButton_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        string gameName = gameNameTextBox.Text;
        string gameSetting = gameSettingTextBox.Text;
        bool acceptingPlayers;
        if (acceptPlayersRadioList.SelectedValue == "true") acceptingPlayers = true;
        else acceptingPlayers = false;

        Game game = new Game();
        game.GameName = gameName;
        game.GameSetting = gameSetting;
        game.AcceptsPlayers = acceptingPlayers;

        GamesTable gameTable = new GamesTable(new DatabaseConnection());
        bool alreadyExists = gameTable.checkGameExistsByName(gameName);
        if (alreadyExists)
        {
            angryLabel.ForeColor = System.Drawing.Color.Red;
            angryLabel.Text = "Game Name already taken!  Please try another.";
            return;
        }
        gameTable.insertGame(game, (int) Session["userID"]);

        //Load Home page
        Session["message"] = new Message("Game Created!  You may find it listed under 'Game Master Games'.", System.Drawing.Color.Green);
        Response.Redirect("CreateGame");
    }
}