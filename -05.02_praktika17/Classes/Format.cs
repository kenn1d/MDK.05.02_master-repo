using System.Collections.Generic;

namespace WpfApp1.Classes
{
    public class Format
    {
        public int id { get; set; }
        public string format { get; set; }
        public string description { get; set; }
        public Format(int id, string format, string description)
        {
            this.id = id;
            this.format = format;
            this.description = description;
        }
        public static List<Format> AllFormats()
        {
            List<Format> allFormat = new List<Format>();

            allFormat.Add(new Format(1, "А4", ""));
            allFormat.Add(new Format(2, "А3", ""));
            allFormat.Add(new Format(3, "А2", ""));
            allFormat.Add(new Format(4, "А1", ""));

            return allFormat;
        }
    }
}
