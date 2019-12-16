using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_GM_NPCTracker : System.Web.UI.Page
{
    private Game game;
    private List<NPC> npcs;
    private Table table;
    private List<RadioButton> radioButtons = new List<RadioButton>();
    private List<ObjectTableRow> objectRows = new List<ObjectTableRow>();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Gate Keeper
        if (Session["userID"] == null) Response.Redirect("~/Login");
        if (Session["activeGame"] == null) Response.Redirect("~/Home");

        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        loadNPCTable();

        //Add js resort function on load
        Main masterPage = this.Master as Main;
        masterPage.Body.Attributes.Add("onLoad", "resort(1);");

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

    private void loadNPCTable()
    {
        NPCsTable dbNpcTable = new NPCsTable(new DatabaseConnection());
        npcs = dbNpcTable.getGameNPCs(game.GameID);
        String[] parameters = new string[] { "Name", "Sex", "Race", "Location" };

        //Generate Table
        table = new Table();
        table.ID = "objectTable";
        TableRow header = new TableRow();

        TableCell blankColumn = new TableCell();
        header.Cells.Add(blankColumn);

        //Make Header Columns
        for (int i = 0; i < parameters.Length; i++)
        {
            TableCell headerColumn = new TableCell();
            Button sortButton = new Button();
            sortButton.Text = parameters[i];
            sortButton.CssClass = "fullWidth";
            sortButton.OnClientClick = "return sortTable(" + (i+1) + ")";

            headerColumn.Controls.Add(sortButton);
            header.Cells.Add(headerColumn);
        }
        table.Rows.Add(header);

        if (npcs.Count == 0) header.Visible = false;
        else header.Visible = true;

        //Make the remaining rows, where each row contains one object from the provided List<T>
        foreach (NPC npc in npcs)
        {
            ObjectTableRow objectRow = new ObjectTableRow(npc, Color.White);

            //Check if Object should be player editable
            bool readOnly = true;

            //Add select radio button
            TableCell selectCell = new TableCell();
            RadioButton isSelected = new RadioButton();
            isSelected.Attributes.Add("value", npc.NpcID.ToString());
            isSelected.GroupName = "npcGroup";
            isSelected.AutoPostBack = true;
            isSelected.CheckedChanged += new EventHandler(npcButton_Click);
            selectCell.Controls.Add(isSelected);
            radioButtons.Add(isSelected);
            objectRow.Controls.Add(selectCell);

            //Make the individual cells for this object, using the provided parameter Names to obtain the desired values.
            for (int column = 0; column < parameters.Length; column++)
            {
                ObjectTableCell objectCell = new ObjectTableCell(objectRow, parameters[column], readOnly);
                objectRow.Cells.Add(objectCell);
                objectRow.ObjectCells.Add(objectCell);
            }

            //Add to table
            table.Rows.Add(objectRow);
            objectRows.Add(objectRow);
            if (objectRow.Color == Color.Empty) objectRow.Visible = false;
        }

        NPCTablePlaceHolder.Controls.Add(table);
    }


    protected void npcButton_Click(object sender, EventArgs e)
    {
        //Event Data
        RadioButton button = (RadioButton)sender;
        ObjectTableRow row = (ObjectTableRow)button.Parent.Parent;

        //Remove Selected from the table, then make a new dictionary so next postback wont build it anymore
        NPC npc = (NPC)row.Obj;

        //Select NPC
        selectNPC(npc);
    }

    //Selects a given npc in the RH box.
    private void selectNPC(NPC npc)
    {
        //Save as session var so we can remember who is being edited/viewed.
        Session["activeNPC"] = npc;

        nameTextBox.Text = npc.Name;
        raceTextBox.Text = npc.Race;
        sexTextBox.Text = npc.Sex;
        locationTextBox.Text = npc.Location;
        bioTextBox.Text = npc.Bio;
        appearanceTextBox.Text = npc.Appearance;
        mannerismTextBox.Text = npc.Mannerism;
        talentTextBox.Text = npc.Talent;
        flawTextBox.Text = npc.Flaw_secret;
        interactionTextBox.Text = npc.InteractionStyle;
    }

    //Saves the active NPC if there is one
    protected void saveButton_Click(object sender, EventArgs e)
    {
        if (Session["activeNPC"] == null)
        {
            angryLabel.Text = "There is no active NPC.";
            return;
        }

        NPCsTable dbNpcTable = new NPCsTable(new DatabaseConnection());
        NPC npc = (NPC)Session["activeNPC"];
        npc.Name = nameTextBox.Text;
        npc.Sex = sexTextBox.Text;
        npc.Race = raceTextBox.Text;
        npc.Location = locationTextBox.Text;
        npc.Bio = bioTextBox.Text;
        npc.Appearance = appearanceTextBox.Text;
        npc.Mannerism = mannerismTextBox.Text;
        npc.InteractionStyle = interactionTextBox.Text;
        npc.Flaw_secret = flawTextBox.Text;
        npc.Talent = talentTextBox.Text;

        //Find the current selected row
        ObjectTableRow selectedRow = null;
        foreach (ObjectTableRow row in objectRows) if (((NPC)row.Obj).NpcID == npc.NpcID) selectedRow = row;

        //Update Textbox Values
        if (selectedRow != null)
        {
            selectedRow.ObjectCells[0].Textbox.Text = npc.Name;
            selectedRow.ObjectCells[1].Textbox.Text = npc.Sex;
            selectedRow.ObjectCells[2].Textbox.Text = npc.Race;
            selectedRow.ObjectCells[3].Textbox.Text = npc.Location;
        }

        Session["activeNPC"] = npc;
        dbNpcTable.updateNPC(npc);

        angryLabel.ForeColor = Color.Green;
        angryLabel.Text = "NPC Saved!";
    }

    protected void deleteButton_Click(object sender, EventArgs e)
    {
        if (Session["activeNPC"] == null)
        {
            angryLabel.Text = "There is no active NPC.";
            return;
        }

        NPCsTable dbNpcTable = new NPCsTable(new DatabaseConnection());
        NPC npc = (NPC)Session["activeNPC"];

        dbNpcTable.deleteNPC(npc);

        //Clear Content from Session Variable
        Session.Remove("activeNPC");
        Session["message"] = new Message("NPC Deleted!", System.Drawing.Color.Green);

        //Reload page to clear any nonsense before loading
        Response.Redirect("NPCTracker");
    }
}