using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomPageList
/// </summary>
public class CustomPageList
{
    private int gameID;
    private Dictionary<int, CustomPage> pages = new Dictionary<int, CustomPage>();
    private String message;

    public int GameID { get => gameID; set => gameID = value; }
    public Dictionary<int, CustomPage> Pages { get => pages; set => pages = value; }
    public string Message { get => message; set => message = value; }
}