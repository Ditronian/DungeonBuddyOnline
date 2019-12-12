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
public class ObjectTable<T> : Table
{
    private int columns;
    private Dictionary<T, Color> colors;
    private List<ObjectTableRow> objectRows = new List<ObjectTableRow>();
    private string[] parameters;
    private bool viewOnly;
    private List<Object> exceptions;

    //Takes a list of any kind of objects, an array with the desired parameters to form the columns, and an optional Dictionary of types/colors so you can color code by inherritance.
    public ObjectTable(Dictionary<T,Color> objects, string[] parameters, bool viewOnly = false, List<Object> exceptions = null)
    {
        
        this.columns = parameters.Length;
        this.colors = objects;
        this.parameters = parameters;
        this.viewOnly = viewOnly;
        this.exceptions = exceptions;

        //Generate Table
        this.ID = "objectTable";  //Needed so javascript can find the table to sort columns.  Should use a better way.
        //Make Header Row, and Buttons so can be sorted by whatever column I want.  Uses JS to sort entire rows.
        
        TableRow header = new TableRow();
        for (int i = 0; i < columns; i++)
        {
            TableCell headerColumn = new TableCell();
            Button sortButton = new Button();
            sortButton.Text = parameters[i];
            sortButton.CssClass = "fullWidth";
            sortButton.OnClientClick = "return sortTable("+i+")";

            headerColumn.Controls.Add(sortButton);
            header.Cells.Add(headerColumn);
        }
        this.Rows.Add(header);
        if (colors.Count == 0) header.Visible = false;
        else header.Visible = true;

        //Make the remaining rows, where each row contains one object from the provided List<T>
        foreach (KeyValuePair<T, Color> entry in colors)
        {
            ObjectTableRow objectRow = new ObjectTableRow(entry.Key, entry.Value);

            //Check if Object should be player editable
            bool readOnly = false;
            if (viewOnly && !exceptions.Contains((Object)entry.Key)) readOnly = true;

            //Make the individual cells for this object, using the provided parameter Names to obtain the desired values.
            for (int column = 0; column < columns; column++)
            {
                ObjectTableCell objectCell = new ObjectTableCell(objectRow, parameters[column], readOnly);
                objectRow.Cells.Add(objectCell);
                objectRow.ObjectCells.Add(objectCell);
            }
            //Make Delete button if not view only
            if (!viewOnly)
            { 
                TableCell deleteCell = new TableCell();
                Button deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.Click += new EventHandler(deleteButton_Click);
                deleteCell.Controls.Add(deleteButton);
                objectRow.Cells.Add(deleteCell);
            }

            //Add to table
            this.Rows.Add(objectRow);
            objectRows.Add(objectRow);
            if (objectRow.Color == Color.Empty) objectRow.Visible = false;
        }
    }

    public void addRow(T newObject, Color color)
    {
        //Make the remaining rows, where each row contains one object from the provided List<T>
        ObjectTableRow objectRow = new ObjectTableRow(newObject, color);

        //Check if Object should be player editable
        bool readOnly = false;
        if (viewOnly && !exceptions.Contains((Object)newObject)) readOnly = true;

        //Make the individual cells for this object, using the provided parameter Names to obtain the desired values.
        for (int column = 0; column < columns; column++)
            {
                ObjectTableCell objectCell = new ObjectTableCell(objectRow, parameters[column], readOnly);
                objectRow.Cells.Add(objectCell);
                objectRow.ObjectCells.Add(objectCell);
            }
            //Make Delete button if not view only
            if (!viewOnly)
            {
                TableCell deleteCell = new TableCell();
                Button deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.Click += new EventHandler(deleteButton_Click);
                deleteCell.Controls.Add(deleteButton);
                objectRow.Cells.Add(deleteCell);
            }

            //Add to table
            this.Rows.Add(objectRow);
            objectRows.Add(objectRow);
            if (color == Color.Empty) objectRow.Visible = false;
    }

    //Postbacks get very confused as to what textbox values go with what textboxes, so force restore the correct values.
    public void restoreValues()
    {
        foreach (ObjectTableRow row in objectRows)
        {
            foreach (ObjectTableCell cell in row.ObjectCells) cell.updateTextBox();
        }
    }

    //Uses reflection to update the objects used to construct the table with the current table values, and then returns those objects.
    public Dictionary<T, Color> getContent()
    {
        //Build a new dictionary with the updated objects then return that updated dictionary
        Dictionary<T, Color> content = new Dictionary<T, Color>();
        foreach (ObjectTableRow objectRow in objectRows)
        {
            content.Add((T)objectRow.Obj, objectRow.Color);
        }

        return content;
    }

    //Update the Row Objects so their properties have updated values that match what's in the textboxes
    public void saveContentChanges()
    {
        for (int row = 0; row < ObjectRows.Count; row++)
        {
            for (int cell = 0; cell < objectRows[row].ObjectCells.Count; cell++)
            {
                object parameterValue = ObjectRows[row].ObjectCells[cell].Textbox.Text;
                PropertyInfo propertyInfo = objectRows[row].Obj.GetType().GetProperty(ObjectRows[row].ObjectCells[cell].ParameterName);
                if (propertyInfo.PropertyType == typeof(Int32) && ObjectRows[row].ObjectCells[cell].Textbox.Text == "") parameterValue = "0";
                if (propertyInfo.PropertyType == typeof(char) && ObjectRows[row].ObjectCells[cell].Textbox.Text.Count() < 1) parameterValue = 'x';
                    propertyInfo.SetValue(objectRows[row].Obj, Convert.ChangeType(parameterValue, propertyInfo.PropertyType), null);
            }
        }
    }


    public int Columns { get => columns; set => columns = value; }
    public Dictionary<T, Color> Colors { get => colors; set => colors = value; }
    public List<ObjectTableRow> ObjectRows { get => objectRows; set => objectRows = value; }

    public void deleteButton_Click(object sender, EventArgs e)
    {
        //Event Data
        Button button = (Button)sender;
        ObjectTableRow row = (ObjectTableRow)button.Parent.Parent;

        //Remove Selected from the table, then make a new dictionary so next postback wont build it anymore
        int rowIndex = this.Rows.GetRowIndex(row);
        this.Rows[rowIndex].Visible = false;
        row.Color = Color.Empty;
    }
}