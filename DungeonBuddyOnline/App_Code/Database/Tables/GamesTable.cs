using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GamesTable
/// </summary>
public class GamesTable : DBTable
{

    public GamesTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Gets a specific Game by its id
    public Game getGame(int gameID)
    {
        //Make query
        string query = "spGetGame";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return useful data
        string gameName = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["gameName"].ToString());
        if (gameName.Contains("&#39;")) gameName = gameName.Replace("&#39;", "'");
        string gameSetting = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["gameSetting"].ToString());
        if (gameSetting.Contains("&#39;")) gameSetting = gameSetting.Replace("&#39;", "'");
        string gameDescription = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["gameDescription"].ToString());
        if (gameDescription.Contains("&#39;")) gameDescription = gameDescription.Replace("&#39;", "'");
        string gameAdditionalInfo = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["gameAdditionalInfo"].ToString());
        if (gameAdditionalInfo.Contains("&#39;")) gameAdditionalInfo = gameAdditionalInfo.Replace("&#39;", "'");
        string imagePath = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["imagePath"].ToString());
        if (imagePath.Contains("&#39;")) imagePath = imagePath.Replace("&#39;", "'");
        bool gameAcceptsPlayers = (bool)data.Tables[0].Rows[0]["gameAcceptsPlayers"];
        DateTime createdDate = (DateTime)data.Tables[0].Rows[0]["createdDate"];

        Game game = new Game();
        game.GameID = gameID;
        game.GameName = gameName;
        game.GameSetting = gameSetting;
        game.GameDescription = gameDescription;
        game.GameAdditionalInfo = gameAdditionalInfo;
        game.ImagePath = imagePath;
        game.AcceptsPlayers = gameAcceptsPlayers;
        game.FullyLoaded = true;
        game.CreatedDate = createdDate;

        return game;
    }

    //Gets all games GMed by the provided userID
    public List<Game> getGMGames(int userID)
    {
        //Make query
        string query = "spGetGMGames";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("userID", userID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Assemble the List
        List<Game> games = new List<Game>();

        //Return useful data
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int gameID = (Int32)data.Tables[0].Rows[i]["gameID"];
            string gameName = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["gameName"].ToString());
            if (gameName.Contains("&#39;")) gameName = gameName.Replace("&#39;", "'");

            Game game = new Game();
            game.GameID = gameID;
            game.GameName = gameName;
   
            games.Add(game);
        }

        return games;
    }

    //Gets all games played by the provided userID
    public List<Game> getPlayerGames(int userID)
    {
        //Make query
        string query = "spGetPlayerGames";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("userID", userID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Assemble the List
        List<Game> games = new List<Game>();

        //Return useful data
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int gameID = (Int32)data.Tables[0].Rows[i]["gameID"];
            string gameName = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["gameName"].ToString());
            if (gameName.Contains("&#39;")) gameName = gameName.Replace("&#39;", "'");

            Game game = new Game();
            game.GameID = gameID;
            game.GameName = gameName;

            games.Add(game);
        }

        return games;
    }

    //Gets all games that are available to join
    public List<Game> getJoinableGames()
    {
        //Make query
        string query = "spGetJoinableGames";

        //Retrieve Data
        DataSet data = database.downloadCommand(query);

        //Assemble the List
        List<Game> games = new List<Game>();

        //Return useful data
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int gameID = (Int32)data.Tables[0].Rows[i]["gameID"];
            string gameName = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["gameName"].ToString());
            if (gameName.Contains("&#39;")) gameName = gameName.Replace("&#39;", "'");
            string gameSetting = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["gameSetting"].ToString());
            if (gameSetting.Contains("&#39;")) gameSetting = gameSetting.Replace("&#39;", "'");

            Game game = new Game();
            game.GameID = gameID;
            game.GameName = gameName;
            game.GameSetting = gameSetting;

            games.Add(game);
        }

        return games;
    }

    //Inserts the provided game and the GM's id
    public void insertGame(Game game, int userID)
    {
        string query = "spInsertGame";
        SqlParameter[] parameters = new SqlParameter[5];
        parameters[0] = new SqlParameter("gameName", game.GameName);
        parameters[1] = new SqlParameter("gameSetting", game.GameSetting);
        parameters[2] = new SqlParameter("gameAcceptsPlayers", game.AcceptsPlayers);
        parameters[3] = new SqlParameter("userID", userID);
        parameters[4] = new SqlParameter("createdDate", DateTime.Now);


        database.uploadCommand(query, parameters);
    }

    //Gets all games GMed by the provided userID
    public bool checkGameExistsByName(string gameName)
    {
        //Make query
        string query = "spGetGameByName";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameName", gameName);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return whether or not the db found any games with the provided name.
        if (data.Tables[0].Rows.Count >= 1) return true;
        else return false;
    }


    //Updates the provided Game's image path in the DB.
    public void updateGameImage(Game game)
    {
        string query = "spUpdateGameImage";
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("gameID", game.GameID);
        parameters[1] = new SqlParameter("imagePath", game.ImagePath);

        database.uploadCommand(query, parameters);
    }

    //Updates the provided Game in the DB.
    public void updateGame(Game game)
    {
        string query = "spUpdateGame";
        SqlParameter[] parameters = new SqlParameter[6];
        parameters[0] = new SqlParameter("gameID", game.GameID);
        parameters[1] = new SqlParameter("gameName", game.GameName);
        parameters[2] = new SqlParameter("gameSetting", game.GameSetting);
        parameters[3] = new SqlParameter("gameDescription", game.GameDescription);
        parameters[4] = new SqlParameter("gameAdditionalInfo", game.GameAdditionalInfo);
        parameters[5] = new SqlParameter("gameAcceptsPlayers", game.AcceptsPlayers);

        database.uploadCommand(query, parameters);
    }

    //Deletes the provided Game in its entirety.  Very catacylsmic.
    public void deleteGame(int gameID)
    {
        string query = "spDeleteGame";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("gameID", gameID);

        database.uploadCommand(query, parameters);
    }
}