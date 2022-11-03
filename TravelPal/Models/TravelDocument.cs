using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class TravelDocument : IPackingListItem
    {
        public string Name { get; set; }
        public bool Required { get; set; }

        //Constructor taking required parameters to create a new TravelDocument
        public TravelDocument(string name, bool required)
        {
            Name = name;
            Required = required;
        }

        //Returns a string containing the item's name and required-status
        public override string ToString()
        {
            if (Required)
            {
                return $"{Name} | Required";
            }

            return $"{Name} | Not required";
        }
    }
}
