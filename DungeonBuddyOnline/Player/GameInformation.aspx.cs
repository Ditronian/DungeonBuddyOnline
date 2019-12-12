using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_GameInformation : System.Web.UI.Page
{
    Game game;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
        if (Session["activeGame"] == null) Response.Redirect("~/Home.aspx");

        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        if (!this.IsPostBack) loadGame();
    }

    //Last chance to load stuff
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (game.ImagePath == "") gameImage.ImageUrl = "~/Resources/Global/upload.png";
        else gameImage.ImageUrl = "~/" + game.ImagePath;
    }

    //Loads all the details of the current game
    private void loadGame()
    {
        //Check if Im dealing with a fully loaded game object, if not fully load it.
        if (game.FullyLoaded == false)
        {
            GamesTable gamesTable = new GamesTable(new DatabaseConnection());
            game = gamesTable.getGame(game.GameID);
            Session["activeGame"] = game;
        }

        nameTextBox.Text = game.GameName;
        settingTextBox.Text = game.GameSetting;
        descriptionTextBox.Text = game.GameDescription;
        additionalTextBox.Text = game.GameAdditionalInfo;
    }
}