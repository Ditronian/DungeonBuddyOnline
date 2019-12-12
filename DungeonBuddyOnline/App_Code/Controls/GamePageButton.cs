using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

//A modified LinkButton to reference key bits of database data to the button itself, to make my life easier.
public class GamePageButton : LinkButton
{
    private GameLink gameLink;
    private int gameID;
    private int pageID;
    private string url;
    private Game game;
    private bool isCustomPage;

    public GamePageButton(GameLink gameLink, String url, Game game, bool isCustomPage = false)
    {
        this.gameLink = gameLink;
        this.url = url;
        this.game = game;
        this.isCustomPage = isCustomPage;
    }

    public int GameID { get => gameID; set => gameID = value; }
    public int PageID { get => pageID; set => pageID = value; }
    public string Url { get => url; set => url = value; }
    public GameLink GameLink { get => gameLink; set => gameLink = value; }
    public Game Game { get => game; set => game = value; }
    public bool IsCustomPage { get => isCustomPage; set => isCustomPage = value; }
}