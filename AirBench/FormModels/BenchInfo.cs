using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.FormModels
{
    public class BenchInfo
    {
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int NumberOfSeats { get; set; }

    }
}