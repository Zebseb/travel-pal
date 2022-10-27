using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal.Models
{
    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        public override string GetInfo()
        {
            return $"All Inclusive: {AllInclusive}";
        }
    }
}
