using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Models
{
    public class Travel
    {
        public string Destination { get; set; }
        public int Travelers { get; set; }
        public Countries Country { get; set; }

        public Travel()
        {

        }

        public Travel(string destination, int travelers, Countries country)
        {
            this.Destination = destination;
            this.Travelers = travelers;
            this.Country = country;
        }

        public virtual string GetInfo()
        {
            return $"Destination: {Destination} | Departure: {Country.ToString()} | Num. of travelers: {Travelers}";
        }
    }
}
