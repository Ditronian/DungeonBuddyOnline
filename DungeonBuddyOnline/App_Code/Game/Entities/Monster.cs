using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Monster
/// </summary>
public class Monster : Entity
{
    private bool isFriendly = false;
    private int monsterID;
    private Encounter encounter;

    public bool IsFriendly { get => isFriendly; set => isFriendly = value; }
    public int MonsterID { get => monsterID; set => monsterID = value; }
    public Encounter Encounter { get => encounter; set => encounter = value; }
}