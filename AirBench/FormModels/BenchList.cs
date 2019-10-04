using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.FormModels
{
    public class BenchList
    {
        public string Description { get; set; }
        public int NumberOfSeats { get; set; }
        public int CreatorUserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Id { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfReviews { get; set; }
    }
}