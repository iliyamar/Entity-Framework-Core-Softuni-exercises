namespace Social2.DomainClasses
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class TagTransofrmer
    {
        
        public static string Transform(string tag)
        {
            var repairedTag = String.Empty;

            if (!tag.StartsWith("#"))
            {
                tag = "#" + tag;
            }

            tag = Regex.Replace(tag, @"\s+", "");
            
            if (tag.Length>20)
            {
                tag = tag.Substring(0,20);
            }

            Console.WriteLine($"{tag} was added to the database");

            return tag;
        }


    }
}
