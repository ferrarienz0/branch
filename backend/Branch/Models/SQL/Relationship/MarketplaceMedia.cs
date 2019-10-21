using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class MarketplaceMedia
    {
        public int Id { get; set; }

        [Required]
        public virtual Media Media { get; set; }

        [Required]
        public virtual TypeMedia TypeMedia { get; set; }

        [Required]
        public virtual Marketplace Marketplace { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}