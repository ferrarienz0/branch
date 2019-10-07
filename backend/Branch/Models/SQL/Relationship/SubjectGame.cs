using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class SubjectGame
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public int Affinity { get; set; }

        [Required]
        public Subject IDSubject { get; set; }

        [Required]
        public Game IDGame { get; set; }
    }
}