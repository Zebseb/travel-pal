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

        public virtual string GetInfo()
        {
            return "";
        }
    }
}
