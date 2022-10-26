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
        private List<IUser> users = new();
        public IUser signedInUser;

        public UserManager()
        {
            Admin newAdmin = new("admin", "password");
            users.Add(newAdmin);
            User newUser = new("Gandalf", "password", Countries.Sweden);
            users.Add(newUser);
        }

        public List <IUser> GetUsers()
        {
            return users;
        }

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

        public bool UpdateUsername()
        {
            ValidateUsername(username);
            return true;
        }

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

        public bool SignInUser(string username, string password)
        {
            bool isFoundUser = false;

            foreach (IUser user in users)
            {
                if (username == user.Username && password == user.Password)
                {
                    isFoundUser = true;
                    signedInUser = user;

                    return isFoundUser;
                }
            }

            return isFoundUser;
        }
    }
}
