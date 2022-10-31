using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Interfaces;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public class TravelManager
    {
        private List<Travel> travels = new();

        public TravelManager()
        {

        }

        public List<Travel> GetTravels()
        {
            return travels;
        }

        //Adds a travel to the travels-list.
        public void AddTravel(Travel newTravel)
        {
            travels.Add(newTravel);
        }

        //Removes a travel from the travels-list.
        public void RemoveTravel(Travel travelToRemove)
        {
            travels.Remove(travelToRemove);
        }
    }
}
