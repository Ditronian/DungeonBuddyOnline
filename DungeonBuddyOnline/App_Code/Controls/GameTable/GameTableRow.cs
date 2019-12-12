using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

//See comments
public class GameTableRow : TableRow
{
    private Game game;
    private List<GameTableCell> gameCells = new List<GameTableCell>();

    public GameTableRow(Game game)
    {
        this.game = game;
    }

    public Game Game { get => game; set => game = value; }
    public List<GameTableCell> ObjectCells { get => gameCells; set => gameCells = value; }
}