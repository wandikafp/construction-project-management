using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Request;
using Application.Dtos.Response;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IConstructionProjectService
    {
        Task<PaginatedList<ConstructionProjectResponse>> GetAllProjectsAsync(int pageNumber, int pageSize, string query);
        Task<ConstructionProjectDetailResponse?> GetProjectByIdAsync(int id);
        Task<ConstructionProjectDetailResponse> CreateProjectAsync(ConstructionProjectRequest project);
        Task UpdateProjectAsync(ConstructionProjectRequest project);
        Task DeleteProjectAsync(int id);
    }
}
