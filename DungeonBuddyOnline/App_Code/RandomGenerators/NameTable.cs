using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class NameTable
    {
        String race;
        Random random = new Random();
        //Hashsets are like hashmaps in list form.  Should be quick searches
        HashSet<String> maleFirstNames = new HashSet<String>();
        HashSet<String> femaleFirstNames = new HashSet<String>();
        HashSet<String> lastNames = new HashSet<String>();
        HashSet<String[]> maleFullNames = new HashSet<String[]>();
        HashSet<String[]> femaleFullNames = new HashSet<String[]>();
    

        //Adds values to the name lists
        public NameTable(String race)
        {
            this.race = race;

            //Adds the three sets of names to their lists
            foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Names/" + race + "MaleNames.txt"), Encoding.UTF8)) disectLine(true, line);
            foreach (String line in File.ReadLines(HttpContext.Current.Server.MapPath("~/App_Data/Names/" + race + "FemaleNames.txt"), Encoding.UTF8)) disectLine(false, line);
        }

        //Splits a line from the text file into its firstname and lastname components, then adds to the appropriate list
        private void disectLine(bool isMale, String line)
        {
            String[] lineArray = line.Split(' ');
            String firstName = lineArray[0];
            String lastName = null;

            //Check if the line even has a last name included
            if (lineArray.Length >= 2)
            {
                lastName = lineArray[1];
                if (isMale) maleFullNames.Add(lineArray);
                else femaleFullNames.Add(lineArray);
            }

            //Add to appropriate gender list if not already contained
            if (isMale && !maleFirstNames.Contains(firstName)) maleFirstNames.Add(firstName);
            else if (!isMale && !femaleFirstNames.Contains(firstName)) femaleFirstNames.Add(firstName);

            //Add to last name list if not already contained and a last name is present
            if (lastName != null && !lastNames.Contains(firstName)) lastNames.Add(lastName);
        }


        //Returns a randomly chosen male first name
        public String getMaleFirstName()
        {
            String name = maleFirstNames.ElementAt(random.Next(maleFirstNames.Count));
            return name;
        }

        //Returns a randomly chosen female first name
        public String getFemaleFirstName()
        {

            String name = femaleFirstNames.ElementAt(random.Next(femaleFirstNames.Count));
            return name;
        }

        //Returns a randomly chosen last name
        public String getLastName()
        {
            String name = lastNames.ElementAt(random.Next(lastNames.Count));
            return name;
        }

        //Returns a randomly chosen full name.  These are more likely to match styles for first/last name
        public String[] getMaleFullName()
        {
            String[] name = maleFullNames.ElementAt(random.Next(maleFullNames.Count));
            return name;
        }

        //Returns a randomly chosen full name.  These are more likely to match styles for first/last name
        public String[] getFemaleFullName()
        {
            String[] name = femaleFullNames.ElementAt(random.Next(femaleFullNames.Count));
            return name;
        }
    }
