using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class UserSubject
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public int Affinity { get; set; }

        [Required]
        public User IDUser { get; set; }

        [Required]
        public Subject IDSubject { get; set; }
    }
}