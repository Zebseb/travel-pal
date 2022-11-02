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
        public List<IPackingListItem> PackingList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }

        public Travel()
        {

        }

        public Travel(string destination, int travelers, Countries country, DateTime startDate, DateTime endDate)
        {
            this.Destination = destination;
            this.Travelers = travelers;
            this.Country = country;
            this.StartDate = startDate;
            this.EndDate = endDate;
            CalculateTravelDays();
        }

        private void CalculateTravelDays()
        {
            int travelDays;
            System.TimeSpan diff = EndDate.Subtract(StartDate);
            travelDays = int.Parse(diff.Days.ToString());
            this.TravelDays = travelDays;
        }

        public string GetFormattedStartDate()
        {
            return $"{StartDate.Year}-{StartDate.Month}-{StartDate.Day}";
        }

        public string GetFormattedEndDate()
        {
            return $"{EndDate.Year}-{EndDate.Month}-{EndDate.Day}";
        }

        public virtual string GetInfo()
        {
            return $"Destination: {Destination} | Departure: {Country.ToString()} | Num. of travelers: {Travelers}";
        }
    }
}
