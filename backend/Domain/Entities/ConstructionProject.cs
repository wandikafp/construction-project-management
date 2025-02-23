using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObject;

namespace Domain.Entities
{
    public class ConstructionProject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Allow auto-increment
        public int? ProjectId { get; set; }

        [Required]
        [MaxLength(200)]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string ProjectLocation { get; set; } = string.Empty;

        [Required]
        public string Stage { get; set; }

        [Required]
        public string Category { get; set; }

        [MaxLength(100)]
        public string? OtherCategory { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [MaxLength(2000)]
        public string? ProjectDetails { get; set; }

        [Required]
        public Guid CreatorId { get; set; }
        public virtual User Creator { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
