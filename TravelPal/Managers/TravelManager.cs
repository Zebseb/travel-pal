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
        private UserManager userManager;
        private List<Travel> travels = new();
        private List<IUser> users;

        public TravelManager()
        {

        }

        //public TravelManager(UserManager userManager)
        //{
        //  Vacation newVacation = new(true, "Madagascar", 2, Enums.Countries.Sweden);
        //  travels.Add(newVacation);
        //  Trip newTrip = new(Enums.TripTypes.Leisure, "Sydney", 4, Enums.Countries.Denmark);
        //  travels.Add(newTrip);

        //  this.userManager = userManager;
        //  this.users = userManager.GetUsers();
        //}

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
        public void RemoveTravel()
        {

        }
    }
}
