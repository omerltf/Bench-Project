using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AirBench.FormModels
{
    public class CreateUser
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        public string Password {get; set;}
    }
}