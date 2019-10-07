using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Branch.Models
{
    public class Category
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}