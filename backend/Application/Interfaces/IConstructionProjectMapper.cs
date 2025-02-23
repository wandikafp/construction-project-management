using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Request;
using Application.Dtos.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IConstructionProjectMapper
    {
        ConstructionProject MapToRequest(ConstructionProjectRequest project);
        ConstructionProjectResponse MapToResponse(ConstructionProject project);
        ConstructionProjectDetailResponse MapToDetailResponse(ConstructionProject project);
    }
}
