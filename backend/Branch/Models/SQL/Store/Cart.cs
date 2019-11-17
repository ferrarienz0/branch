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
        public int Id { get; set; }

        [Required]
        public bool Finished { get; set; }

        [Required]
        public float Total { get; set; }

        [ForeignKey("Pro")]
        public int ProId { get; set; }

        public virtual User Pro { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}