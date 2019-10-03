using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class ProductCart
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public Product IDProduct { get; set; }

        [Required]
        public Cart IDCart { get; set; }
    }
}