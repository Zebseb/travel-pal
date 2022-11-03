using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Admin : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        //Constructor taking required parameters to create a new Admin
        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
