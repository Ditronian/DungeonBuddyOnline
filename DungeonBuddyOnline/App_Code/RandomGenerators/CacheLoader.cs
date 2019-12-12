using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;


public static class CacheLoader
{
    public static void loadRandomTables()
    {
         
        Dictionary<String, NameTable> nameTables = new Dictionary<string, NameTable>();
        nameTables.Add("humanNames", new NameTable("human"));
        nameTables.Add("elvenNames", new NameTable("elven"));
        nameTables.Add("dwarvenNames", new NameTable("dwarven"));
        nameTables.Add("halflingNames", new NameTable("halfling"));
        nameTables.Add("gnomishNames", new NameTable("gnomish"));
        nameTables.Add("gnollNames", new NameTable("gnoll"));

        MagicItemTable magicItemTable = new MagicItemTable();
        magicItemTable.initializeMagicItemTables();
        SpellTable spellTable = new SpellTable();
        spellTable.initializeSpellTable();

        HttpRuntime.Cache.Insert("names", nameTables, null,Cache.NoAbsoluteExpiration,Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable,null);
        HttpRuntime.Cache.Insert("items", magicItemTable, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        HttpRuntime.Cache.Insert("spells", spellTable, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
    }
}