using System;

namespace ANCBE.Models
{
    public class ArchivedPostsSummary
    {
        public DateTime Date
        {
            get
            {
                return new DateTime(Year, Month, 1);
            }
        }

        public int Count { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
