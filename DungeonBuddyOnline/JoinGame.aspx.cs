using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_JoinGame : System.Web.UI.Page
{
    List<Game> games;
    GameTable gameTable;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Set Game for Page
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");

        loadGameTable();
    }

    //Loads the party from the database.
    private void loadGameTable()
    {
        GamesTable dbGameTable = new GamesTable(new DatabaseConnection());
        games = dbGameTable.getJoinableGames();
        gameTable = new GameTable(games);
        JoinGameTablePlaceHolder.Controls.Add(gameTable);
    }

    //Sends request to GM's Game Party screen to join game with the made character.
    protected void submitJoinButton_Click(object sender, EventArgs e)
    {
        int gameID = 0;
        RadioButton selected = null;
        foreach (RadioButton button in gameTable.RadioButtons) if (button.Checked) selected = button;
        if (selected == null)
        {
            angryLabel.ForeColor = Color.Red;
            angryLabel.Text = "You must select a game to join!";
            return;
        }
        else gameID = Int32.Parse(selected.Attributes["value"]);


        Int32.TryParse(perceptionTextBox.Text, out int perception);
        Int32.TryParse(hpTextBox.Text, out int hp);

        PartyMember pc = new PartyMember();
        if (nameTextBox.Text == "") pc.Name = "No Name";
        else pc.Name = nameTextBox.Text;
        pc.Race = raceTextBox.Text;
        pc.Perception = perception;
        pc.CurrentHP = hp;
        pc.MaxHP = hp;
        pc.UserID = (int) Session["userID"];
        if (sizeTextBox.Text == "") pc.Size = 'M';
        else pc.Size = sizeTextBox.Text.ElementAtOrDefault(0);
        pc.IsNpc = false;

        PartyMembersTable partyMembersTable = new PartyMembersTable(new DatabaseConnection());
        UsersTable userTable = new UsersTable(new DatabaseConnection());
        //partyMember.GameID = game.GameID;
        int partyMemberID = partyMembersTable.insertPartyMember(pc);
        if (partyMemberID > 0) pc.PartyMemberID = partyMemberID;
        pc.EntityID = partyMembersTable.getPartyMemberEntityID(pc.PartyMemberID);

        userTable.insertJoinRequest((int)Session["userID"], gameID, partyMemberID);

        //Load Home page
        angryLabel.ForeColor = Color.Red;
        angryLabel.Text = "A join request has been sent to the game's Dungeon Master.";

        //Reset Stuff
        nameTextBox.Text = "";
        raceTextBox.Text = "";
        sizeTextBox.Text = "";
        perceptionTextBox.Text = "";
        hpTextBox.Text = "";
        selected.Checked = false;

    }
}