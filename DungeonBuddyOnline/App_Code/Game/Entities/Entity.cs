using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Entity
/// </summary>

public abstract class Entity
{
    protected string name;
    private string race;
    protected int initiative;
    protected int currentHP;
    protected int maxHP;
    protected int armorClass;
    private int entityID;
    private int gameID;

    public string Name { get => name; set => name = value; }
    public string Race { get => race; set => race = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    public int Initiative { get => initiative; set => initiative = value; }
    public int ArmorClass { get => armorClass; set => armorClass = value; }
    public int EntityID { get => entityID; set => entityID = value; }
    public int GameID { get => gameID; set => gameID = value; }
}