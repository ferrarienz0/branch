using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public float CurrentDiscount { get; set; }

        [Required]
        public float MaxDiscount { get; set; }

        
        [ForeignKey("TypeProduct")]
        public int TypeProductId { get; set; }

        public virtual TypeProduct TypeProduct { get; set; }

        [ForeignKey("Marketplace")]
        public int MarketplaceId { get; set; }

        public virtual Marketplace Marketplace { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}