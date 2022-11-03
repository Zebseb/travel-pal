using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class OtherItem : IPackingListItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        //Constructor taking required parameters to create a new OtherItem
        public OtherItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        //Returns a string containing the name and quantity of the item
        public override string ToString()
        {
            return $"{Name} | Quantity: {Quantity}";
        }
    }
}
