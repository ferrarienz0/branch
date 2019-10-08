using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class Pro
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required][Index(IsUnique = true)]
        public string EmailContact { get; set; }

        [Required]
        public User IDUser { get; set; }

        [Required]
        public Team IDTeam { get; set; }
    }
}