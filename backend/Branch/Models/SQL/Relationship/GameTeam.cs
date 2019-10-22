using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Branch.Models
{
    public class GameTeam
    {
        public int Id { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }
    }
}