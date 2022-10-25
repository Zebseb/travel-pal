using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;
using TravelPal.Interfaces;
using TravelPal.Models;

namespace TravelPal.Managers
{
    public class UserManager
    {
        public List<IUser> users = new();
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

        public void AddUser(string username, string password)
        {
            //User newUser = new(username, password, Countries location);
            //users.Add(newUser);

        }

        public void RemoveUser()
        {

        }

        public void UpdateUsername()
        {
            ValidateUsername();
        }

        private void ValidateUsername()
        {
            
        }

        public void SignInUser()
        {

        }
    }
}
