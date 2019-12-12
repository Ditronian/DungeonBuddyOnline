using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MagicShop
/// </summary>
public class MagicShop
{
    private int shopID;
    private int gameID;
    private Dictionary<MagicItem, Color> items = new Dictionary<MagicItem, Color>();
    private string shopName = "";
    private string shopQuality;

    public int ShopID { get => shopID; set => shopID = value; }
    public int GameID { get => gameID; set => gameID = value; }
    public Dictionary<MagicItem, Color> Items { get => items; set => items = value; }
    public string ShopName { get => shopName; set => shopName = value; }
    public string ShopQuality { get => shopQuality; set => shopQuality = value; }
}