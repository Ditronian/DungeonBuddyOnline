using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_GameInformationGM : System.Web.UI.Page
{
    Game game;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
        if (Session["activeGame"] == null) Response.Redirect("~/Home.aspx");

        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        if(!this.IsPostBack) loadGame();
    }

    //Last chance to load stuff
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (game.ImagePath == "") gameImage.ImageUrl = "~/Resources/Global/upload.png";
        else gameImage.ImageUrl = "~/"+game.ImagePath;
    }

    //Loads all the details of the current game
    private void loadGame()
    {
        //Check if Im dealing with a fully loaded game object, if not fully load it.
        if(game.FullyLoaded == false)
        {
            GamesTable gamesTable = new GamesTable(new DatabaseConnection());
            game = gamesTable.getGame(game.GameID);
            Session["activeGame"] = game;
        }

        nameTextBox.Text = game.GameName;
        settingTextBox.Text = game.GameSetting;
        descriptionTextBox.Text = game.GameDescription;
        additionalTextBox.Text = game.GameAdditionalInfo;

        if (game.AcceptsPlayers) acceptingPlayersList.SelectedIndex = 0;
        else acceptingPlayersList.SelectedIndex = 1;
    }


    //Saves all the details of the current game
    protected void saveButton_Click(object sender, EventArgs e)
    {
        GamesTable gameTable = new GamesTable(new DatabaseConnection());

        //Check if a name is written
        if (nameTextBox.Text == "")
        {
            angryLabelBottom.Text = "A game name is required!";
            return;
        }

        //Check if game name is alrdy taken, or is equal to the current name
        if (nameTextBox.Text != game.GameName)
        {
            bool alreadyExists = gameTable.checkGameExistsByName(nameTextBox.Text);
            if (alreadyExists)
            {
                angryLabelBottom.Text = "Game Name already taken!";
                nameTextBox.Text = game.GameName;
                return;
            }
        }

        game.GameName = nameTextBox.Text;
        game.GameSetting = settingTextBox.Text;
        game.GameDescription = descriptionTextBox.Text;
        game.GameAdditionalInfo = additionalTextBox.Text;
        if (acceptingPlayersList.SelectedIndex == 0) game.AcceptsPlayers = true;
        else game.AcceptsPlayers = false;

        gameTable.updateGame(game);
        Session["activeGame"] = game;

        //Reload page to clear any nonsense before loading
        Response.Redirect("GameInformationGM.aspx");
    }

    //Deletes everythin there is about the game
    protected void deleteButton_Click(object sender, EventArgs e)
    {
        //Remove all game files
        string folderUrl = Server.MapPath("~/Resources\\") + game.GameID;
        if (Directory.Exists(folderUrl))
        {
            string[] files = Directory.GetFiles(folderUrl, "*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(folderUrl);
        }

        //Delete everything in the database, ragnarok has come.
        GamesTable gamesTable = new GamesTable(new DatabaseConnection());
        gamesTable.deleteGame(game.GameID);

        //Reload to the Home Page
        Session.Remove("activeGame");
        Response.Redirect("~/Home.aspx");
    }


    //Uploads a new image, deletes the old (if applicable), and updates the database
    protected void uploadButton_Click(object sender, EventArgs e)
    {
        string[] acceptedExtensions = new string[] { ".jpg", ".bmp", ".png"};

            if (this.imageUploader.HasFile)
            {
                if (acceptedExtensions.Contains(Path.GetExtension(imageUploader.PostedFile.FileName)))
                {
                    if (imageUploader.PostedFile.ContentLength < 26214400)
                    {
                        string filename = Path.GetFileName(imageUploader.FileName);
                        string folderUrl = Server.MapPath("~/Resources\\") + game.GameID;
                        string url = folderUrl + "\\" + filename;

                        if (!Directory.Exists(folderUrl)) Directory.CreateDirectory(folderUrl);

                        //If image already exists, delete it and overwrite
                        if (File.Exists(url))
                        {
                            File.Delete(url);
                            imageUploader.SaveAs(url);

                            //Resize the image so it fits properly and doesnt take up a ton of space.
                            ImageResizer.ImageBuilder.Current.Build(url, url, new ResizeSettings("maxwidth=200&maxheight=200"));
                        }
                        //Else make the new image, and add it to the database and pages list
                        else
                        {
                            //Delete existing image if it exists
                            if (game.ImagePath != "") File.Delete(Server.MapPath("~/") + game.ImagePath);

                            //Upload new file
                            imageUploader.SaveAs(url);

                            //Set Image Path at end of existing pages
                            game.ImagePath = "Resources/" + game.GameID + "/" + imageUploader.FileName;

                            //Resize the image so it fits properly and doesnt take up a ton of space.
                            ImageResizer.ImageBuilder.Current.Build(url, url, new ResizeSettings("maxwidth=200&maxheight=200"));

                            //Update Game in the db
                            GamesTable gamesTable = new GamesTable(new DatabaseConnection());
                            gamesTable.updateGameImage(game);
                        }
                        angryLabel.ForeColor = System.Drawing.Color.ForestGreen;
                        angryLabel.Text = "Upload successful!";
                    }
                    else
                    {
                        angryLabel.ForeColor = System.Drawing.Color.Red;
                        angryLabel.Text = "Upload failed: The image has to be less than 25 mb!";
                    }
                }
                else
                {
                    angryLabel.ForeColor = System.Drawing.Color.Red;
                    angryLabel.Text = "Upload failed: Only .jpg, .bmp, or .png files are accepted!";
                }
            }
            else
            {
                angryLabel.ForeColor = System.Drawing.Color.Red;
                angryLabel.Text = "Upload failed: You must select an image to upload!";
            }
        
        this.Page.Session["activeGame"] = game;
    }
}