using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsersTable
/// </summary>
public class UsersTable : DBTable
{
 
    public UsersTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Inserts a new user.
    public void insertUser(User user)
    {
        string query = "spInsertUser";
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("userName", user.UserName);
        parameters[1] = new SqlParameter("password", user.Password);

        database.uploadCommand(query, parameters);
    }   

    //Gets a specific user using the passed parameters
    public int authenticateUser(User user)
    {
        //Make query
        string query = "spAuthenticateUser";
        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("userName", user.UserName);    //Set sql parameter 1 (with sql name of "userID"), with a value of userID
        parameters[1] = new SqlParameter("password", user.Password);    //Set sql parameter 1 (with sql name of "userID"), with a value of userID

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return whether or not the db found the user by returning the userID or 0.
        if (data.Tables[0].Rows.Count == 1) return (Int32)data.Tables[0].Rows[0]["userID"];
        else return 0;
    }

    //Inserts the provided game and the GM's id
    public void insertJoinRequest(int userID, int gameID, int partyMemberID)
    {
        string query = "spInsertJoinRequest";
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("userID", userID);
        parameters[1] = new SqlParameter("gameID", gameID);
        parameters[2] = new SqlParameter("partyMemberID", partyMemberID);


        database.uploadCommand(query, parameters);
    }

    //Gets all games that are available to join
    public List<JoinRequest> getJoinRequests(int gameID)
    {
        //Make query
        string query = "spGetJoinRequests";

        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Assemble the List
        List<JoinRequest> requests = new List<JoinRequest>();

        //Return useful data
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            string userName = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["userName"].ToString());
            string characterName = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["entityName"].ToString());
            if (characterName.Contains("&#39;")) characterName = characterName.Replace("&#39;", "'");
            string race = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["race"].ToString());
            if (race.Contains("&#39;")) race = race.Replace("&#39;", "'");
            int passivePerception = (Int32)data.Tables[0].Rows[i]["passivePerception"];
            int hp = (Int32)data.Tables[0].Rows[i]["maxHP"];
            char size = data.Tables[0].Rows[i]["size"].ToString().ElementAtOrDefault(0);
            int partyMemberID = (Int32)data.Tables[0].Rows[i]["partyMemberID"];
            int entityID = (Int32)data.Tables[0].Rows[i]["entityID"];
            int userID = (Int32)data.Tables[0].Rows[i]["userID"];

            JoinRequest request = new JoinRequest();
            request.PartyMember = new PartyMember();
            request.GameID = gameID;
            request.User = new User();
            request.PartyMember.Name = characterName;
            request.PartyMember.Race = race;
            request.PartyMember.Perception = passivePerception;
            request.PartyMember.MaxHP = hp;
            request.PartyMember.Size = size;
            request.PartyMember.PartyMemberID = partyMemberID;
            request.PartyMember.EntityID = entityID;
            request.PartyMember.UserID = userID;
            request.User.UserID = userID;
            request.User.UserName = userName;

            requests.Add(request);
        }

        return requests;
    }

    //Accepts the provided join request in the database
    public void acceptJoinRequest(JoinRequest request)
    {
        string query = "spAcceptJoinRequest";
        SqlParameter[] parameters = new SqlParameter[4];
        parameters[0] = new SqlParameter("userID", request.User.UserID);
        parameters[1] = new SqlParameter("gameID", request.GameID);
        parameters[2] = new SqlParameter("partyMemberID", request.PartyMember.PartyMemberID);
        parameters[3] = new SqlParameter("entityID", request.PartyMember.EntityID);

        database.uploadCommand(query, parameters);
    }

    //Denies the provided join request in the database
    public void denyJoinRequest(JoinRequest request)
    {
        string query = "spDenyJoinRequest";
        SqlParameter[] parameters = new SqlParameter[4];
        parameters[0] = new SqlParameter("userID", request.User.UserID);
        parameters[1] = new SqlParameter("gameID", request.GameID);
        parameters[2] = new SqlParameter("partyMemberID", request.PartyMember.PartyMemberID);
        parameters[3] = new SqlParameter("entityID", request.PartyMember.EntityID);

        database.uploadCommand(query, parameters);
    }

    //Inserts the User's access to the game
    public void insertUserPlayerGame(int userID, int gameID)
    {
        string query = "spInsertUserPlayerGame";
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("userID", userID);
        parameters[1] = new SqlParameter("gameID", gameID);

        database.uploadCommand(query, parameters);
    }

    //Deletes the User's access to the game
    public void deleteUserPlayerGame(int userID, int gameID)
    {
        string query = "spDeleteUserPlayerGame";
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("userID", userID);
        parameters[1] = new SqlParameter("gameID", gameID);

        database.uploadCommand(query, parameters);
    }

    //Gets whether the userID/gameID combo already exists
    public bool getUserPlayerGame(int userID, int gameID)
    {
        //Make query
        string query = "spGetUserPlayerGame";
        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("userID", userID);
        parameters[1] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return whether or not the db found the user by returning the userID or 0.
        if (data.Tables[0].Rows.Count >= 1) return true;
        else return false;
    }

    //Checks if Username exists in the db
    public bool checkUsername(string username)
    {
        string query = "spCheckUsername";
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("Username", username);

        DataSet data = database.downloadCommand(query, parameters);

        if (data.Tables[0].Rows.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Deletes the provided User.
    public void deleteUser(int userID)
    {
        string query = "spDeleteUser";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("userID", userID);

        database.uploadCommand(query, parameters);
    }
}