using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MonstersTable
/// </summary>
public class MonstersTable : DBTable
{

    public MonstersTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Gets all monsters belonging to the provided encounterID
    public List<Monster> getEncounterMonsters(int encounterID)
    {
        //Make query
        string query = "spGetEncounterMonsters";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("encounterID", encounterID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return useful data
        List<Monster> monsters = new List<Monster>();

        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int entityID = (Int32)data.Tables[0].Rows[i]["entityID"];
            string name = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["entityName"].ToString());
            if (name.Contains("&#39;")) name = name.Replace("&#39;", "'");
            int initiative = (Int32)data.Tables[0].Rows[i]["initiative"];
            int armorClass = (Int32)data.Tables[0].Rows[i]["armorClass"];
            int currentHP = (Int32)data.Tables[0].Rows[i]["currentHP"];
            int maxHP = (Int32)data.Tables[0].Rows[i]["maxHP"];

            int monsterID = (Int32)data.Tables[0].Rows[i]["monsterID"];
            bool isFriendly = (bool)data.Tables[0].Rows[i]["isFriendly"];

            Monster monster = new Monster();
            monster.EntityID = entityID;
            monster.Name = name;
            monster.Initiative = initiative;
            monster.ArmorClass = armorClass;
            monster.CurrentHP = currentHP;
            monster.MaxHP = maxHP;

            monster.MonsterID = monsterID;
            monster.IsFriendly = isFriendly;

            monsters.Add(monster);
        }
        return monsters;
    }

    //Deletes all Monsters and Entities associated with the provided encounter
    public void deleteEncounterMonsters(int encounterID)
    {
        string query = "spDeleteEncounterMonsters";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("encounterID", encounterID);

        database.uploadCommand(query, parameters);
    }

    //Deletes a provided Monster/Entity
    public void deleteMonster(Monster monster)
    {
        string query = "spDeleteMonster";
        SqlParameter[] parameters = new SqlParameter[2];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("entityID", monster.EntityID);
        parameters[1] = new SqlParameter("monsterID", monster.MonsterID);

        database.uploadCommand(query, parameters);
    }

    //Updates the provided Monster in the DB.
    public void updateMonster(Monster monster)
    {
        string query = "spUpdateMonster";
        SqlParameter[] parameters = new SqlParameter[6];
        parameters[0] = new SqlParameter("entityID", monster.EntityID);
        parameters[1] = new SqlParameter("entityName", monster.Name);
        parameters[2] = new SqlParameter("initiative", monster.Initiative);
        parameters[3] = new SqlParameter("currentHP", monster.CurrentHP);
        parameters[4] = new SqlParameter("maxHP", monster.MaxHP);
        parameters[5] = new SqlParameter("armorClass", monster.ArmorClass);

        database.uploadCommand(query, parameters);
    }

    //Inserts the provided Monster into the DB and returns its id
    public int insertMonster(Monster monster)
    {
        string query = "spInsertMonster";
        SqlParameter[] parameters = new SqlParameter[8];
        parameters[0] = new SqlParameter("gameID", monster.GameID);
        parameters[1] = new SqlParameter("encounterID", monster.Encounter.EncounterID);
        parameters[2] = new SqlParameter("initiative", monster.Initiative);
        parameters[3] = new SqlParameter("currentHP", monster.CurrentHP);
        parameters[4] = new SqlParameter("maxHP", monster.MaxHP);
        parameters[5] = new SqlParameter("armorClass", monster.ArmorClass);
        parameters[6] = new SqlParameter("isFriendly", monster.IsFriendly);
        parameters[7] = new SqlParameter("entityName", monster.Name);

        SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
        outputVal.Direction = ParameterDirection.Output;

        return database.uploadAndReturnCommand(query, outputVal, parameters);
    }

    //Just returns a monster's entityID
    public int getMonsterEntityID(int monsterID)
    {
        string query = "spGetMonsterEntityID";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("monsterID", monsterID);

        DataSet data = database.downloadCommand(query, parameters);

        int entityID = 0;
        if (data.Tables[0].Rows.Count == 1)
        {
            entityID = (int)data.Tables[0].Rows[0]["entityID"];
        }
        return entityID;
    }

}