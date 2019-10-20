using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public bool Finished { get; set; }

        [Required]
        public float Total { get; set; }

        [Required]
        public Marketplace IDMarketplace { get; set; }

        [Required]
        public virtual User IDUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}