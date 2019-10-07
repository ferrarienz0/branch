using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class User
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname{ get; set; }

        [Required][Index(IsUnique = true)]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required][Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

    }
}