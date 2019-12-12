using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GM_MagicShopTool : System.Web.UI.Page
{
    MagicShop shop;
    ObjectTable<MagicItem> itemTable;
    Game game;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
        if (Session["activeGame"] == null) Response.Redirect("~/Home.aspx");

        //Get Active Game
        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        //Load Encounter State if appropriate
        if (Session["savedContent"] != null && ((MagicShop)Session["savedContent"]).GameID != game.GameID) Session.Remove("savedContent");  //Handles if there is savedContent from a wrong game.
        if (Session["savedContent"] == null)  //Handles a fresh load with no saved content
        {
            shop = new MagicShop();
            shop.GameID = game.GameID;
        }
        else    //Handles there is saved content from this game.
        {
            shop = (MagicShop)Session["savedContent"];
            shopNameLabel.Text = "Shop Name: " + shop.ShopName;
        }

        if (!this.IsPostBack) populateEncounterDropdown();
        if (shop.ShopName != "") hiddenShopName.Value = shop.ShopName;

        loadShopTable();
        //Add js resort function on load
        Main masterPage = this.Master as Main;
        masterPage.Body.Attributes.Add("onLoad", "resort(4);");
    }

    //Page_PreRender is the last thing to happen before html is born
    protected void Page_PreRender(object sender, EventArgs e)
    {
        itemTable.restoreValues();
        if(shopQualityDropDownList.SelectedIndex == 0 && shop.ShopQuality != null) shopQualityDropDownList.SelectedValue = shop.ShopQuality;
    }

    //Make ObjectTable of type Entity, passing my entities and the desired parameters
    private void loadShopTable()
    {
        itemTable = new ObjectTable<MagicItem>(shop.Items, new string[] { "Name", "Rarity", "Value", "MinimumValue", "MaximumValue" });
        ShopTablePlaceHolder.Controls.Add(itemTable);
    }

    //Populates the dropdown list with the encounters
    private void populateEncounterDropdown()
    {
        MagicsTable magicTable = new MagicsTable(new DatabaseConnection());
        DataSet data = magicTable.getMagicShops(game.GameID);    //Call to database

        shopDropDownList.DataSource = data;
        shopDropDownList.DataTextField = "shopName";
        shopDropDownList.DataValueField = "shopID";
        shopDropDownList.DataBind();

        shopDropDownList.Items.Insert(0, new ListItem("<Select Shop>", "0"));
    }

    //Adds a magic item to the shop dictionary
    protected void itemButton_Click(object sender, EventArgs e)
    {
        //Check for required field
        if (nameTextBox.Text == String.Empty)
        {
            angryLabel.Text = "New Item requires a name.";
            return;
        }

        //Get inputs
        String name = nameTextBox.Text;
        String rarity = rarityDropDownList.SelectedValue;
        Int32.TryParse(valueTextBox.Text, out int value);
        Int32.TryParse(minimumValueTextBox.Text, out int minimumValue);
        Int32.TryParse(maximumValueTextBox.Text, out int maximumValue);
        
        //Create new Item
        MagicItem item = new MagicItem();
        item.Name = name;
        item.Rarity = rarity;
        item.Value = value;
        item.MinimumValue = minimumValue;
        item.MaximumValue = maximumValue;
        item.Shop = shop;
        //Insert item into Database, and fetch its itemID, so it can be passed around here in code
        itemTable.saveContentChanges();
        shop.Items = itemTable.getContent();
        shop.Items.Add(item, Color.White);      //For now make it white, but realistically it will be determined by rarity.  So have a method for it in MagicItem.

        //Save Content and add to table
        this.Page.Session["savedContent"] = shop;
        itemTable.addRow(item, Color.White);  //For now make it white, but realistically it will be determined by rarity.  So have a method for it in MagicItem.
        if (!itemTable.Rows[0].Visible) itemTable.Rows[0].Visible = true;
    }

    protected void loadButton_Click(object sender, EventArgs e)
    {
        Int32.TryParse(shopDropDownList.SelectedValue, out int shopID);
        //use this to make an encounter and save to session variable

        MagicsTable magicTable = new MagicsTable(new DatabaseConnection());
        shop = magicTable.getMagicShop(shopID);

        List<MagicItem> shopItems = magicTable.getMagicShopItems(shopID);

        shop.Items = new Dictionary<MagicItem, Color>();

        //Add to items dictionary
        foreach (MagicItem item in shopItems)
        {
            //Color color;
            //if (monster.IsFriendly) color = Color.LightGreen;
            //else color = Color.LightSalmon;
            shop.Items.Add(item, Color.White);
        }
        
        //Save to savedContent
        Session["savedContent"] = shop;

        //Reload page to clear any nonsense before loading
        Response.Redirect("MagicShopTool.aspx");
    }

    //Basically resets the page to a fresh load state, and clears all session variables (except activeGame).
    protected void newButton_Click(object sender, EventArgs e)
    {

        //Clear Content from Session Variable
        Session.Remove("savedContent");

        //Reload page to clear any nonsense before loading
        Response.Redirect("MagicShopTool.aspx");
    }

    //Saves entities to the db
    protected void saveButton_Click(object sender, EventArgs e)
    {
        if (shopQualityDropDownList.SelectedIndex == 0)
        {
            angryLabel.Text = "You must select a quality level for the shop.";
            return;
        }

        shop.ShopQuality = shopQualityDropDownList.SelectedValue;

        MagicsTable magicTable = new MagicsTable(new DatabaseConnection());

        itemTable.saveContentChanges();
        shop.Items = itemTable.getContent();
        Session["savedContent"] = shop;

        //Fresh Shop, needs creation first
        if (shop.ShopID == 0)
        {
            shop.ShopName = hiddenShopName.Value;   //Get name from js prompt.

            int shopID = magicTable.insertMagicShop(shop); //Make encounter in db and return id
            if (shopID > 0) shop.ShopID = shopID;   //If valid id, set shopID

            Session["savedContent"] = shop;
        }
        else magicTable.updateMagicShop(shop);

        //Foreach tableRow do the applicable database command (update/insert/delete) to mirror what the user has done in the table.
        foreach (ObjectTableRow objRow in itemTable.ObjectRows)
        {
            MagicItem item = (MagicItem) objRow.Obj;

            if (objRow.Visible == false)
            {
                if (item.MagicItemID != 0) magicTable.deleteShopMagicItem(item); //delete magic item
                shop.Items.Remove(item);
            }
            else if (objRow.Visible == true && item.MagicItemID != 0) magicTable.updateMagicItem(item); //update monster
            else if (objRow.Visible == true && item.MagicItemID == 0) //create monster
            {
                if (item.Shop.ShopID == 0) item.Shop = shop;
                int itemID = magicTable.insertMagicItem(item);
                if (itemID > 0) item.MagicItemID = itemID;
            }
        }


        //Save to savedContent w/ new IDs
        Session["savedContent"] = shop;

        //Reload page to clear any nonsense before loading
        Response.Redirect("MagicShopTool.aspx");

    }

    //Clears the server information, removes encounter and its monsters from the db, Response.Redirect back to same page to clear postback changes
    protected void deleteButton_Click(object sender, EventArgs e)
    {

        if (shop.ShopID != 0)
        {
            //Delete encounter information from database
            MagicsTable magicTable = new MagicsTable(new DatabaseConnection());
            magicTable.deleteMagicShopItems(shop.ShopID);
            magicTable.deleteMagicShop(shop.ShopID);

            //Clear Content from Session Variable
            Session.Remove("savedContent");
        }

        //Reload page to clear any nonsense before loading
        Response.Redirect("MagicShopTool.aspx");
    }

    protected void generateButton_Click(object sender, EventArgs e)
    {
        if(shopQualityDropDownList.SelectedIndex == 0)
        {
            angryLabel.Text = "You must select a quality level for the shop.";
            return;
        }

        shop.ShopQuality = shopQualityDropDownList.SelectedValue;
        //'Remove' all existing items.  Note they are still in memory as it has not yet been saved.
        foreach (ObjectTableRow row in itemTable.ObjectRows)
        {
            Button deleteButton = (Button)row.Cells[row.Cells.Count - 1].Controls[0];
            itemTable.deleteButton_Click(deleteButton, new EventArgs());
        }

        //Determine selected Shop Quality and generate appropriate items
        if (shopQualityDropDownList.SelectedValue == "Common") generateCommonShop();
        else if (shopQualityDropDownList.SelectedValue == "Uncommon") generateUncommonShop();
        else if (shopQualityDropDownList.SelectedValue == "Rare") generateRareShop();
        else if (shopQualityDropDownList.SelectedValue == "Very Rare") generateVeryRareShop();
        else if (shopQualityDropDownList.SelectedValue == "Legendary") generateLegendaryShop();

        if (!itemTable.Rows[0].Visible) itemTable.Rows[0].Visible = true;

        Session["savedContent"] = shop;
    }


    //----------------------------------------------------------------------------//
    //                   SHOP GENERATION PRIVATE METHODS
    //----------------------------------------------------------------------------//


    //Generates items from the Common Magic Item Tables
    private void generateCommonShop(int core = 10, int wonderous = 1)
    {
        if (HttpRuntime.Cache.Get("items") == null) CacheLoader.loadRandomTables();
        MagicItemTable magicItemsTable = (MagicItemTable) HttpRuntime.Cache.Get("items");

        for (int i = 0; i < core; i++)
        {
            MagicItem item = magicItemsTable.getItem("A");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        for (int i = 0; i < wonderous; i++)
        {
            MagicItem item = magicItemsTable.getItem("F");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
    }

    //Generates items from the Uncommon Magic Item Tables
    private void generateUncommonShop(int core = 9, int wonderous = 3)
    {
        if (HttpRuntime.Cache.Get("items") == null) CacheLoader.loadRandomTables();
        MagicItemTable magicItemsTable = (MagicItemTable)HttpRuntime.Cache.Get("items");

        for (int i = 0; i < core; i++)
        {
            MagicItem item = magicItemsTable.getItem("B");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        for (int i = 0; i < wonderous; i++)
        {
            MagicItem item = magicItemsTable.getItem("F");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        generateCommonShop(6, 0);
    }

    //Generates items from the Rare Magic Item Tables
    private void generateRareShop(int core = 8, int wonderous = 2)
    {
        if (HttpRuntime.Cache.Get("items") == null) CacheLoader.loadRandomTables();
        MagicItemTable magicItemsTable = (MagicItemTable)HttpRuntime.Cache.Get("items");

        for (int i = 0; i < core; i++)
        {
            MagicItem item = magicItemsTable.getItem("C");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        for (int i = 0; i < wonderous; i++)
        {
            MagicItem item = magicItemsTable.getItem("G");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        generateUncommonShop(4, 2);
    }

    //Generates items from the Very Rare Magic Item Tables
    private void generateVeryRareShop(int core = 6, int wonderous = 2)
    {
        if (HttpRuntime.Cache.Get("items") == null) CacheLoader.loadRandomTables();
        MagicItemTable magicItemsTable = (MagicItemTable)HttpRuntime.Cache.Get("items");

        for (int i = 0; i < core; i++)
        {
            MagicItem item = magicItemsTable.getItem("D");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        for (int i = 0; i < wonderous; i++)
        {
            MagicItem item = magicItemsTable.getItem("H");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        generateRareShop(4, 2);
    }

    //Generates items from the Legendary Magic Item Tables
    private void generateLegendaryShop(int core = 4, int wonderous = 2)
    {
        if (HttpRuntime.Cache.Get("items") == null) CacheLoader.loadRandomTables();
        MagicItemTable magicItemsTable = (MagicItemTable)HttpRuntime.Cache.Get("items");

        for (int i = 0; i < core; i++)
        {
            MagicItem item = magicItemsTable.getItem("E");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        for (int i = 0; i < wonderous; i++)
        {
            MagicItem item = magicItemsTable.getItem("I");
            item.Shop = shop;
            shop.Items.Add(item, Color.White);
            itemTable.addRow(item, Color.White);
        }
        generateVeryRareShop(3, 2);
    }
}