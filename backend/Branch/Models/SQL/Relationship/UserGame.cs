using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Branch.Models
{
    public class UserGame
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Affinity { get; set; }

        [Required]
        public User IDUser { get; set; }

        [Required]
        public Game IDGame { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}