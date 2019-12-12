using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for EntityTableCell
/// </summary>
public class GameTableCell : TableCell
{
    private GameTableRow row;

    public GameTableCell(GameTableRow row)
    {
        this.row = row;
    }
    

    public GameTableRow Row { get => row; set => row = value; }
}