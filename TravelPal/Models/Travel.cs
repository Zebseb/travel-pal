using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;
using TravelPal.Interfaces;

namespace TravelPal.Models
{
    public class Travel
    {
        public string Destination { get; set; }
        public int Travelers { get; set; }
        public Countries Country { get; set; }
        public List<IPackingListItem> PackingList { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }

        //Constructor taking no parameters to create a new Travel
        public Travel()
        {

        }

        //Constructor taking required parameters to create a new Travel
        public Travel(string destination, int travelers, Countries country, DateTime startDate, DateTime endDate)
        {
            this.Destination = destination;
            this.Travelers = travelers;
            this.Country = country;
            this.StartDate = startDate;
            this.EndDate = endDate;
            CalculateTravelDays();
        }

        //Calculates the number of traveling days
        private void CalculateTravelDays()
        {
            int travelDays;
            System.TimeSpan diff = EndDate.Subtract(StartDate);
            travelDays = int.Parse(diff.Days.ToString());
            this.TravelDays = travelDays;
        }

        //Returns a formatted start date-string
        public string GetFormattedStartDate()
        {
            return $"{StartDate.Year}-{StartDate.Month}-{StartDate.Day}";
        }

        //Returns a formatted end date-string
        public string GetFormattedEndDate()
        {
            return $"{EndDate.Year}-{EndDate.Month}-{EndDate.Day}";
        }

        //Returns a string containing information about the Travel
        public virtual string GetInfo()
        {
            return $"Destination: {Destination} | Departure: {Country.ToString()} | Num. of travelers: {Travelers}";
        }
    }
}
