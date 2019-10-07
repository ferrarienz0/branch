using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class GameMedia
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public Media IDMedia { get; set; }

        [Required]
        public TypeMedia IDTypeMedia { get; set; }

        [Required]
        public Game IDGame { get; set; }
    }
}