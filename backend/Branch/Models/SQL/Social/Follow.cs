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
        public int Id { get; set; }

        [Required]
        public int Affinity { get; set; }

        [ForeignKey("Follower")]
        public int FollowerId { get; set; }
        
        public virtual User Follower { get; set; }

        [ForeignKey("Followed")]
        public int FollowedId;

        public virtual User Followed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}