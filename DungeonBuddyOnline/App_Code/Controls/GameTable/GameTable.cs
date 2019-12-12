using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


//To Do:  Find a better way for the JS Sort Method to find the table, rather than give each table the same ID.  Probably should generate an id, and then pass it.
//Another big problem is there is no way to lookup if a monster is friendly or not, in which case it should be green, not red
public class GameTable : Table
{
    private List<Game> games;
    private List<GameTableRow> gameRows = new List<GameTableRow>();
    private List<RadioButton> radioButtons = new List<RadioButton>();

    //Takes a list of any kind of objects, an array with the desired parameters to form the columns, and an optional Dictionary of types/colors so you can color code by inherritance.
    public GameTable(List<Game> games)
    {
        this.games = games;

        //Generate Table
        this.ID = "gameTable";
        
        TableRow header = new TableRow();

        TableCell blankCell = new TableCell();
        header.Cells.Add(blankCell);

        TableCell nameColumn = new TableCell();
        nameColumn.Text = "Game Name";
        header.Cells.Add(nameColumn);

        TableCell settingColumn = new TableCell();
        settingColumn.Text = "Game Setting";
        header.Cells.Add(settingColumn);

        this.Rows.Add(header);

        //if (games.Count == 0) header.Visible = false;
        //else header.Visible = true;
        //Make the remaining rows, where each row contains one object from the provided List<T>
        foreach (Game game in games)
        {
            GameTableRow gameRow = new GameTableRow(game);

            //Make the individual cells for this row
            GameTableCell checkBoxCell = new GameTableCell(gameRow);
            RadioButton isSelected = new RadioButton();
            isSelected.Attributes.Add("value", game.GameID.ToString());
            isSelected.GroupName = "gameGroup";
            checkBoxCell.Controls.Add(isSelected);
            radioButtons.Add(isSelected);
            gameRow.Controls.Add(checkBoxCell);

            GameTableCell gameNameCell = new GameTableCell(gameRow);
            TextBox nameTextBox = new TextBox();
            nameTextBox.Text = game.GameName;
            nameTextBox.ReadOnly = true;
            gameNameCell.Controls.Add(nameTextBox);
            gameRow.Controls.Add(gameNameCell);

            GameTableCell gameSettingCell = new GameTableCell(gameRow);
            TextBox settingTextBox = new TextBox();
            settingTextBox.Text = game.GameSetting;
            settingTextBox.ReadOnly = true;
            gameSettingCell.Controls.Add(settingTextBox);
            gameRow.Controls.Add(gameSettingCell);

            //Add to table
            this.Rows.Add(gameRow);
            gameRows.Add(gameRow);
        }
    }


    public List<Game> Games { get => games; set => games = value; }
    public List<GameTableRow> GameRows { get => gameRows; set => gameRows = value; }
    public List<RadioButton> RadioButtons { get => radioButtons; set => radioButtons = value; }
}