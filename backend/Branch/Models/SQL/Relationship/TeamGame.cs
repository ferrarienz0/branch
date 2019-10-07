using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class TeamGame
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public Game IDGame { get; set; }

        [Required]
        public Team IDTeam { get; set; }
    }
}