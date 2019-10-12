using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class UserMedia
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public Media IDMedia { get; set; }

        [Required]
        public TypeMedia IDTypeMedia { get; set; }

        [Required]
        public User IDUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}