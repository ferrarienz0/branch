using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Hashtag { get; set; }

        [ForeignKey("Media")]
        public int? MediaId { get; set; }

        public virtual Media Media { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}