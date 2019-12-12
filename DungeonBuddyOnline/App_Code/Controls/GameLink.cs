using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GameLink
/// </summary>
public class GameLink : HyperLink
{
    HyperLink gameTypeDropdown;
    HtmlGenericControl carrot;
    string gameCarrotID;

    public GameLink(HyperLink gameTypeDropdown, HtmlGenericControl carrot)
    {
        this.gameTypeDropdown = gameTypeDropdown;
        this.carrot = carrot;
    }

    public HyperLink GameTypeDropdown { get => gameTypeDropdown; set => gameTypeDropdown = value; }
    public HtmlGenericControl Carrot { get => carrot; set => carrot = value; }
    public string GameCarrotID { get => gameCarrotID; set => gameCarrotID = value; }
}