using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for EntityTableCell
/// </summary>
public class ObjectTableCell : TableCell
{
    private ObjectTableRow row;
    private TextBox textbox;
    private DropDownList dropDown;
    private string parameterName;

    public ObjectTableCell(ObjectTableRow row, string parameterName, bool readOnly, int column = -1)
    {
        this.row = row;
        this.parameterName = parameterName;
        if (column != 1)
        {
            //Make TextBox
            this.textbox = new TextBox();
            updateTextBox();
            this.Controls.Add(textbox);
        }
        else
        {
            dropDown = new DropDownList();
            dropDown.Items.Insert(0, new ListItem("Common", "0"));
            dropDown.Items.Insert(0, new ListItem("Uncommon", "1"));
            dropDown.Items.Insert(0, new ListItem("Rare", "2"));
            dropDown.Items.Insert(0, new ListItem("Very Rare", "3"));
            dropDown.Items.Insert(0, new ListItem("Legendary", "4"));
        }
        if (readOnly) textbox.ReadOnly = true;
    }

    //Sets the TextBox's value
    public void updateTextBox()
    {
        object parameterValue = row.Obj.GetType().GetProperty(parameterName).GetValue(row.Obj, null);
        if (row.Obj.GetType().GetProperty(parameterName).GetValue(row.Obj, null).GetType() == typeof(int)) textbox.TextMode = TextBoxMode.Number;
        textbox.Text = parameterValue.ToString();
        //textbox.ReadOnly = true;
        if(row.Color != null) textbox.BackColor = row.Color;
        
    }

    //Updates Object with new value via Reflection
    public void saveTextBox()
    {
        
    }

    public ObjectTableRow Row { get => row; set => row = value; }
    public TextBox Textbox { get => textbox; set => textbox = value; }
    public string ParameterName { get => parameterName; set => parameterName = value; }
}