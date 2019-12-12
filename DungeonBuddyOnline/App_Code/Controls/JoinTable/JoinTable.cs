using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for JoinTable
/// </summary>
public class JoinTable : Table
{
    private List<JoinRequest> requests;

    public JoinTable(List<JoinRequest> requests)
    {
        this.requests = requests;

        foreach (JoinRequest request in requests)
        {
            RequestRow row = new RequestRow(request);

            TableCell cellUserName = new TableCell();
            TextBox userNameTextBox = new TextBox();
            userNameTextBox.Text = request.User.UserName;
            userNameTextBox.ReadOnly = true;
            cellUserName.Controls.Add(userNameTextBox);
            row.Controls.Add(cellUserName);

            TableCell cellCharName = new TableCell();
            TextBox charNameTextBox = new TextBox();
            charNameTextBox.Text = request.PartyMember.Name;
            charNameTextBox.ReadOnly = true;
            cellCharName.Controls.Add(charNameTextBox);
            row.Controls.Add(cellCharName);

            TableCell cellCharRace = new TableCell();
            TextBox charRaceTextBox = new TextBox();
            charRaceTextBox.Text = request.PartyMember.Race;
            charRaceTextBox.ReadOnly = true;
            cellCharRace.Controls.Add(charRaceTextBox);
            row.Controls.Add(cellCharRace);

            TableCell cellCharPerc = new TableCell();
            TextBox charPercTextBox = new TextBox();
            charPercTextBox.Text = request.PartyMember.Perception.ToString();
            charPercTextBox.ReadOnly = true;
            cellCharPerc.Controls.Add(charPercTextBox);
            row.Controls.Add(cellCharPerc);

            TableCell cellCharHP = new TableCell();
            TextBox charHPTextBox = new TextBox();
            charHPTextBox.Text = request.PartyMember.MaxHP.ToString();
            charHPTextBox.ReadOnly = true;
            cellCharHP.Controls.Add(charHPTextBox);
            row.Controls.Add(cellCharHP);

            TableCell cellCharSize = new TableCell();
            TextBox charSizeTextBox = new TextBox();
            charSizeTextBox.Text = request.PartyMember.Size.ToString();
            charSizeTextBox.ReadOnly = true;
            cellCharSize.Controls.Add(charSizeTextBox);
            row.Controls.Add(cellCharSize);

            TableCell cellAccept = new TableCell();
            Button acceptButton = new Button();
            acceptButton.Text = "Accept";
            acceptButton.CssClass = "fullWidth";
            acceptButton.Click += new EventHandler(acceptButtonClicked);
            cellAccept.Controls.Add(acceptButton);
            row.Controls.Add(cellAccept);

            TableCell cellDeny = new TableCell();
            Button denyButton = new Button();
            denyButton.Text = "Deny";
            denyButton.CssClass = "fullWidth";
            denyButton.Click += new EventHandler(denyButtonClicked);
            cellDeny.Controls.Add(denyButton);
            row.Controls.Add(cellDeny);

            this.Controls.Add(row);
        }

    }

    //Accepts User's request to join the party
    protected void acceptButtonClicked(Object sender, EventArgs e)
    {
        Button button = (Button)sender;
        JoinRequest request = ((RequestRow)button.Parent.Parent).Request;

        UsersTable userTable = new UsersTable(new DatabaseConnection());
        userTable.acceptJoinRequest(request);

        bool alreadyPlaying = userTable.getUserPlayerGame(request.User.UserID, request.GameID);
        if (!alreadyPlaying) userTable.insertUserPlayerGame(request.User.UserID, request.GameID);

        //Clear Content from Session Variable
        this.Page.Session.Remove("savedContent");

        //Reload page to clear any nonsense before loading
        this.Page.Response.Redirect("GamePartyGM.aspx");
    }

    //Denies User's request to join the party
    protected void denyButtonClicked(Object sender, EventArgs e)
    {
        Button button = (Button)sender;
        JoinRequest request = ((RequestRow)button.Parent.Parent).Request;

        UsersTable userTable = new UsersTable(new DatabaseConnection());
        userTable.denyJoinRequest(request);

        //Clear Content from Session Variable
        this.Page.Session.Remove("savedContent");

        //Reload page to clear any nonsense before loading
        this.Page.Response.Redirect("GamePartyGM.aspx");
    }
}