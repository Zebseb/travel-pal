﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPal.Enums;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        //Constructor taking required parameters to create a new Vacation
        public Vacation(bool allInclusive, string destination, int travelers, Countries country, DateTime startDate, DateTime endDate) : base(destination, travelers, country, startDate, endDate)
        {
            this.AllInclusive = allInclusive;
        }

        //Returns a string contatining the Vacation's destination and number of traveling days
        public override string GetInfo()
        {
            return $"Destination: {Destination} | Travel days: {TravelDays}";  
        }
    }
}
