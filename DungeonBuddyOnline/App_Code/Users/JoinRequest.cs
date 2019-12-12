using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JoinRequest
/// </summary>
public class JoinRequest
{
    private User user;
    private PartyMember partyMember;
    private int gameID;

    public User User { get => user; set => user = value; }
    public PartyMember PartyMember { get => partyMember; set => partyMember = value; }
    public int GameID { get => gameID; set => gameID = value; }
}