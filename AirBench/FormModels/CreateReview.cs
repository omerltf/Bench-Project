using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.FormModels
{
    public class CreateReview
    {
        public int Rating { get; set; }
        public string Description { get; set; }
        public int BenchId { get; set; }
    }
}