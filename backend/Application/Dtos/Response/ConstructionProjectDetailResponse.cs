using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos.Response
{
    public class ConstructionProjectDetailResponse
    {
        public int? ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public string ProjectLocation { get; set; } = string.Empty;

        public string Stage { get; set; }

        public string Category { get; set; }

        public string? OtherCategory { get; set; }

        public DateOnly StartDate { get; set; }

        public string? ProjectDetails { get; set; }

        public Guid CreatorId { get; set; }

        public virtual UserResponse Creator { get; set; }

    }
}
