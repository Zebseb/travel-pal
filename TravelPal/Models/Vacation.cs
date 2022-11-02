using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        public Vacation(bool allInclusive, string destination, int travelers, Countries country, DateTime startDate, DateTime endDate) : base(destination, travelers, country, startDate, endDate)
        {
            this.AllInclusive = allInclusive;
        }

        public override string GetInfo()
        {
            return $"Destination: {Destination} | Travel days: {TravelDays}\n";  
        }
    }
}
