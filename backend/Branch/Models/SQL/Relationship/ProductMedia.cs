using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class ProductMedia
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public Media IDMedia { get; set; }

        [Required]
        public TypeMedia IDTypeMedia { get; set; }

        [Required]
        public Product IDProduct { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}