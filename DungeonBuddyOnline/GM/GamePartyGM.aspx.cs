using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_GamePartyGM : System.Web.UI.Page
{
    Game game;
    ObjectTable<PartyMember> partyTable;
    Party party;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login");
        if (Session["activeGame"] == null) Response.Redirect("~/Home");

        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        //Load Party State if appropriate
        if (Session["savedContent"] != null && ((Party)Session["savedContent"]).GameID != game.GameID) Session.Remove("savedContent");  //Handles if there is savedContent from a wrong game.
        if (Session["savedContent"] == null) loadParty(); //Handles a fresh load with no saved content
        else party = (Party)Session["savedContent"];   //Handles there is saved party from this game.

        loadPartyTable();
        loadJoinRequestsTable();

        //Add js resort function on load
        Main masterPage = this.Master as Main;
        masterPage.Body.Attributes.Add("onLoad", "resort(0);");

        //Little tool for relaying messages thru redirects
        if (Session["message"] != null)
        {
            Message message = (Message)Session["message"];
            angryLabel.ForeColor = message.Color;
            angryLabel.Text = message.Text;
            Session.Remove("message");
        }
        else angryLabel.Text = "&nbsp;";
    }


    //Loads the party from the database.
    private void loadParty()
    {
        PartyMembersTable partyTable = new PartyMembersTable(new DatabaseConnection());
        party = partyTable.getParty(game.GameID);
    }

    //Loads the party to the partyTable
    private void loadPartyTable()
    {
        partyTable = new ObjectTable<PartyMember>(party.PartyMembers, new string[] { "Name", "Race", "Perception", "CurrentHP", "MaxHP", "Size" });
        PartyTablePlaceHolder.Controls.Add(partyTable);
    }

    //Loads the incoming Join Requests
    private void loadJoinRequestsTable()
    {
        UsersTable userTable = new UsersTable(new DatabaseConnection());
        List<JoinRequest> requests = userTable.getJoinRequests(game.GameID);
        if (requests.Count == 0) return;
        
        JoinTable joinTable = new JoinTable(requests);

        JoinRequestsPlaceholder.Controls.Add(joinTable);
        
    }


    //Saves changes to the party
    protected void saveButton_Click(object sender, EventArgs e)
    {
        PartyMembersTable partyMembersTable = new PartyMembersTable(new DatabaseConnection());

        partyTable.saveContentChanges();
        party.PartyMembers = partyTable.getContent();
        Session["savedContent"] = party;

        //Foreach tableRow if its a monster, do the applicable database command (update/insert/delete) to mirror what the user has done in the table.
        Dictionary<int, int> userChoppingBlock = new Dictionary<int, int>();
        foreach (ObjectTableRow objRow in partyTable.ObjectRows)
        {
            //Only care about PartyMembers
            if (objRow.Obj.GetType() == typeof(PartyMember))
            {
                PartyMember partyMember = (PartyMember)objRow.Obj;
                //Add to character count of users
                if (partyMember.UserID != 0 && userChoppingBlock.ContainsKey(partyMember.UserID)) userChoppingBlock[partyMember.UserID]++;
                else if (partyMember.UserID != 0) userChoppingBlock.Add(partyMember.UserID, 1);

                if (objRow.Visible == false) //delete partyMember
                {
                    if (partyMember.PartyMemberID != 0) //Remove from database
                    {
                        partyMembersTable.deletePartyMember(partyMember);

                        //if is owned by a user, subtract from counter so we can see later if they should still have access to the game
                        if (partyMember.UserID != 0) userChoppingBlock[partyMember.UserID]--;
                    } 
                    party.PartyMembers.Remove(partyMember);
                }
                else if (objRow.Visible == true && partyMember.PartyMemberID != 0) partyMembersTable.updatePartyMember(partyMember); //update partyMember
                else if (objRow.Visible == true && partyMember.PartyMemberID == 0) //create partyMember
                {
                    partyMember.GameID = game.GameID;
                    int partyMemberID = partyMembersTable.insertPartyMember(partyMember);
                    if (partyMemberID > 0) partyMember.PartyMemberID = partyMemberID;
                    partyMember.EntityID = partyMembersTable.getPartyMemberEntityID(partyMember.PartyMemberID);
                }
            }
        }
        //Remove users who should no longer have access because they have no characters
        UsersTable userTable = new UsersTable(new DatabaseConnection());
        foreach (KeyValuePair<int, int> user in userChoppingBlock) if (user.Value <= 0) userTable.deleteUserPlayerGame(user.Key, game.GameID);

        //Save to savedContent w/ new IDs
        Session["savedContent"] = party;
        Session["message"] = new Message("Party Saved!", System.Drawing.Color.Green);

        //Reload page to clear any nonsense before loading
        Response.Redirect("GamePartyGM");
    }

    //Adds a PC party member
    protected void addPartyPCButton_Click(object sender, EventArgs e)
    {
        //Check for required field
        if (partyPCNameTextBox.Text == String.Empty)
        {
            angryLabel.Text = "New Creatures require a name.";
            return;
        }

        //Get inputs
        String originalName = partyPCNameTextBox.Text;
        String race = partyPCRaceTextBox.Text;
        Int32.TryParse(partyPCCurrentHPTextBox.Text, out int currentHP);
        Int32.TryParse(partyPCMaxHPTextBox.Text, out int maxHP);
        Int32.TryParse(partyPCPerceptionTextBox.Text, out int passivePerception);
        char size;
        if (partyPCSizeTextBox.Text == "") size = 'M';
        else size = partyPCSizeTextBox.Text.ElementAtOrDefault(0);

        //Decide if partymember needs a suffix to distinguish multiple creatures with the same name
        String actualName = originalName;
        HashSet<String> names = new HashSet<string>();
        foreach (ObjectTableRow objRow in partyTable.ObjectRows) if (objRow.Visible == true && objRow.Obj != null) names.Add(((Entity)objRow.Obj).Name);
        int i = 2;
        while (names.Contains(actualName))
        {
            actualName = originalName + " (" + i + ")";
            i++;
        }

        //Create new party member
        PartyMember partyMember = new PartyMember();
        partyMember.Name = actualName;
        partyMember.Race = race;
        partyMember.MaxHP = maxHP;
        partyMember.CurrentHP = currentHP;
        partyMember.Perception = passivePerception;
        partyMember.GameID = game.GameID;
        partyMember.Size = size;
        partyMember.IsNpc = false;

        //Add party member to the party
        partyTable.saveContentChanges();
        party.PartyMembers = partyTable.getContent();
        party.PartyMembers.Add(partyMember, Color.LightBlue);

        //Save Content and add to table
        Session["savedContent"] = party;
        partyTable.addRow(partyMember, Color.LightBlue);

        if (!partyTable.Rows[0].Visible) partyTable.Rows[0].Visible = true;
    }


    //Adds a NPC party member
    protected void addPartyNPCButton_Click(object sender, EventArgs e)
    {
        //Check for required field
        if (partyNPCNameTextBox.Text == String.Empty)
        {
            angryLabel.Text = "New Creatures require a name.";
            return;
        }

        //Get inputs
        String originalName = partyNPCNameTextBox.Text;
        String race = partyNPCRaceTextBox.Text;
        Int32.TryParse(partyNPCCurrentHPTextBox.Text, out int currentHP);
        Int32.TryParse(partyNPCMaxHPTextBox.Text, out int maxHP);
        Int32.TryParse(partyNPCPerceptionTextBox.Text, out int passivePerception);
        char size;
        if (partyNPCSizeTextBox.Text == "") size = 'M';
        else size = partyNPCSizeTextBox.Text.ElementAtOrDefault(0);


        //Decide if partymember needs a suffix to distinguish multiple creatures with the same name
        String actualName = originalName;
        HashSet<String> names = new HashSet<string>();
        foreach (ObjectTableRow objRow in partyTable.ObjectRows) if (objRow.Visible == true && objRow.Obj != null) names.Add(((Entity)objRow.Obj).Name);
        int i = 2;
        while (names.Contains(actualName))
        {
            actualName = originalName + " (" + i + ")";
            i++;
        }

        //Create new party member
        PartyMember partyMember = new PartyMember();
        partyMember.Name = actualName;
        partyMember.Race = race;
        partyMember.MaxHP = maxHP;
        partyMember.CurrentHP = currentHP;
        partyMember.Perception = passivePerception;
        partyMember.GameID = game.GameID;
        partyMember.Size = size;
        partyMember.IsNpc = true;

        //Add party member to the party
        partyTable.saveContentChanges();
        party.PartyMembers = partyTable.getContent();
        party.PartyMembers.Add(partyMember, Color.LightGreen);

        //Save Content and add to table
        Session["savedContent"] = party;
        partyTable.addRow(partyMember, Color.LightGreen);

        if (!partyTable.Rows[0].Visible) partyTable.Rows[0].Visible = true;
    }
}