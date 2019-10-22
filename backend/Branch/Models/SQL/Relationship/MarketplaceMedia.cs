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

        [ForeignKey("Media")]
        public int MediaId { get; set; }

        public virtual Media Media { get; set; }

        [ForeignKey("TypeMedia")]
        public int TypeMediaId { get; set; }

        public virtual TypeMedia TypeMedia { get; set; }

        [ForeignKey("Marketplace")]
        public int MarketplaceId { get; set; }

        public virtual Marketplace Marketplace { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}