using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Converters;
using TravelPal.Enums;
using TravelPal.Interfaces;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public class UserManager
    {
        private TravelManager travelManager = new();
        private List<IUser> users = new();
        public IUser signedInUser;

        public UserManager()
        {
            
            Admin newAdmin = new("admin", "password");
            users.Add(newAdmin);

            User newUser = new("Gandalf", "password", Countries.Sweden);
            users.Add(newUser); 

            Vacation newVacation = new(true, "Madagascar", 2, Enums.Countries.Sweden);
            travelManager.AddTravel(newVacation);
            newUser.travels.Add(newVacation);

            Trip newTrip = new(Enums.TripTypes.Leisure, "Sydney", 4, Enums.Countries.Denmark);
            travelManager.AddTravel(newTrip);
            newUser.travels.Add(newTrip);
        }

        //Returns all users (Users and Admins) in the users-list.
        public List <IUser> GetUsers()
        {
            return users;
        }

        //Returns a true value if a user was added to the users-list or returns a false value if the username the username is taken.
        public bool AddUser(string username, string password, Countries country)
        {
            bool isAvailableUsername = ValidateUsername(username);

            if (isAvailableUsername)
            {
                User registeredUser = new(username, password, country);
                users.Add(registeredUser);
                return true;
            }

            return false;
        }

        //Updates the user's username if the username is available and returns a true value. Returns a false value if the username is already taken.
        public bool UpdateUsername(User user, string username)
        {
            bool isAvailableUsername = ValidateUsername(username);

            if (isAvailableUsername)
            {
                user.Username = username;

                return true;
            }

            return false;
        }

        //Checks if the wanted username is available or not.
        private bool ValidateUsername(string username)
        {
            foreach (IUser user in users)
            {
                if (user.Username == username)
                {
                    return false;
                }
            }

            return true;
        }

        //Sets the signedInUser variable if the username and password matches with a users' in the users-list.
        public bool SignInUser(string username, string password)
        {
            foreach (IUser user in users)
            {
                if (username == user.Username && password == user.Password)
                {
                    signedInUser = user;

                    return true;
                }
            }

            return false;
        }
    }
}
