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

        public Trip(TripTypes type, string destination, int travelers, Countries country) : base(destination, travelers, country)
        {
            this.Type = type;
        }

        public override string GetInfo()
        {
            return $"Destination: {Destination}\n";
        }
    }
}
