using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PartyMembersTable
/// </summary>
public class PartyMembersTable : DBTable
{

    public PartyMembersTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Gets a specific party member by its id
    public void getPartyMember(int partyMemberID)
    {
        //Make query
        string query = "spGetPartyMemberByID";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("partyMemberID", partyMemberID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return useful data
    }

    public Party getParty(int gameID)
    {
        //Make query
        string query = "spGetParty";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Make sure the database found the encounter, else return an empty encounter
        Party party = new Party();
        party.GameID = gameID;
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int entityID = (Int32)data.Tables[0].Rows[i]["entityID"];
            string name = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["entityName"].ToString());
            if (name.Contains("&#39;")) name = name.Replace("&#39;", "'");
            string race = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["race"].ToString());
            if (race.Contains("&#39;")) race = race.Replace("&#39;", "'");
            int armorClass = (Int32)data.Tables[0].Rows[i]["armorClass"];
            int currentHP = (Int32)data.Tables[0].Rows[i]["currentHP"];
            int maxHP = (Int32)data.Tables[0].Rows[i]["maxHP"];

            int partyMemberID = (Int32)data.Tables[0].Rows[i]["partyMemberID"];
            int userID = (Int32)data.Tables[0].Rows[i]["userID"];
            bool isNPC = (bool)data.Tables[0].Rows[i]["isNpc"];
            int passivePerception = (Int32)data.Tables[0].Rows[i]["passivePerception"];
            char size = data.Tables[0].Rows[i]["size"].ToString().ElementAtOrDefault(0);

            PartyMember partyMember = new PartyMember();
            partyMember.Name = name;
            partyMember.EntityID = entityID;
            partyMember.Race = race;
            partyMember.GameID = gameID;
            partyMember.CurrentHP = currentHP;
            partyMember.MaxHP = maxHP;
            partyMember.ArmorClass = armorClass;

            partyMember.Perception = passivePerception;
            partyMember.Size = size;
            partyMember.IsNpc = isNPC;
            partyMember.PartyMemberID = partyMemberID;
            partyMember.UserID = userID;

            Color color;
            if (isNPC) color = Color.LightGreen;
            else color = Color.LightBlue;

            party.PartyMembers.Add(partyMember, color);
        }
        return party;
    }


    //Inserts the provided partymember into the DB and returns its id
    public int insertPartyMember(PartyMember partyMember)
    {
        string query = "spInsertPartyMember";
        SqlParameter[] parameters = new SqlParameter[10];
        parameters[0] = new SqlParameter("gameID", partyMember.GameID);
        parameters[1] = new SqlParameter("passivePerception", partyMember.Perception);
        parameters[2] = new SqlParameter("currentHP", partyMember.CurrentHP);
        parameters[3] = new SqlParameter("maxHP", partyMember.MaxHP);
        parameters[4] = new SqlParameter("armorClass", partyMember.ArmorClass);
        parameters[5] = new SqlParameter("isNpc", partyMember.IsNpc);
        parameters[6] = new SqlParameter("entityName", partyMember.Name);
        parameters[7] = new SqlParameter("race", partyMember.Race);
        parameters[8] = new SqlParameter("size", partyMember.Size);
        parameters[9] = new SqlParameter("userID", partyMember.UserID);

        SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
        outputVal.Direction = ParameterDirection.Output;

        return database.uploadAndReturnCommand(query, outputVal, parameters);
    }


    //Updates the provided PartyMember in the DB.
    public void updatePartyMember(PartyMember partyMember)
    {
        string query = "spUpdatePartyMember";
        SqlParameter[] parameters = new SqlParameter[9];
        parameters[0] = new SqlParameter("entityID", partyMember.EntityID);
        parameters[1] = new SqlParameter("entityName", partyMember.Name);
        parameters[2] = new SqlParameter("partyMemberID", partyMember.PartyMemberID);
        parameters[3] = new SqlParameter("currentHP", partyMember.CurrentHP);
        parameters[4] = new SqlParameter("maxHP", partyMember.MaxHP);
        parameters[5] = new SqlParameter("armorClass", partyMember.ArmorClass);
        parameters[6] = new SqlParameter("passivePerception", partyMember.Perception);
        parameters[7] = new SqlParameter("race", partyMember.Race);
        parameters[8] = new SqlParameter("size", partyMember.Size);

        database.uploadCommand(query, parameters);
    }


    //Deletes a provided PartyMember and Entity
    public void deletePartyMember(PartyMember partyMember)
    {
        string query = "spDeletePartyMember";
        SqlParameter[] parameters = new SqlParameter[2];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("entityID", partyMember.EntityID);
        parameters[1] = new SqlParameter("partyMemberID", partyMember.PartyMemberID);

        database.uploadCommand(query, parameters);
    }

    //Just returns a partyMembers's entityID
    public int getPartyMemberEntityID(int partyMemberID)
    {
        string query = "spGetPartyMemberEntityID";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("partyMemberID", partyMemberID);

        DataSet data = database.downloadCommand(query, parameters);

        int entityID = 0;
        if (data.Tables[0].Rows.Count == 1)
        {
            entityID = (int)data.Tables[0].Rows[0]["entityID"];
        }
        return entityID;
    }
}