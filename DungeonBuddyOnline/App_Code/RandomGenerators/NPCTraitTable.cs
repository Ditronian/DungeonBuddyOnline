using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NPCTable
/// </summary>
public static class NPCTraitTable
{

    private static String[] appearances = { "Distinctive Jewelry", "Piercings", "Flamboyant Clothes", "Formal/Clean Clothes", "Ragged/Dirty Clothes", "Scarred", "Missing Teeth",
            "Missing Fingers", "Unusual Eye Color(s)", "Tattoos", "Birthmark","Unusual Skin Color","Bald","Braided beard/hair","Unusual Hair Color","Nervous Eye Twitch",
            "Distinctive Nose","Crooked/Rigid Posture","Very Beautufil","Very Ugly"};


    private static String[] mannerisms = { "Prone to Sing/Hum", "Speaks in rhyme", "Low/High pitch voice", "Slurs/lisps/stutters", "Overly Enunciates", "Speaks loudly",
            "Whispers", "Uses flowery speech", "Often uses wrong words", "Uses oaths/exclamations", "Constantly jokes/puns", "Predicts doom", "Fidgets",
            "Squints", "Stares into the distance", "Chews Something", "Paces", "Taps Fingers", "Bites fingernails", "Twirls Hair/Tugs Beard"};


    private static String[] interactionStyles = { "Argumentative", "Arrogant", "Blustering", "Rude", "Curious", "Friendly", "Honest", "Hot Tempered", "Irritable", "Ponderous", "Quiet", "Suspicious" };

    private static String[] flaws_secrets = { "Forbidden love", "Enjoys decadence", "Arrogance", "Desires npc's item/station", "Overpowering greed", "Prone to rage",
            "Has powerful enemy", "Specific phobia", "Scandalous history", "Secret crime or misdeed", "Possesses forbidden lore", "Foolhardy bravery"};

    private static String[] talents = {"Plays musical instrument", "Speaks many languages", "Unbelievably lucky", "Perfect Memory", "Great with animals", "Great with children", "Great with puzzles",
            "Great at some game", "Great Impersonator", "Draws beautifully", "Paints beautifully", "Good Singer", "Holds his/her liquor", "Expert carpenter", "Expert cook", "Expert dart thrower",
            "Expert Juggler", "Skilled at disquise/acting", "Skilled Dancer", "Knows theives' cant"};

    public static String getAppearance()
    {
        Random random = new Random();
        return appearances[random.Next(appearances.Length)];
    }

    public static String getMannerism()
    {
        Random random = new Random();
        return mannerisms[random.Next(mannerisms.Length)];
    }

    public static String getInteractionStyle()
    {
        Random random = new Random();
        return interactionStyles[random.Next(interactionStyles.Length)];
    }

    public static String getFlawSecret()
    {
        Random random = new Random();
        return flaws_secrets[random.Next(flaws_secrets.Length)];
    }

    public static String getTalent()
    {
        Random random = new Random();
        return talents[random.Next(talents.Length)];
    }
}