using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_GameParty : System.Web.UI.Page
{
    Game game;
    ObjectTable<PartyMember> partyTable;
    Party party;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Set Game for Page
        if (Session["userID"] == null) Response.Redirect("~/Login");
        if (Session["activeGame"] == null) Response.Redirect("~/Home");

        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        //Load Party State if appropriate
        if (Session["savedContent"] != null && ((Party)Session["savedContent"]).GameID != game.GameID) Session.Remove("savedContent");  //Handles if there is savedContent from a wrong game.
        if (Session["savedContent"] == null) loadParty(); //Handles a fresh load with no saved content
        else party = (Party)Session["savedContent"];   //Handles there is saved party from this game.

        loadPartyTable();
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
        List<Object> exceptions = new List<object>();
        for (int i = 0; i<party.PartyMembers.Count;i++)
        {
            PartyMember partyMember = party.PartyMembers.ElementAt(i).Key;
            if (partyMember.UserID == (int)Session["userID"])
            {
                exceptions.Add(partyMember);
                party.PartyMembers[partyMember] = Color.Gold;
            }
        }
        partyTable = new ObjectTable<PartyMember>(party.PartyMembers, new string[] { "Name", "Race", "Perception", "CurrentHP", "MaxHP", "Size" }, true, exceptions);
        PartyTablePlaceHolder.Controls.Add(partyTable);
    }

    //Saves changes to the party
    protected void saveButton_Click(object sender, EventArgs e)
    {
        PartyMembersTable partyMembersTable = new PartyMembersTable(new DatabaseConnection());

        partyTable.saveContentChanges();
        party.PartyMembers = partyTable.getContent();
        Session["savedContent"] = party;

        //Foreach tableRow if its a monster, do the applicable database command (update/insert/delete) to mirror what the user has done in the table.
        foreach (ObjectTableRow objRow in partyTable.ObjectRows)
        {
            //Only care about PartyMembers
            if (objRow.Obj.GetType() == typeof(PartyMember))
            {
                PartyMember partyMember = (PartyMember)objRow.Obj;
                if (partyMember.UserID == (int)Session["userID"]) partyMembersTable.updatePartyMember(partyMember); //update partyMember
            }
        }

        //Save to savedContent w/ new IDs
        Session["savedContent"] = party;

        angryLabel.ForeColor = Color.Green;
        angryLabel.Text = "Character(s) saved!";
    }

    //Quits the game on behalf of the player
    protected void leaveGameButton_Click(object sender, EventArgs e)
    {
        PartyMembersTable partyMembersTable = new PartyMembersTable(new DatabaseConnection());
        UsersTable userTable = new UsersTable(new DatabaseConnection());

        foreach(PartyMember pc in party.PartyMembers.Keys) if(pc.UserID == (int)Session["userID"]) partyMembersTable.deletePartyMember(pc);

        userTable.deleteUserPlayerGame((int)Session["userID"], game.GameID);

        Session.Remove("savedContent");
        Session.Remove("activeGame");

        //Load Home page and eat cookie so no error from controls not existing
        this.Page.Session.Remove("gameLinkID");
        this.Page.Session.Remove("gameCategoryID");
        this.Page.Session.Remove("gamePanelID");
        this.Page.Session.Remove("gameTypePanelID");

        this.Page.Session.Remove("page");

        Response.Redirect("~/Home");
        
    }
}