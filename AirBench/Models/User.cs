using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirBench.Models
{
    public class User
    {
        public User() { }              //empty constructor

        //initializing constructor
        public User(int id, string name, string userName, string hashedPassword)
        {
            Id = id;
            Name = name;
            UserName = userName;
            HashedPassword = hashedPassword;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string HashedPassword { get; set; }
    }
}