using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PagesTable
/// </summary>
public class PagesTable : DBTable
{
    public PagesTable(DatabaseConnection database)
    {
        this.database = database;
    }


    //Gets all custom pages belonging to this game
    public CustomPageList getGamePages(int gameID)
    {
        //Make query
        string query = "spGetGameCustomPages";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Make sure the database found the encounter, else return an empty encounter
        CustomPageList pages = new CustomPageList();
        pages.GameID = gameID;

        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int pageID = (Int32)data.Tables[0].Rows[i]["pageID"];
            int sortIndex = (Int32)data.Tables[0].Rows[i]["sortIndex"];
            string pageName = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["pageName"].ToString());
            if (pageName.Contains("&#39;")) pageName = pageName.Replace("&#39;", "'");
            string pageURL = data.Tables[0].Rows[i]["pageURL"].ToString();

            CustomPage page = new CustomPage();
            page.PageID = pageID;
            page.GameID = gameID;
            page.SortIndex = sortIndex;
            page.PageName = pageName;
            page.PageURL = pageURL;

            pages.Pages.Add(page.SortIndex, page);
        }
        return pages;
    }

    //Deletes the provided custom page
    public void deleteCustomPage(int pageID)
    {
        string query = "spDeleteCustomPage";
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("pageID", pageID);

        database.uploadCommand(query, parameters);
    }

    //Inserts the provided custom page
    public int insertCustomPage(CustomPage page)
    {
        string query = "spInsertCustomPage";
        SqlParameter[] parameters = new SqlParameter[4];
        parameters[0] = new SqlParameter("gameID", page.GameID);
        parameters[1] = new SqlParameter("pageName", page.PageName);
        parameters[2] = new SqlParameter("pageURL", page.PageURL);
        parameters[3] = new SqlParameter("sortIndex", page.SortIndex);

        SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
        outputVal.Direction = ParameterDirection.Output;

        return database.uploadAndReturnCommand(query, outputVal, parameters);
    }


    //Updates the provided custom page in the DB.
    public void updateCustomPage(CustomPage page)
    {
        string query = "spUpdateCustomPage";
        SqlParameter[] parameters = new SqlParameter[3];
        parameters[0] = new SqlParameter("pageName", page.PageName);
        parameters[1] = new SqlParameter("sortIndex", page.SortIndex);
        parameters[2] = new SqlParameter("pageID", page.PageID);

        database.uploadCommand(query, parameters);
    }
}