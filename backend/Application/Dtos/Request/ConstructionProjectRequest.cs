using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos.Request
{
    public class ConstructionProjectRequest
    {
        public int? ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectLocation { get; set; }

        public string Stage { get; set; }

        public string Category { get; set; }

        public string? OtherCategory { get; set; }

        public DateOnly StartDate { get; set; }

        public string ProjectDetails { get; set; }

        public Guid CreatorId { get; set; }

        public bool IsValid(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (Stage == "Concept" || Stage == "Design" || Stage == "PreConstruction")
            {
                if (StartDate <= DateOnly.FromDateTime(DateTime.Now))
                {
                    errorMessage = "StartDate should be a future date for stages Concept, Design, or PreConstruction.";
                    return false;
                }
            }

            return true;
        }

    }
}
