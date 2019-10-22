using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Branch.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual User User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}