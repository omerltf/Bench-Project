using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirBench.Models
{
    public class Review
    {
        public Review () { }
        public Review (int rating, string description, int userId, int benchId)
        {
            this.Rating = rating;
            this.Description = description;
            this.UserId = userId;
            this.BenchId = benchId;
        }

        public int Rating { get; set; }
        [Required]
        public string Description { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BenchId { get; set; }

        public User User { get; set; }
        public Bench Bench { get; set; }
    }
}