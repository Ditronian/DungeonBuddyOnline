using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NPC
/// </summary>

public class NPC : Entity
{
    private int npcID;
    private String bio;
    private String location;
    private String sex;

    //Traits
    private String appearance;
    private String mannerism;
    private String interactionStyle;
    private String flaw_secret;
    private String talent;

    public string Bio { get => bio; set => bio = value; }
    public string Location { get => location; set => location = value; }
    public string Sex { get => sex; set => sex = value; }
    public string Appearance { get => appearance; set => appearance = value; }
    public string Mannerism { get => mannerism; set => mannerism = value; }
    public string InteractionStyle { get => interactionStyle; set => interactionStyle = value; }
    public string Flaw_secret { get => flaw_secret; set => flaw_secret = value; }
    public string Talent { get => talent; set => talent = value; }
    public int NpcID { get => npcID; set => npcID = value; }
}
