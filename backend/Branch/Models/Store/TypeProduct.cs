using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class TypeProduct
    {
        [Key][Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}