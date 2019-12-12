using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for MagicItemTable
/// </summary>
public class MagicItemTable
{
    private Random random = new Random();
    private  MagicItem[] magicItemTableA = new MagicItem[100];
    private  MagicItem[] magicItemTableB = new MagicItem[100];
    private  MagicItem[] magicItemTableC = new MagicItem[100];
    private  MagicItem[] magicItemTableD = new MagicItem[100];
    private  MagicItem[] magicItemTableE = new MagicItem[100];
    private  MagicItem[] magicItemTableF = new MagicItem[100];
    private  MagicItem[] magicItemTableG = new MagicItem[100];
    private  MagicItem[] magicItemTableH = new MagicItem[100];
    private  MagicItem[] magicItemTableI = new MagicItem[100];

    //Fills in all the Magic Item Tables from text files
    public void initializeMagicItemTables()
    {
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableA.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableA);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableB.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableB);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableC.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableC);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableD.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableD);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableE.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableE);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableF.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableF);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableG.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableG);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableH.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableH);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/MagicItems/MagicItemTableI.txt"), Encoding.UTF8)) disectAndAddItem(line, magicItemTableI);
    }

    //Recieves a line of input and its corresponding item table, and inserts the item into the applicable indexes in that ItemTable's array
    private  void disectAndAddItem(string line, MagicItem[] magicItemTable)
    {
        
        //Get Item
        String[] lineArray = line.Split('\t');
        String diceRolls = lineArray[0];
        String name = lineArray[1];
        String rarity = lineArray[2];
        String valueRange = lineArray[3];

        int maxValue;
        int minValue;
        int value;
        //Get Values
        if (valueRange.Contains('-'))
        {
            String[] splitValue = valueRange.Split('-');
            minValue = Int32.Parse(splitValue[0]);
            maxValue = Int32.Parse(splitValue[1]);
            value = random.Next(minValue, maxValue+1);
        }
        else if (valueRange.Contains('+'))
        {
            String parsedValue = valueRange.Trim('+');
            minValue = Int32.Parse(parsedValue);
            maxValue = Int32.Parse(parsedValue)*3;
            value = random.Next(minValue, maxValue + 1);
        }
        else
        {
            minValue = Int32.Parse(valueRange);
            maxValue = Int32.Parse(valueRange);
            value = Int32.Parse(valueRange);
        }

        //Get dice rolls of this item
        int startPos;
        int endPos;
        if (diceRolls.Length == 2)
        {
            startPos = Int32.Parse(Char.ToString(diceRolls[0]) + Char.ToString(diceRolls[1]));
            endPos = startPos;
        }
        else if (diceRolls.Length == 3)
        {
            startPos = Int32.Parse(Char.ToString(diceRolls[0]) + Char.ToString(diceRolls[1]) + Char.ToString(diceRolls[2]));
            endPos = startPos;
        }
        else
        {
            startPos = Int32.Parse(Char.ToString(diceRolls[0]) + Char.ToString(diceRolls[1]));
            endPos = Int32.Parse(Char.ToString(diceRolls[3]) + Char.ToString(diceRolls[4]));
        }

        //Add the item to the applicable indexes of the table
        for (int i = startPos - 1; i < endPos; i++)
        {
            MagicItem item = new MagicItem();
            item.Name = name;
            item.Rarity = rarity;
            item.Value = value;
            item.MaximumValue = maxValue;
            item.MinimumValue = minValue;
            magicItemTable[i] = item;
        }
    }

    //Randomly gets a magic item from the appropriate Item Table, and assigns spells to any scrolls
    public  MagicItem getItem(String letter)
    {
        MagicItem item;

        if (letter == "A") item = magicItemTableA.ElementAt(random.Next(magicItemTableA.Length));
        else if (letter == "B") item = magicItemTableB.ElementAt(random.Next(magicItemTableB.Length));
        else if (letter == "C") item = magicItemTableC.ElementAt(random.Next(magicItemTableC.Length));
        else if (letter == "D") item = magicItemTableD.ElementAt(random.Next(magicItemTableD.Length));
        else if (letter == "E") item = magicItemTableE.ElementAt(random.Next(magicItemTableE.Length));
        else if (letter == "F") item = magicItemTableF.ElementAt(random.Next(magicItemTableF.Length));
        else if (letter == "G") item = magicItemTableG.ElementAt(random.Next(magicItemTableG.Length));
        else if (letter == "H") item = magicItemTableH.ElementAt(random.Next(magicItemTableH.Length));
        else if (letter == "I") item = magicItemTableI.ElementAt(random.Next(magicItemTableI.Length));
        else
        {
            item = new MagicItem();
            item.Name = "ERROR";
            item.Rarity = "ERROR";
            item.Value = 0;
            item.MaximumValue = 0;
            item.MinimumValue = 0;
        }

        //Make a copy of the item so any changes dont mess with the table
        MagicItem itemCopy = new MagicItem();
        itemCopy.Name = item.Name;
        itemCopy.Rarity = item.Rarity;
        itemCopy.Value = item.Value;
        itemCopy.MaximumValue = item.MaximumValue;
        itemCopy.MinimumValue = item.MinimumValue;

        //If grabbed item is a scroll, determine its level then acquire a random spell of that level and append the spell name to the item
        if (itemCopy.Name.Contains("scroll"))
        {
            int level = 0;

            //Check for #, which will be the spell lvl
            foreach (Char c in itemCopy.Name) if (Int32.TryParse(Char.ToString(c), out int spellLvl)) level = spellLvl;

            if (HttpRuntime.Cache.Get("spells") == null) CacheLoader.loadRandomTables();
            SpellTable spellsTable = (SpellTable)HttpRuntime.Cache.Get("spells");

            //Grab a random spell of the appropriate level and add to name
            itemCopy.Name += $" of {spellsTable.getRandomSpell(level)}";
        }

        return itemCopy;
    }
}
