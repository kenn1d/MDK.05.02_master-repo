using System;

namespace praktika18.Models
{
    public class Ticket
    {
        public int Price { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Ticket() { }

        public Ticket(int price, string from, string to, DateTime startTime, DateTime endTime)
        {
            Price = price;
            From = from;
            To = to;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
