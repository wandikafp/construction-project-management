using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Dtos.Response
{
    public class ConstructionProjectResponse
    {
        public int? ProjectId { get; set; }

        public string ProjectName { get; set; } = string.Empty;

        public string ProjectLocation { get; set; } = string.Empty;

        public string Stage { get; set; }

        public string Category { get; set; }
        public DateOnly StartDate { get; set; }

    }
}
