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
public class PageTable : Table
{
    private int columns;
    private CustomPageList pages;
    private List<ObjectTableRow> objectRows = new List<ObjectTableRow>();
    private HashSet<String> names = new HashSet<String>();

    //Takes a list of any kind of objects, an array with the desired parameters to form the columns, and an optional Dictionary of types/colors so you can color code by inherritance.
    public PageTable(CustomPageList pages)
    {
        this.pages = pages;
        //Generate Table
        this.ID = "objectTable";  //Needed so javascript can find the table to sort columns.  Should use a better way.
                                  //Make Header Row, and Buttons so can be sorted by whatever column I want.  Uses JS to sort entire rows.

        TableRow header = new TableRow();

        TableCell indexColumn = new TableCell();
        indexColumn.Text = "Index";
        header.Cells.Add(indexColumn);

        TableCell nameColumn = new TableCell();
        nameColumn.Text = "Page Name";
        header.Cells.Add(nameColumn);

        this.Rows.Add(header);

        if (pages.Pages.Count == 0) header.Visible = false;
        else header.Visible = true;

        //Make the remaining rows, where each row contains one object from the provided List<T>
        for (int i = 0; i< pages.Pages.Count;i++)
        {
            CustomPage page = pages.Pages[i];
            
            ObjectTableRow objectRow = new ObjectTableRow(page, Color.White);
            if (page.MarkedForDeletion == true) objectRow.Visible = false;

            //Make the individual cells for this object, using the provided parameter Names to obtain the desired values.
            TableCell indexCell = new TableCell();
            Label indexLabel = new Label();
            indexLabel.EnableViewState = false;
            indexLabel.CssClass = "regularFont";
            indexLabel.Text = page.SortIndex.ToString();
            indexCell.Controls.Add(indexLabel);
            objectRow.Controls.Add(indexCell);

            ObjectTableCell pageNameCell = new ObjectTableCell(objectRow, "PageName", false);
            objectRow.ObjectCells.Add(pageNameCell);
            objectRow.Controls.Add(pageNameCell);

            TableCell upCell = new TableCell();
            Button upButton = new Button();
            upButton.Text = "+";
            upButton.Click += new EventHandler(upButton_Click);
            upCell.Controls.Add(upButton);
            objectRow.Cells.Add(upCell);

            TableCell downCell = new TableCell();
            Button downButton = new Button();
            downButton.Text = "-";
            downButton.Click += new EventHandler(downButton_Click);
            downCell.Controls.Add(downButton);
            objectRow.Cells.Add(downCell);

            TableCell deleteCell = new TableCell();
            Button deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.Click += new EventHandler(deleteButton_Click);
            deleteCell.Controls.Add(deleteButton);
            objectRow.Cells.Add(deleteCell);
            

            //Add to table
            this.Rows.Add(objectRow);
            objectRows.Add(objectRow);
        }
    }

    public void addRow(CustomPage page)
    {
        names.Add(page.PageName);
        ObjectTableRow objectRow = new ObjectTableRow(page, Color.White);

        //Make the individual cells for this object, using the provided parameter Names to obtain the desired values.
        TableCell indexCell = new TableCell();
        Label indexLabel = new Label();

        indexLabel.Text = page.SortIndex.ToString();
        indexCell.Controls.Add(indexLabel);
        objectRow.Controls.Add(indexCell);

        ObjectTableCell pageNameCell = new ObjectTableCell(objectRow, "PageName", false);
        TextBox nameTextBox = new TextBox();
        nameTextBox.Text = page.PageName;
        pageNameCell.Controls.Add(nameTextBox);
        objectRow.ObjectCells.Add(pageNameCell);
        objectRow.Controls.Add(pageNameCell);

        TableCell upCell = new TableCell();
        Button upButton = new Button();
        upButton.Text = "+";
        upButton.Click += new EventHandler(upButton_Click);
        upCell.Controls.Add(upButton);
        objectRow.Cells.Add(upCell);

        TableCell downCell = new TableCell();
        Button downButton = new Button();
        downButton.Text = "-";
        downButton.Click += new EventHandler(downButton_Click);
        downCell.Controls.Add(downButton);
        objectRow.Cells.Add(downCell);

        TableCell deleteCell = new TableCell();
        Button deleteButton = new Button();
        deleteButton.Text = "Delete";
        deleteButton.Click += new EventHandler(deleteButton_Click);
        deleteCell.Controls.Add(deleteButton);
        objectRow.Cells.Add(deleteCell);


        //Add to table
        this.Rows.Add(objectRow);
        objectRows.Add(objectRow);
        if (objectRow.Color == Color.Empty) objectRow.Visible = false;
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
    public Dictionary<int,CustomPage> getContent()
    {
        //Build a new dictionary with the updated objects then return that updated dictionary
        Dictionary<int,CustomPage> content = new Dictionary<int,CustomPage>();
        foreach (ObjectTableRow objectRow in objectRows)
        {
            CustomPage page = (CustomPage)objectRow.Obj;
            content.Add(page.SortIndex,page);
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
                propertyInfo.SetValue(objectRows[row].Obj, Convert.ChangeType(parameterValue, propertyInfo.PropertyType), null);
            }
        }
    }


