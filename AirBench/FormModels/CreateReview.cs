using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirBench.FormModels
{
    public class CreateReview
    {
        [Range (1,5)]
        public int Rating { get; set; }
        [Required]
        public string Description { get; set; }
        public int BenchId { get; set; }
    }
}