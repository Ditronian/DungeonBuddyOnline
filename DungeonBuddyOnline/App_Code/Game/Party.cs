using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Party
/// </summary>
public class Party
{
    private Dictionary<PartyMember, Color> partyMembers = new Dictionary<PartyMember, Color>();
    private int gameID;



    public Dictionary<PartyMember, Color> PartyMembers { get => partyMembers; set => partyMembers = value; }
    public int GameID { get => gameID; set => gameID = value; }
}