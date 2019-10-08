using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Branch.Models
{
    public class Follow
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public int Affinity { get; set; }

        [Required]
        public User IDFollower { get; set; }

        [Required]
        public User IDFollowed { get; set; }
    }
}