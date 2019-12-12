using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MagicItem
/// </summary>
public class MagicItem
{
    private int magicItemID;
    private String name;
    private String rarity;
    private int maximumValue;
    private int minimumValue;
    private int value;
    private MagicShop shop;

    public string Name { get => name; set => name = value; }
    public string Rarity { get => rarity; set => rarity = value; }
    public int Value { get => value; set => this.value = value; }
    public int MaximumValue { get => maximumValue; set => maximumValue = value; }
    public int MinimumValue { get => minimumValue; set => minimumValue = value; }
    public int MagicItemID { get => magicItemID; set => magicItemID = value; }
    public MagicShop Shop { get => shop; set => shop = value; }
}