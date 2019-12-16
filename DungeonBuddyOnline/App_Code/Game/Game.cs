using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Game
/// </summary>
public class Game
{
    private int gameID;
    private string gameName = "";
    private string gameSetting = "";
    private string gameDescription = "";
    private string gameAdditionalInfo = "";
    private string imagePath = "";
    private bool acceptsPlayers;
    private bool fullyLoaded = false;
    private DateTime createdDate;

    public int GameID { get => gameID; set => gameID = value; }
    public string GameName { get => gameName; set => gameName = value; }
    public string GameSetting { get => gameSetting; set => gameSetting = value; }
    public string GameDescription { get => gameDescription; set => gameDescription = value; }
    public string GameAdditionalInfo { get => gameAdditionalInfo; set => gameAdditionalInfo = value; }
    public bool AcceptsPlayers { get => acceptsPlayers; set => acceptsPlayers = value; }
    public string ImagePath { get => imagePath; set => imagePath = value; }
    public bool FullyLoaded { get => fullyLoaded; set => fullyLoaded = value; }
    public DateTime CreatedDate { get => createdDate; set => createdDate = value; }
}