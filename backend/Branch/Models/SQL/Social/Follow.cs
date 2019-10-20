using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Branch.Models
{
    public class Follow
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Affinity { get; set; }

        [Required]
        public virtual User IDFollower { get; set; }

        [Required]
        public virtual User IDFollowed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}