    public int Columns { get => columns; set => columns = value; }
    public CustomPageList Pages { get => pages; set => pages = value; }
    public List<ObjectTableRow> ObjectRows { get => objectRows; set => objectRows = value; }
    public HashSet<string> Names { get => names; set => names = value; }

    public void deleteButton_Click(object sender, EventArgs e)
    {
        //Event Data
        Button button = (Button)sender;
        ObjectTableRow row = (ObjectTableRow)button.Parent.Parent;

        //Remove Selected from the table, then make a new dictionary so next postback wont build it anymore
        CustomPage page = (CustomPage) row.Obj;
        page.MarkedForDeletion = true;

        //Save Content and add to table
        saveContentChanges();
        pages.Pages = getContent();
        this.Page.Session["savedContent"] = pages;

        //Reload page to clear any nonsense before loading
        this.Page.Response.Redirect("CustomPageTool.aspx");
    }

    public void upButton_Click(object sender, EventArgs e)
    {
        //Event Data
        Button button = (Button)sender;
        ObjectTableRow selectedRow = (ObjectTableRow)button.Parent.Parent;
        CustomPage selectedPage = (CustomPage)selectedRow.Obj;

        if (selectedPage.SortIndex == 0) return;  //Cannot go any higher

        ObjectTableRow previousRow = objectRows[selectedPage.SortIndex-1];
        CustomPage previousPage = (CustomPage)previousRow.Obj;

        //Make tables correspond to the new change
        TableRow temp = this.Rows[selectedPage.SortIndex+1];

        this.Rows.RemoveAt(selectedPage.SortIndex+1);
        this.Rows.AddAt(previousPage.SortIndex+1, temp);

        pages.Pages[previousPage.SortIndex] = selectedPage;
        pages.Pages[selectedPage.SortIndex] = previousPage;

        previousPage.SortIndex++;
        selectedPage.SortIndex--;

        ((Label)selectedRow.Cells[0].Controls[0]).Text = selectedPage.SortIndex.ToString();
        ((Label)previousRow.Cells[0].Controls[0]).Text = previousPage.SortIndex.ToString();

        //Save Content and add to table
        saveContentChanges();
        pages.Pages = getContent();
        this.Page.Session["savedContent"] = pages;

        //Reload page to clear any nonsense before loading
        this.Page.Response.Redirect("CustomPageTool.aspx");
    }

    public void downButton_Click(object sender, EventArgs e)
    {
        //Event Data
        Button button = (Button)sender;
        ObjectTableRow selectedRow = (ObjectTableRow)button.Parent.Parent;
        CustomPage selectedPage = (CustomPage)selectedRow.Obj;

        if (selectedPage.SortIndex == this.Rows.Count-2) return;  //Cannot go any lower, -2 because of this row AND header row

        ObjectTableRow nextRow = objectRows[selectedPage.SortIndex + 1];
        CustomPage nextPage = (CustomPage)nextRow.Obj;

        //Make tables correspond to the new change
        TableRow temp = this.Rows[nextPage.SortIndex+1];

        this.Rows.RemoveAt(nextPage.SortIndex+1);
        this.Rows.AddAt(selectedPage.SortIndex+1, temp);

        pages.Pages[nextPage.SortIndex] = selectedPage;
        pages.Pages[selectedPage.SortIndex] = nextPage;

        nextPage.SortIndex--;
        selectedPage.SortIndex++;

        ((Label)selectedRow.Cells[0].Controls[0]).Text = selectedPage.SortIndex.ToString();
        ((Label)nextRow.Cells[0].Controls[0]).Text = nextPage.SortIndex.ToString();

        //Save Content and add to table
        saveContentChanges();
        pages.Pages = getContent();
        this.Page.Session["savedContent"] = pages;

        //Reload page to clear any nonsense before loading
        this.Page.Response.Redirect("CustomPageTool.aspx");
    }
}