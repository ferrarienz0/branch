using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class Subject
    {
        [Key]
        [Required]
        public int ID { get; set; }

        [Required]
        public string Hashtag { get; set; }
    }
}