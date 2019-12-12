using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SpellTable
/// </summary>
public class SpellTable
{
    private Random random = new Random();
    private List<String> levelC = new List<String>();
    private List<String> level1 = new List<String>();
    private List<String> level2 = new List<String>();
    private List<String> level3 = new List<String>();
    private List<String> level4 = new List<String>();
    private List<String> level5 = new List<String>();
    private List<String> level6 = new List<String>();
    private List<String> level7 = new List<String>();
    private List<String> level8 = new List<String>();
    private List<String> level9 = new List<String>();

    //Loads all the spells from text files to their appropriate list
    public void initializeSpellTable()
    {
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/LevelC.txt"), Encoding.UTF8)) levelC.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level1.txt"), Encoding.UTF8)) level1.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level2.txt"), Encoding.UTF8)) level2.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level3.txt"), Encoding.UTF8)) level3.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level4.txt"), Encoding.UTF8)) level4.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level5.txt"), Encoding.UTF8)) level5.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level6.txt"), Encoding.UTF8)) level6.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level7.txt"), Encoding.UTF8)) level7.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level8.txt"), Encoding.UTF8)) level8.Add(line);
        foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Spells/Level9.txt"), Encoding.UTF8)) level9.Add(line);
    }

    //Returns a random spell from the appropriate spell list
    public String getRandomSpell(int level)
    {

        if (level == 0) return levelC.ElementAt(random.Next(levelC.Count));
        else if (level == 1) return level1.ElementAt(random.Next(level1.Count));
        else if (level == 2) return level2.ElementAt(random.Next(level2.Count));
        else if (level == 3) return level3.ElementAt(random.Next(level3.Count));
        else if (level == 4) return level4.ElementAt(random.Next(level4.Count));
        else if (level == 5) return level5.ElementAt(random.Next(level5.Count));
        else if (level == 6) return level6.ElementAt(random.Next(level6.Count));
        else if (level == 7) return level7.ElementAt(random.Next(level7.Count));
        else if (level == 8) return level8.ElementAt(random.Next(level8.Count));
        else if (level == 9) return level9.ElementAt(random.Next(level9.Count));
        else return "ERROR";
    }
}
