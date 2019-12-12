using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomPage
/// </summary>
public class CustomPage
{
    private int pageID;
    private int gameID;
    private string pageName;
    private string pageURL;
    private int sortIndex;
    private bool markedForDeletion = false;
    
    

    public int PageID { get => pageID; set => pageID = value; }
    public string PageName { get => pageName; set => pageName = value; }
    public string PageURL { get => pageURL; set => pageURL = value; }
    public int GameID { get => gameID; set => gameID = value; }
    public int SortIndex { get => sortIndex; set => sortIndex = value; }
    public bool MarkedForDeletion { get => markedForDeletion; set => markedForDeletion = value; }
}