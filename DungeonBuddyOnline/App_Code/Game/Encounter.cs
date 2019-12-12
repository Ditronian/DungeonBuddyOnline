using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Encounter
/// </summary>
public class Encounter
{
    private int encounterID;
    private int gameID;
    private string encounterName = "";
    private Dictionary<Entity, Color> entities = new Dictionary<Entity, Color>();

    public int EncounterID { get => encounterID; set => encounterID = value; }
    public string EncounterName { get => encounterName; set => encounterName = value; }
    public Dictionary<Entity, Color> Entities { get => entities; set => entities = value; }
    public int GameID { get => gameID; set => gameID = value; }
}