using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AirBench.Models
{
    public class Bench
    {
        public Bench() { }
        public Bench (string description, int numberOfSeats, int creatorUserId, decimal latitude, decimal longitude)
        {
            this.Description = description;
            this.NumberOfSeats = numberOfSeats;
            this.CreatorUserId = creatorUserId;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        [Required]
        public string Description { get; set; }
        public int NumberOfSeats { get; set; }
        public int CreatorUserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Id { get; set; }

        [ForeignKey("CreatorUserId")]
        public User User { get; set; }
    }
}