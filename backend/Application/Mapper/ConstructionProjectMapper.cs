using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Mapper
{
    public class ConstructionProjectMapper : IConstructionProjectMapper
    {
        public ConstructionProject MapToRequest(ConstructionProjectRequest project)
        {
            return new ConstructionProject
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectLocation = project.ProjectLocation,
                Stage = project.Stage,
                Category = project.Category,
                StartDate = project.StartDate,
                OtherCategory = project.OtherCategory,
                ProjectDetails = project.ProjectDetails,
                CreatorId = project.CreatorId,
            };
        }

        public ConstructionProjectDetailResponse MapToDetailResponse(ConstructionProject project)
        {
            return new ConstructionProjectDetailResponse
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectLocation = project.ProjectLocation,
                Stage = project.Stage,
                Category = project.Category,
                StartDate = project.StartDate,
                OtherCategory = project.OtherCategory,
                ProjectDetails = project.ProjectDetails,
                CreatorId = project.CreatorId,
                Creator = project.Creator != null ? MapToUserResponse(project.Creator): null,
            };
        }

        public ConstructionProjectResponse MapToResponse(ConstructionProject project)
        {
            return new ConstructionProjectResponse
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                ProjectLocation = project.ProjectLocation,
                Stage = project.Stage,
                Category = project.Category,
                StartDate = project.StartDate,
            };
        }

        public UserResponse MapToUserResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
            };
        }
    }
}
