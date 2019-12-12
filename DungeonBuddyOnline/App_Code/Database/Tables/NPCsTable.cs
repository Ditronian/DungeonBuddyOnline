using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NPCsTable
/// </summary>
public class NPCsTable : DBTable
{

    public NPCsTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Inserts the provided NPC into the DB
    public void insertNPC(NPC npc)
    {
        string query = "spInsertNPC";
        SqlParameter[] parameters = new SqlParameter[11];
        parameters[0] = new SqlParameter("gameID", npc.GameID);
        parameters[1] = new SqlParameter("entityName", npc.Name);
        parameters[2] = new SqlParameter("sex", npc.Sex);
        parameters[3] = new SqlParameter("race", npc.Race);
        parameters[4] = new SqlParameter("appearance", npc.Appearance);
        parameters[5] = new SqlParameter("mannerism", npc.Mannerism);
        parameters[6] = new SqlParameter("interactionStyle", npc.InteractionStyle);
        parameters[7] = new SqlParameter("flawSecret", npc.Flaw_secret);
        parameters[8] = new SqlParameter("bio", npc.Bio);
        parameters[9] = new SqlParameter("talent", npc.Talent);
        parameters[10] = new SqlParameter("location", npc.Location);

        SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
        outputVal.Direction = ParameterDirection.Output;

        database.uploadAndReturnCommand(query, outputVal, parameters);
    }

    //Returns a list of all npcs in the game
    public List<NPC> getGameNPCs(int gameID)
    {
        //Make query
        string query = "spGetGameNPCs";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Make sure the database found the encounter, else return an empty encounter
        List<NPC> npcs = new List<NPC>();
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int entityID = (Int32)data.Tables[0].Rows[i]["entityID"];
            int npcID = (Int32)data.Tables[0].Rows[i]["npcID"];
            string name = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["entityName"].ToString());
            if (name.Contains("&#39;")) name = name.Replace("&#39;", "'");
            string race = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["race"].ToString());
            if (race.Contains("&#39;")) race = race.Replace("&#39;", "'");
            string sex = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["sex"].ToString());
            if (sex.Contains("&#39;")) sex = sex.Replace("&#39;", "'");
            string location = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["location"].ToString());
            if (location.Contains("&#39;")) location = location.Replace("&#39;", "'");
            string appearance = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["appearance"].ToString());
            if (appearance.Contains("&#39;")) appearance = location.Replace("&#39;", "'");
            string mannerism = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["mannerism"].ToString());
            if (mannerism.Contains("&#39;")) mannerism = mannerism.Replace("&#39;", "'");
            string interactionStyle = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["interactionStyle"].ToString());
            if (interactionStyle.Contains("&#39;")) interactionStyle = interactionStyle.Replace("&#39;", "'");
            string flawSecret = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["flawSecret"].ToString());
            if (flawSecret.Contains("&#39;")) flawSecret = flawSecret.Replace("&#39;", "'");
            string talent = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["talent"].ToString());
            if (talent.Contains("&#39;")) talent = talent.Replace("&#39;", "'");
            string bio = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["bio"].ToString());
            if (bio.Contains("&#39;")) bio = bio.Replace("&#39;", "'");

            NPC npc = new NPC();
            npc.Name = name;
            npc.EntityID = entityID;
            npc.NpcID = npcID;
            npc.GameID = gameID;
            npc.Sex = sex;
            npc.Race = race;
            npc.Location = location;
            npc.Bio = bio;
            npc.Appearance = appearance;
            npc.Mannerism = mannerism;
            npc.InteractionStyle = interactionStyle;
            npc.Flaw_secret = flawSecret;
            npc.Talent = talent;

            npcs.Add(npc);
        }
        return npcs;
    }


    //Updates the provided NPC in the DB.
    public void updateNPC(NPC npc)
    {
        string query = "spUpdateNPC";
        SqlParameter[] parameters = new SqlParameter[12];
        parameters[0] = new SqlParameter("entityID", npc.EntityID);
        parameters[1] = new SqlParameter("entityName", npc.Name);
        parameters[2] = new SqlParameter("npcID", npc.NpcID);
        parameters[3] = new SqlParameter("race", npc.Race);
        parameters[4] = new SqlParameter("sex", npc.Sex);
        parameters[5] = new SqlParameter("appearance", npc.Appearance);
        parameters[6] = new SqlParameter("mannerism", npc.Mannerism);
        parameters[7] = new SqlParameter("interactionStyle", npc.InteractionStyle);
        parameters[8] = new SqlParameter("flawSecret", npc.Flaw_secret);
        parameters[9] = new SqlParameter("talent", npc.Talent);
        parameters[10] = new SqlParameter("bio", npc.Bio);
        parameters[11] = new SqlParameter("location", npc.Location);

        database.uploadCommand(query, parameters);
    }


    //Deletes a provided NPC and Entity
    public void deleteNPC(NPC npc)
    {
        string query = "spDeleteNPC";
        SqlParameter[] parameters = new SqlParameter[2];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("entityID", npc.EntityID);
        parameters[1] = new SqlParameter("npcID", npc.NpcID);

        database.uploadCommand(query, parameters);
    }

}