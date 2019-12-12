using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.MasterPage
{
    List<Game> gmGamesList;
    List<Game> playerGamesList;
    int userID;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
        else userID = (int)Session["userID"];

        //Populate User's Games
        GamesTable gameTable = new GamesTable(new DatabaseConnection());
        gmGamesList = gameTable.getGMGames(userID);
        playerGamesList = gameTable.getPlayerGames(userID);

        loadPlayerGames();
        loadGMGames();
        loadCookies();
    }

    //Loads all the NavBar content for each game the user is Game Mastering in.
    protected void loadGMGames()
    {

        Panel gmGames = new Panel();
        gmGames.ID = "gmGames";
        gmGames.CssClass = "outerDropdownContainer";

        //Foreach Game Database says User is GMing in, create a corresponding Game Dropdown and associated panel.
        //This is a 'pretend' game to display what it should look like if you actually are playing in two games.
        foreach (Game game in gmGamesList)
        {
            GameLink gameLink = new GameLink(gmGamesLink, gmCarrot);
            gameLink.ID = Server.HtmlDecode(game.GameName + "GMLink");
            gameLink.Text = game.GameName;
            gameLink.Text += "<i class=\"fa fa-caret-down\"></i>";
            gameLink.GameCarrotID = game.GameName + "GMCarrot";
            gameLink.CssClass = "dropdown-btn";
            gmGames.Controls.Add(gameLink);

            Panel gamePanel = new Panel();
            gamePanel.ID = Server.HtmlDecode(game.GameName + "GMPanel");
            gamePanel.CssClass = "innerDropdownContainer";
            {
                GamePageButton gameInfo = new GamePageButton(gameLink, "GM/GameInformationGM.aspx", game);
                gameInfo.Text = "Game Information";
                gameInfo.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(gameInfo);

                GamePageButton charList = new GamePageButton(gameLink, "GM/GamePartyGM.aspx", game);
                charList.Text = "Game Party";
                charList.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(charList);

                GamePageButton encounterPage = new GamePageButton(gameLink, "GM/EncounterTool.aspx", game);
                encounterPage.Text = "Encounter Tool";
                encounterPage.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(encounterPage);

                GamePageButton npcTrackerPage = new GamePageButton(gameLink, "GM/NPCTracker.aspx", game);
                npcTrackerPage.Text = "NPC Tracker";
                npcTrackerPage.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(npcTrackerPage);

                GamePageButton npcToolPage = new GamePageButton(gameLink, "GM/NPCTool.aspx", game);
                npcToolPage.Text = "NPC Tool";
                npcToolPage.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(npcToolPage);

                GamePageButton magicShopToolPage = new GamePageButton(gameLink, "GM/MagicShopTool.aspx", game);
                magicShopToolPage.Text = "Magic Shop Tool";
                magicShopToolPage.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(magicShopToolPage);

                GamePageButton publicPages = new GamePageButton(gameLink, "GM/CustomPageTool.aspx", game);
                publicPages.Text = "Add/Edit/Remove Pages";
                publicPages.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(publicPages);
            }
            gmGames.Controls.Add(gamePanel);
        }

        //Give a nice label if user has no games, so they are not sad :(
        if (gmGamesList.Count == 0)
        {
            Label noGamesLabel = new Label();
            noGamesLabel.Text = "You have no games.";
            noGamesLabel.CssClass = "innerDropdownNoGames";
            gmGames.Controls.Add(noGamesLabel);
            gmGamesDropdown.Controls.Add(noGamesLabel);
        }
        else gmGamesDropdown.Controls.Add(gmGames);
    }

    //Loads all the NavBar content for each game the user is playing in.
    protected void loadPlayerGames()
    {
        Panel playerGames = new Panel();
        playerGames.ID = "playerGames";
        playerGames.CssClass = "outerDropdownContainer";

        //Foreach Game database says User is playing in, create a corresponding Game Dropdown and associated panel.  These are both 'pretend' games
        //to display what it should look like if you actually are playing in two games.
        foreach (Game game in playerGamesList)
        {
            GameLink gameLink = new GameLink(playerGamesLink, playerCarrot);
            gameLink.ID = Server.HtmlDecode(game.GameName + "PlayerLink");
            gameLink.Text = game.GameName;
            gameLink.Text += "<i class=\"fa fa-caret-down\"></i>";
            gameLink.GameCarrotID = game.GameName + "PlayerCarrot";
            gameLink.CssClass = "dropdown-btn";
            playerGames.Controls.Add(gameLink);

            Panel gamePanel = new Panel();
            gamePanel.ID = Server.HtmlDecode(game.GameName + "PlayerPanel");
            gamePanel.CssClass = "innerDropdownContainer";
            {
                GamePageButton gameInfo = new GamePageButton(gameLink, "Player/GameInformation.aspx", game);
                gameInfo.Text = "Game Information";
                gameInfo.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(gameInfo);

                GamePageButton charList = new GamePageButton(gameLink, "Player/GameParty.aspx", game);
                charList.Text = "Game Party";
                charList.Click += new EventHandler(gamePageButtonClicked);
                gamePanel.Controls.Add(charList);

                //Add Custom Pages
                PagesTable pagesTable = new PagesTable(new DatabaseConnection());
                CustomPageList pages = pagesTable.getGamePages(game.GameID);

                for (int i = 0; i < pages.Pages.Count; i++)
                {
                    CustomPage page = pages.Pages[i];
                    GamePageButton publicPage = new GamePageButton(gameLink, page.PageURL, game, true);
                    publicPage.Text = page.PageName;
                    publicPage.Click += new EventHandler(gamePageButtonClicked);
                    gamePanel.Controls.Add(publicPage);
                }
            }
            playerGames.Controls.Add(gamePanel);
        }

        //Give a nice label if user has no games, so they are not sad :(
        if (playerGamesList.Count == 0)
        {
            Label noGamesLabel = new Label();
            noGamesLabel.Text = "You have no games.";
            noGamesLabel.CssClass = "innerDropdownNoGames";
            playerGames.Controls.Add(noGamesLabel);
            playerGamesDropdown.Controls.Add(noGamesLabel);
        }

        else playerGamesDropdown.Controls.Add(playerGames);
    }

    //Grabs all the cookies and uses them to persist all necessary data on the Main page.
    protected void loadCookies()
    {

        //Make sure I have cookie needed for Dropdown load state, and if so set CSS to saved state
        if (Session["gameLinkID"] != null && Session["gameCategoryID"] != null && Session["gamePanelID"] != null && Session["gameTypePanelID"] != null)
        {
            GameLink gameLink = (GameLink)this.FindControl((String)Session["gameLinkID"]);
            HyperLink gameCategory = (HyperLink)this.FindControl((String)Session["gameCategoryID"]);
            Panel gamePanel = (Panel)this.FindControl((String)Session["gamePanelID"]);
            Panel gameTypePanel = (Panel)this.FindControl((String)Session["gameTypePanelID"]);

            //Re-Set Appropriate Controls as Active
            if (gameLink == null) return;
            gameLink.CssClass += " active";
            gameLink.GameTypeDropdown.CssClass += " active";
            gameLink.Carrot.Attributes.Remove("class");
            gameLink.Carrot.Attributes.Add("class", "fa fa-caret-up");

            gameLink.Text = gameLink.Text.Replace("fa fa-caret-down", "fa fa-caret-up");

            //Display Appropriate Dropdowns
            gamePanel.Style.Add("display", "block");
            gameTypePanel.Style.Add("display", "block");
        }
    }

    //Bake appropriate cookies, then send to user and reload the page.  See loadCookies() for cookie unboxing and loadMain()
    //for loading of the appropriate main window page.
    protected void gamePageButtonClicked(Object sender, EventArgs e)
    {
        //Get needed Controls
        GamePageButton gamePageButton = (GamePageButton)sender;
        GameLink gameLink = gamePageButton.GameLink;
        Panel gamePanel = (Panel)gamePageButton.Parent;
        Panel gameTypePanel = (Panel)gameLink.Parent;

        //Make required Cookies
        Session["gameLinkID"] = gameLink.ID;
        Session["gameCategoryID"] = gameLink.GameTypeDropdown.ID;
        Session["gamePanelID"] = gamePanel.ID;
        Session["gameTypePanelID"] = gameTypePanel.ID;

        //Save Active Game to be Shown, and remove any savedContent from objectTables.
        Session["activeGame"] = gamePageButton.Game;
        Session.Remove("savedContent");
        if(!gamePageButton.IsCustomPage) Response.Redirect("~/"+gamePageButton.Url);
        else
        {
            Session["customPath"] = Request.ApplicationPath+gamePageButton.Url;
            Response.Redirect("~/Player/CustomPage.aspx");
        }
    }

    //LinkButton to one of the static pages pressed, bake cookie for the appropriate page and redirect.
    protected void navbarButton_Click(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;

        //If Dropdown cookie is still present, eat the cookie since it is no longer wanted.
        Session.Remove("gameLinkID");
        Session.Remove("gameCategoryID");
        Session.Remove("gamePanelID");
        Session.Remove("gameTypePanelID");

        //If there is an activeGame, remove it.
        Session.Remove("activeGame");

        Response.Redirect("~/"+button.ID+".aspx");
    }

    //Logs the user out
    protected void logoutButton_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Login.aspx");
    }

    //Returns the body so I can access it in content pages
    public HtmlGenericControl Body
    {
        get
        {
            return swoleBody;
        }
    }
}