using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_EncounterTool : System.Web.UI.Page
{
    Encounter encounter;
    ObjectTable<Entity> combatTable;
    Game game;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
        if (Session["activeGame"] == null) Response.Redirect("~/Home.aspx");

        //Get Active Game
        game = (Game) Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        //Load Encounter State if appropriate
        if (Session["savedContent"] != null && ((Encounter)Session["savedContent"]).GameID != game.GameID) Session.Remove("savedContent");  //Handles if there is savedContent from a wrong game.
        if (Session["savedContent"] == null)  //Handles a fresh load with no saved content
        {
            encounter = new Encounter();
            encounter.GameID = game.GameID;
            loadParty();
        }   
        else encounter = (Encounter)Session["savedContent"];   //Handles there is saved content from this game.

        if (!this.IsPostBack) populateEncounterDropdown();
        if (encounter.EncounterName != "")
        {
            hiddenEncounterName.Value = encounter.EncounterName;
            encounterNameLabel.Text = "Encounter Name: " + encounter.EncounterName;
        }
        loadCombatTable();
    }

    //Note to me:  Page_PreRender is the last thing to happen before html is born, Without this PostBacks will wreck my table's textboxes.
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //combatTable.restoreValues();

        //Add js resort function on load
        Main masterPage = this.Master as Main;
        masterPage.Body.Attributes.Add("onLoad", "resort(1);");
    }

    //Make ObjectTable of type Entity, passing my entities and the desired parameters
    private void loadCombatTable()
    {
        combatTable = new ObjectTable<Entity>(encounter.Entities, new string[] { "Name", "Initiative", "CurrentHP", "MaxHP", "ArmorClass" });
        CombatTablePlaceHolder.Controls.Add(combatTable);
    }

    //Loads the party from the database.
    private void loadParty()
    {
        PartyMembersTable partyTable = new PartyMembersTable(new DatabaseConnection());
        Party party = partyTable.getParty(game.GameID);

        foreach (PartyMember partyMember in party.PartyMembers.Keys)
        {
            Color color;
            if (partyMember.IsNpc) color = Color.LightGreen;
            else color = Color.LightBlue;
            encounter.Entities.Add(partyMember, color);
        }
    }


    //Populates the dropdown list with the encounters
    private void populateEncounterDropdown()
    {
        EncountersTable encounterTable = new EncountersTable(new DatabaseConnection());
        DataSet data = encounterTable.getEncounters(game.GameID);    //Call to database

        encounterDropDownList.DataSource = data;
        encounterDropDownList.DataTextField = "encounterName";
        encounterDropDownList.DataValueField = "encounterID";
        encounterDropDownList.DataBind();

        encounterDropDownList.Items.Insert(0, new ListItem("<Select Encounter>", "0"));
    }

    //Adds a monster to the entities dictionary
    protected void monsterButton_Click(object sender, EventArgs e)
    {
        //Check for required field
        if(monsterNameTextBox.Text == String.Empty)
        {
            angryLabel.Text = "New Creatures require a name.";
            return;
        }

        //Get inputs
        String originalName = monsterNameTextBox.Text;
        Int32.TryParse(monsterInitiativeTextBox.Text, out int initiative);
        Int32.TryParse(monsterCurrentHPTextBox.Text, out int currentHP);
        Int32.TryParse(monsterMaxHPTextBox.Text, out int maxHP);
        Int32.TryParse(monsterACTextBox.Text, out int ac);
        String actualName = monsterNameTextBox.Text;

        //Decide if monster needs a suffix to distinguish multiple creatures with the same name
        HashSet<String> names = new HashSet<string>();
        foreach (ObjectTableRow objRow in combatTable.ObjectRows) if (objRow.Visible == true && objRow.Obj != null) names.Add(((Entity) objRow.Obj).Name);
        int i = 2;
        while (names.Contains(actualName))
        {
            actualName = originalName + " (" + i + ")";
            i++;
        }

        //Create new hostile monster
        Monster monster = new Monster();
        monster.Name = actualName;
        monster.Initiative = initiative;
        monster.MaxHP = maxHP;
        monster.CurrentHP = currentHP;
        monster.ArmorClass = ac;
        monster.GameID = game.GameID;
        monster.Encounter = encounter;
        monster.IsFriendly = false;
        //Insert monster into Database, and fetch its entity/monster IDs, so they can be passed around here in code
        //Make sure the monster with id is added not OG monster without IDs.  Important so we can delete/update later as needed.
        combatTable.saveContentChanges();
        encounter.Entities = combatTable.getContent();
        encounter.Entities.Add(monster, Color.LightSalmon);

        //Save Content and add to table
        this.Page.Session["savedContent"] = encounter;
        combatTable.addRow(monster, Color.LightSalmon);
        if (!combatTable.Rows[0].Visible) combatTable.Rows[0].Visible = true;
    }

    //Adds a friendly to the entities dictionary
    protected void friendlyButton_Click(object sender, EventArgs e)
    {
        //Check for required field
        if (friendlyNameTextBox.Text == String.Empty)
        {
            angryLabel.Text = "New Creatures require a name.";
            return;
        }

        //Get inputs
        String originalName = friendlyNameTextBox.Text;
        Int32.TryParse(friendlyInitiativeTextBox.Text, out int initiative);
        Int32.TryParse(friendlyCurrentHPTextBox.Text, out int currentHP);
        Int32.TryParse(friendlyMaxHPTextBox.Text, out int maxHP);
        Int32.TryParse(friendlyACTextBox.Text, out int ac);
        String actualName = friendlyNameTextBox.Text;

        //Decide if monster needs a suffix to distinguish multiple creatures with the same name
        HashSet<String> names = new HashSet<string>();
        foreach (Entity entity in encounter.Entities.Keys) if (encounter.Entities[entity] != Color.Empty) names.Add(entity.Name);
        int i = 2;
        while (names.Contains(actualName))
        {
            actualName = originalName + " (" + i + ")";
            i++;
        }

        //Create new friendly monster
        Monster monster = new Monster();
        monster.Name = actualName;
        monster.Initiative = initiative;
        monster.MaxHP = maxHP;
        monster.CurrentHP = currentHP;
        monster.ArmorClass = ac;
        monster.GameID = game.GameID;
        monster.Encounter = encounter;
        monster.IsFriendly = true;
        //Insert monster into Database, and fetch its entity/monster IDs, so they can be passed around here in code
        //Make sure the monster with id is added not OG monster without IDs.  Important so we can delete/update later as needed.
        combatTable.saveContentChanges();
        encounter.Entities = combatTable.getContent();
        encounter.Entities.Add(monster, Color.LightGreen);

        //Save Content and add to table
        this.Page.Session["savedContent"] = encounter;
        combatTable.addRow(monster, Color.LightGreen);
        if (!combatTable.Rows[0].Visible) combatTable.Rows[0].Visible = true;
    }

    protected void loadButton_Click(object sender, EventArgs e)
    {
        Int32.TryParse(encounterDropDownList.SelectedValue, out int encounterID);
        //use this to make an encounter and save to session variable
        
        EncountersTable encounterTable = new EncountersTable(new DatabaseConnection());
        encounter = encounterTable.getEncounter(encounterID);

        MonstersTable monsterTable = new MonstersTable(new DatabaseConnection());
        List<Monster> encounterMonsters = monsterTable.getEncounterMonsters(encounterID);

        encounter.Entities = new Dictionary<Entity, Color>();
        
        //Add to entities dictionary
        foreach (Monster monster in encounterMonsters)
        {
            Color color;
            if (monster.IsFriendly) color = Color.LightGreen;
            else color = Color.LightSalmon;
            encounter.Entities.Add(monster, color);
        }

        //Load party
        loadParty();
        
        //Save to savedContent
        Session["savedContent"] = encounter;

        //Reload page to clear any nonsense before loading
        Response.Redirect("EncounterTool.aspx");
    }

    //Basically resets the page to a fresh load state, and clears all session variables (except activeGame).
    protected void newButton_Click(object sender, EventArgs e)
    {
        
        //Clear Content from Session Variable
        Session.Remove("savedContent");

        //Reload page to clear any nonsense before loading
        Response.Redirect("EncounterTool.aspx");
    }

    //Saves entities to the db
    protected void saveButton_Click(object sender, EventArgs e)
    {
        MonstersTable monstersTable = new MonstersTable(new DatabaseConnection());

        combatTable.saveContentChanges();
        encounter.Entities = combatTable.getContent();
        Session["savedContent"] = encounter;

        //Fresh Encounter, needs creation first
        if (encounter.EncounterID == 0)
        {
            EncountersTable encountersTable = new EncountersTable(new DatabaseConnection());

            encounter.EncounterName = hiddenEncounterName.Value;   //Get name from js prompt.

            int encounterID = encountersTable.insertEncounter(encounter); //Make encounter in db and return id
            if (encounterID > 0) encounter.EncounterID = encounterID;   //If valid id, set encounterID

            Session["savedContent"] = encounter;
        }

        //Foreach tableRow if its a monster, do the applicable database command (update/insert/delete) to mirror what the user has done in the table.
        foreach (ObjectTableRow objRow in combatTable.ObjectRows)
        {
            //Only care about Monsters
            if(objRow.Obj.GetType() == typeof(Monster))
            {
                Monster monster = (Monster) objRow.Obj;

                if (objRow.Visible == false)
                {
                    if (monster.MonsterID != 0) monstersTable.deleteMonster(monster); //delete monster
                    encounter.Entities.Remove(monster);
                }
                else if (objRow.Visible == true && monster.MonsterID != 0) monstersTable.updateMonster(monster); //update monster
                else if (objRow.Visible == true && monster.MonsterID == 0) //create monster
                {
                    if (monster.Encounter.EncounterID == 0) monster.Encounter = encounter;
                    int monsterID = monstersTable.insertMonster(monster);
                    if (monsterID > 0) monster.MonsterID = monsterID;
                    monster.EntityID = monstersTable.getMonsterEntityID(monster.MonsterID);
                } 
            }
        }


        //Save to savedContent w/ new IDs
        Session["savedContent"] = encounter;

        //Reload page to clear any nonsense before loading
        Response.Redirect("EncounterTool.aspx");

    }

    //Clears the server information, removes encounter and its monsters from the db, Response.Redirect back to same page to clear postback changes
    protected void deleteButton_Click(object sender, EventArgs e)
    {

        if(encounter.EncounterID != 0)
        {
            //Delete encounter information from database
            MonstersTable monstersTable = new MonstersTable(new DatabaseConnection());
            monstersTable.deleteEncounterMonsters(encounter.EncounterID);
            EncountersTable encountersTable = new EncountersTable(new DatabaseConnection());
            encountersTable.deleteEncounter(encounter.EncounterID);

            //Clear Content from Session Variable
            Session.Remove("savedContent");
        }

        //Reload page to clear any nonsense before loading
        Response.Redirect("EncounterTool.aspx");
    }
}