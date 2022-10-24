using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Interfaces;

namespace TravelPal.Managers
{
    public class UserManager
    {
        public List<IUser> users = new();
        public IUser signedInUser;

        public UserManager()
        {
            
        }

        public void AddUser()
        {

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
