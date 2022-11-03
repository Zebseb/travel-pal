using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal.Interfaces
{
    public interface IPackingListItem
    {
        public string Name { get; set; }

        //Returns a string containing the item's name
        public virtual string ToString()
        {
            return $"{Name}";
        }
    }
}
