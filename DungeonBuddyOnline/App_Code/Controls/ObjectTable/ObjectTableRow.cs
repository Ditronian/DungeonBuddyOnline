using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

//See comments
public class ObjectTableRow : TableRow
{
    private Object obj;
    private Type type;
    private List<ObjectTableCell> objectCells = new List<ObjectTableCell>();
    private Color color;

    public ObjectTableRow(Object obj, Color color)
    {
        this.obj = obj;
        this.type = obj.GetType();
        this.color = color;
    }

    public Object Obj { get => obj; set => obj = value; }
    public Type Type { get => type; set => type = value; }
    public List<ObjectTableCell> ObjectCells { get => objectCells; set => objectCells = value; }
    public Color Color { get => color; set => color = value; }
}