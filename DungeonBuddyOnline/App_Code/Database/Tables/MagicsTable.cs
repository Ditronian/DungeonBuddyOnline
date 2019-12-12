using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MagicsTable
/// </summary>
public class MagicsTable : DBTable
{

    public MagicsTable(DatabaseConnection database)
    {
        this.database = database;
    }

    //Gets all encounters belonging to the provided gameID
    public DataSet getMagicShops(int gameID)
    {
        //Make query
        string query = "spGetMagicShops";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("gameID", gameID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return useful data
        return data;
    }

    //Gets shop by its shopID
    public MagicShop getMagicShop(int shopID)
    {
        //Make query
        string query = "spGetMagicShop";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("shopID", shopID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Make sure the database found the encounter, else return an empty encounter
        MagicShop shop = new MagicShop();
        if (data.Tables[0].Rows.Count == 1)
        {
            string shopName = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["shopName"].ToString());
            if (shopName.Contains("&#39;")) shopName = shopName.Replace("&#39;", "'");
            string shopQuality = HttpUtility.HtmlEncode(data.Tables[0].Rows[0]["shopQuality"].ToString());
            int gameID = (int)data.Tables[0].Rows[0]["gameID"];

            shop.ShopID = shopID;
            shop.ShopName = shopName;
            shop.ShopQuality = shopQuality;
            shop.GameID = gameID;
        }

        //Return (hopefully) useful data
        return shop;
    }


    //Gets all items belonging to the provided shopID
    public List<MagicItem> getMagicShopItems(int shopID)
    {
        //Make query
        string query = "spGetMagicShopItems";

        //Obtain Parameters
        SqlParameter[] parameters = new SqlParameter[1];
        parameters[0] = new SqlParameter("shopID", shopID);

        //Retrieve Data
        DataSet data = database.downloadCommand(query, parameters);

        //Return useful data
        List<MagicItem> items = new List<MagicItem>();

        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int magicItemID = (Int32)data.Tables[0].Rows[i]["magicItemID"];
            string name = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["magicItemName"].ToString());
            if (name.Contains("&#39;")) name = name.Replace("&#39;", "'");
            string rarity = HttpUtility.HtmlEncode(data.Tables[0].Rows[i]["rarity"].ToString());
            if (rarity.Contains("&#39;")) rarity = name.Replace("&#39;", "'");
            int rolledValue = (Int32)data.Tables[0].Rows[i]["rolledValue"];
            int minimumValue = (Int32)data.Tables[0].Rows[i]["minimumValue"];
            int maximumValue = (Int32)data.Tables[0].Rows[i]["maximumValue"];

            MagicItem item = new MagicItem();
            item.MagicItemID = magicItemID;
            item.Name = name;
            item.Rarity = rarity;
            item.Value = rolledValue;
            item.MinimumValue = minimumValue;
            item.MaximumValue = maximumValue;

            items.Add(item);
        }
        return items;
    }


    //Inserts the provided Shop
    public int insertMagicShop(MagicShop shop)
    {
        string query = "spInsertMagicShop";
        SqlParameter[] parameters = new SqlParameter[3];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("gameID", shop.GameID);
        parameters[1] = new SqlParameter("shopName", shop.ShopName);
        parameters[2] = new SqlParameter("shopQuality", shop.ShopQuality);

        SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
        outputVal.Direction = ParameterDirection.Output;

        return database.uploadAndReturnCommand(query, outputVal, parameters);
    }


    //Deletes a provided Monster/Entity
    public void deleteShopMagicItem(MagicItem item)
    {
        string query = "spDeleteMagicItem";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("magicItemID", item.MagicItemID);

        database.uploadCommand(query, parameters);
    }


    //Updates the provided Item in the DB.
    public void updateMagicItem(MagicItem item)
    {
        string query = "spUpdateMagicItem";
        SqlParameter[] parameters = new SqlParameter[6];
        parameters[0] = new SqlParameter("magicItemID", item.MagicItemID);
        parameters[1] = new SqlParameter("magicItemName", item.Name);
        parameters[2] = new SqlParameter("rarity", item.Rarity);
        parameters[3] = new SqlParameter("rolledValue", item.Value);
        parameters[4] = new SqlParameter("minimumValue", item.MinimumValue);
        parameters[5] = new SqlParameter("maximumValue", item.MaximumValue);

        database.uploadCommand(query, parameters);
    }


    //Updates the provided Shop in the DB.
    public void updateMagicShop(MagicShop shop)
    {
        string query = "spUpdateMagicShop";
        SqlParameter[] parameters = new SqlParameter[2];
        parameters[0] = new SqlParameter("shopID", shop.ShopID);
        parameters[1] = new SqlParameter("shopQuality", shop.ShopQuality);

        database.uploadCommand(query, parameters);
    }


    //Inserts the provided item into the DB and returns its id
    public int insertMagicItem(MagicItem item)
    {
        string query = "spInsertMagicItem";
        SqlParameter[] parameters = new SqlParameter[6];
        parameters[0] = new SqlParameter("magicItemName", item.Name);
        parameters[1] = new SqlParameter("shopID", item.Shop.ShopID);
        parameters[2] = new SqlParameter("rarity", item.Rarity);
        parameters[3] = new SqlParameter("rolledValue", item.Value);
        parameters[4] = new SqlParameter("minimumValue", item.MinimumValue);
        parameters[5] = new SqlParameter("maximumValue", item.MaximumValue);

        SqlParameter outputVal = new SqlParameter("@outputID", SqlDbType.Int);
        outputVal.Direction = ParameterDirection.Output;

        return database.uploadAndReturnCommand(query, outputVal, parameters);
    }


    //Deletes all MagicItems with the provided shop
    public void deleteMagicShopItems(int shopID)
    {
        string query = "spDeleteMagicShopItems";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("shopID", shopID);

        database.uploadCommand(query, parameters);
    }


    //Deletes the provided Shop
    public void deleteMagicShop(int shopID)
    {
        string query = "spDeleteMagicShop";
        SqlParameter[] parameters = new SqlParameter[1];      //Add 1 sql parameter
        parameters[0] = new SqlParameter("shopID", shopID);

        database.uploadCommand(query, parameters);
    }
}