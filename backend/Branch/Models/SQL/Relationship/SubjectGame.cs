using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Branch.Models
{
    public class SubjectGame
    {
        public int Id { get; set; }

        [Required]
        public int Affinity { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}