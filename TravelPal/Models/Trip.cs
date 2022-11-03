using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Models
{
    public class Trip : Travel
    {
        public TripTypes Type { get; set; }

        //Constructor taking required parameters to create a new Trip
        public Trip(TripTypes type, string destination, int travelers, Countries country, DateTime startDate, DateTime endDate) : base(destination, travelers, country, startDate, endDate)
        {
            this.Type = type;
        }

        //Returns a string contatining the Trip's destination and number of traveling days
        public override string GetInfo()
        {
            return $"Destination: {Destination} | Travel days: {TravelDays}\n";
        }
    }
}
