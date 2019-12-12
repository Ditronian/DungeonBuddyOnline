using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PC
/// </summary>
public class PartyMember : Entity
{

    private int perception;
    private char size;
    private bool isNpc = false;
    private int partyMemberID;
    private int userID;

    //New Party Member
    public PartyMember(String name, String race, int passivePerception, char size)
    {
        this.name = name;
        this.Race = race;
        this.perception = passivePerception;
        this.Size = size;
    }

    //From Database
    public PartyMember(int partyMemberID, int entityID, String name, String race, int passivePerception, char size)
    {
        this.PartyMemberID = partyMemberID;
        this.EntityID = entityID;
        this.name = name;
        this.Race = race;
        this.perception = passivePerception;
        this.Size = size;
    }

    //Combat Only PartyMember
    public PartyMember(String name, int initiative, int maxHP, int ac)
    {
        this.name = name;
        this.maxHP = maxHP;
        this.currentHP = maxHP;
        this.initiative = initiative;
        this.armorClass = ac;
    }

    public PartyMember()
    {
        //Blank constructor
    }

    public int Perception { get => perception; set => perception = value; }
    public char Size { get => size; set => size = value; }
    public bool IsNpc { get => isNpc; set => isNpc = value; }
    public int PartyMemberID { get => partyMemberID; set => partyMemberID = value; }
    public int UserID { get => userID; set => userID = value; }
}