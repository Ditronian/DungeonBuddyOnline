using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_NPCTool : System.Web.UI.Page
{
    Game game;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login");
        if (Session["activeGame"] == null) Response.Redirect("~/Home");

        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;
    }


    //Saves the entirety of the NPC to the database
    protected void saveButton_Click(object sender, EventArgs e)
    {
        NPC npc = new NPC();
        if(firstNameTextBox.Text == "")
        {
            angryLabel.Text = "At a minimum, a NPC must have a first name!";
            return;
        }
        npc.Name = firstNameTextBox.Text + " " + lastNameTextBox.Text;
        npc.Race = raceDropDown.SelectedValue;
        npc.Sex = sexDropDown.SelectedValue;
        npc.Appearance = appearanceTextBox.Text;
        npc.Mannerism = mannerismTextBox.Text;
        npc.Flaw_secret = flawTextBox.Text;
        npc.InteractionStyle = interactionTextBox.Text;
        npc.Talent = talentTextBox.Text;
        npc.Location = locationTextBox.Text;
        npc.Bio = biographyTextBox.Text;
        npc.GameID = game.GameID;

        NPCsTable npcTable = new NPCsTable(new DatabaseConnection());
        npcTable.insertNPC(npc);

        //Clear the boxes so we dont accidentally duplicate a npc
        firstNameTextBox.Text = "";
        lastNameTextBox.Text = "";
        appearanceTextBox.Text = "";
        mannerismTextBox.Text = "";
        flawTextBox.Text = "";
        interactionTextBox.Text = "";
        talentTextBox.Text = "";
        locationTextBox.Text = "";
        biographyTextBox.Text = "";

        //All is right with the world, therefore we are not angry.
        angryLabel.ForeColor = System.Drawing.Color.Green;
        angryLabel.Text = "NPC Saved!";
    }

    //DEATH BY RANDOM GENERATOR BUTTON HANDLERS!!

    protected void rollFirstNameButton_Click(object sender, EventArgs e)
    {
        if (HttpRuntime.Cache.Get("names") == null) CacheLoader.loadRandomTables();
        Dictionary<String, NameTable> names = (Dictionary<String, NameTable>) HttpRuntime.Cache.Get("names");

        String name = null;
        if (raceDropDown.SelectedValue == "Human" && sexDropDown.SelectedValue == "Male") name = names["humanNames"].getMaleFirstName();
        else if (raceDropDown.SelectedValue == "Human" && sexDropDown.SelectedValue == "Female") name = names["humanNames"].getFemaleFirstName();
        else if (raceDropDown.SelectedValue == "Elven" && sexDropDown.SelectedValue == "Male") name = names["elvenNames"].getMaleFirstName();
        else if (raceDropDown.SelectedValue == "Elven" && sexDropDown.SelectedValue == "Female") name = names["elvenNames"].getFemaleFirstName();
        else if (raceDropDown.SelectedValue == "Dwarven" && sexDropDown.SelectedValue == "Male") name = names["dwarvenNames"].getMaleFirstName();
        else if (raceDropDown.SelectedValue == "Dwarven" && sexDropDown.SelectedValue == "Female") name = names["dwarvenNames"].getFemaleFirstName();
        else if (raceDropDown.SelectedValue == "Halfling" && sexDropDown.SelectedValue == "Male") name = names["halflingNames"].getMaleFirstName();
        else if (raceDropDown.SelectedValue == "Halfling" && sexDropDown.SelectedValue == "Female") name = names["halflingNames"].getFemaleFirstName();
        else if (raceDropDown.SelectedValue == "Gnomish" && sexDropDown.SelectedValue == "Male") name = names["gnomishNames"].getMaleFirstName();
        else if (raceDropDown.SelectedValue == "Gnomish" && sexDropDown.SelectedValue == "Female") name = names["gnomishNames"].getFemaleFirstName();
        else if (raceDropDown.SelectedValue == "Gnoll" && sexDropDown.SelectedValue == "Male") name = names["gnollNames"].getMaleFirstName();
        else if (raceDropDown.SelectedValue == "Gnoll" && sexDropDown.SelectedValue == "Female") name = names["gnollNames"].getFemaleFirstName();

        if (name != null) firstNameTextBox.Text = name;


    }

    protected void rollLastNameButton_Click(object sender, EventArgs e)
    {
        if (HttpRuntime.Cache.Get("names") == null) CacheLoader.loadRandomTables();
        Dictionary<String, NameTable> names = (Dictionary<String, NameTable>)HttpRuntime.Cache.Get("names");

        String name = null;
        if (raceDropDown.SelectedValue == "Human") name = names["humanNames"].getLastName();
        else if (raceDropDown.SelectedValue == "Elven") name = names["elvenNames"].getLastName();
        else if (raceDropDown.SelectedValue == "Dwarven") name = names["dwarvenNames"].getLastName();
        else if (raceDropDown.SelectedValue == "Halfling") name = names["halflingNames"].getLastName();
        else if (raceDropDown.SelectedValue == "Gnomish") name = names["gnomishNames"].getLastName();
        else if (raceDropDown.SelectedValue == "Gnoll") name = names["gnollNames"].getLastName();

        if (name != null) lastNameTextBox.Text = name;
    }

    protected void rollFullNameButton_Click(object sender, EventArgs e)
    {
        if (HttpRuntime.Cache.Get("names") == null) CacheLoader.loadRandomTables();
        Dictionary<String, NameTable> names = (Dictionary<String, NameTable>)HttpRuntime.Cache.Get("names");

        String[] name = null;
        if (raceDropDown.Text == "Human" && sexDropDown.Text == "Male") name = names["humanNames"].getMaleFullName();
        else if (raceDropDown.Text == "Human" && sexDropDown.Text == "Female") name = names["humanNames"].getFemaleFullName();
        else if (raceDropDown.Text == "Elven" && sexDropDown.Text == "Male") name = names["elvenNames"].getMaleFullName();
        else if (raceDropDown.Text == "Elven" && sexDropDown.Text == "Female") name = names["elvenNames"].getFemaleFullName();
        else if (raceDropDown.Text == "Dwarven" && sexDropDown.Text == "Male") name = names["dwarvenNames"].getMaleFullName();
        else if (raceDropDown.Text == "Dwarven" && sexDropDown.Text == "Female") name = names["dwarvenNames"].getFemaleFullName();
        else if (raceDropDown.Text == "Halfling" && sexDropDown.Text == "Male") name = names["halflingNames"].getMaleFullName();
        else if (raceDropDown.Text == "Halfling" && sexDropDown.Text == "Female") name = names["halflingNames"].getFemaleFullName();
        else if (raceDropDown.Text == "Gnomish" && sexDropDown.Text == "Male") name = names["gnomishNames"].getMaleFullName();
        else if (raceDropDown.Text == "Gnomish" && sexDropDown.Text == "Female") name = names["gnomishNames"].getFemaleFullName();
        else if (raceDropDown.Text == "Gnoll" && sexDropDown.Text == "Male") name = names["gnollNames"].getMaleFullName();
        else if (raceDropDown.Text == "Gnoll" && sexDropDown.Text == "Female") name = names["gnollNames"].getFemaleFullName();

        if (name != null)
        {
            firstNameTextBox.Text = name[0];
            lastNameTextBox.Text = name[1];
        }
    }

    protected void rollAppearanceButton_Click(object sender, EventArgs e)
    {
        appearanceTextBox.Text = NPCTraitTable.getAppearance();
    }

    protected void rollMannerismButton_Click(object sender, EventArgs e)
    {
        mannerismTextBox.Text = NPCTraitTable.getMannerism();
    }

    protected void rollInteractionButton_Click(object sender, EventArgs e)
    {
        interactionTextBox.Text = NPCTraitTable.getInteractionStyle();
    }

    protected void rollFlawButton_Click(object sender, EventArgs e)
    {
        flawTextBox.Text = NPCTraitTable.getFlawSecret();
    }

    protected void rollTalentButton_Click(object sender, EventArgs e)
    {
        talentTextBox.Text = NPCTraitTable.getTalent();
    }

    //Rolls all traits
    protected void rollAllButton_Click(object sender, EventArgs e)
    {
        rollAppearanceButton_Click(sender, e);
        rollMannerismButton_Click(sender, e);
        rollInteractionButton_Click(sender, e);
        rollFlawButton_Click(sender, e);
        rollTalentButton_Click(sender, e);
    }

    //Randomly choose race and sex, then roll all traits and name.
    protected void rollEntireNpcButton_Click(object sender, EventArgs e)
    {
        Random random = new Random();
        raceDropDown.SelectedIndex = random.Next(raceDropDown.Items.Count);
        sexDropDown.SelectedIndex = random.Next(sexDropDown.Items.Count);
        rollFullNameButton_Click(sender, e);
        rollAllButton_Click(sender, e);
    }
}