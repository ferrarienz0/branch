using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class GameMedia
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Media IDMedia { get; set; }

        [Required]
        public virtual TypeMedia IDTypeMedia { get; set; }

        [Required]
        public virtual Game IDGame { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}