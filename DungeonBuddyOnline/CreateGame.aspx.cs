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
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
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
            angryLabel.Text = "Game Name already taken!  Please try another.";
            return;
        }
        gameTable.insertGame(game, (int) Session["userID"]);

        //Load Home page
        Response.Redirect("Home.aspx");
    }
}