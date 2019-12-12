using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EncountersTable
/// </summary>
public class EncountersTable : DBTable
{

    public EncountersTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Gets all encounters belonging to the provided gameID
    public DataSet getEncounters(int gameID)
    {
        //Make query
        string query = "spGetEncounters";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return useful data
        return data;
    }

    //Gets encounter by its encounterID
    public Encounter getEncounter(int encounterID)
    {
        //Make query
        string query = "spGetEncounter";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("encounterID", encounterID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Make sure the database found the encounter, else return an empty encounter
        Encounter encounter = new Encounter();
        if (data.Tables[0].Rows.Count == 1)
        {
            string encounterName = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["encounterName"].ToString());
            if (encounterName.Contains("&#39;")) encounterName = encounterName.Replace("&#39;", "'");
            int gameID = (int) data.Tables[0].Rows[0]["gameID"];

            encounter.EncounterID = encounterID;
            encounter.EncounterName = encounterName;
            encounter.GameID = gameID;
        }

        //Return (hopefully) useful data
        return encounter;
    }

    //Deletes the provided Encounter
    public void deleteEncounter(int encounterID)
    {
        string query = "spDeleteEncounter";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("encounterID", encounterID);

        database.uploadCommand(query, parameters);
    }

    //Inserts the provided Encounter
    public int insertEncounter(Encounter encounter)
    {
        string query = "spInsertEncounter";
        SqlParameter[] parameters = new SqlParameter[2];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("gameID", encounter.GameID);
        parameters[1] = new SqlParameter("encounterName", encounter.EncounterName);

        SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
        outputVal.Direction = ParameterDirection.Output;

        return database.uploadAndReturnCommand(query, outputVal, parameters);
    }
}