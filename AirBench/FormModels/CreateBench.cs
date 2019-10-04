using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.FormModels
{
    public class CreateBench
    {
        public string Description { get; set; }
        public int NumberOfSeats { get; set; }
        public int CreatorUserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}