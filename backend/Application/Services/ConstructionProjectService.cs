using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Request;
using Application.Dtos.Response;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class ConstructionProjectService : IConstructionProjectService
    {
        private readonly IConstructionProjectRepository _repository;
        private readonly IConstructionProjectMapper _mapper;

        public ConstructionProjectService(IConstructionProjectRepository repository, IConstructionProjectMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ConstructionProjectResponse>> GetAllProjectsAsync(int pageNumber, int pageSize, string query)
        {
            var projects = await _repository.GetAllElasticAsync(pageNumber, pageSize, query);
            var totalProjects = await _repository.GetTotalElasticAsync(query);
            return new PaginatedList<ConstructionProjectResponse>(projects.Select(p => _mapper.MapToResponse(p)).ToList(), totalProjects, pageNumber, pageSize);
        }

        public async Task<ConstructionProjectDetailResponse?> GetProjectByIdAsync(int id)
        {
            var project = await _repository.GetDetailAsync(id);
            if (project == null)
            {
                return null;
            }
            return _mapper.MapToDetailResponse(project);
        }

        public async Task<ConstructionProjectDetailResponse> CreateProjectAsync(ConstructionProjectRequest project)
            => _mapper.MapToDetailResponse(await _repository.AddAsyncAndElastic(_mapper.MapToRequest(project)));

        public async Task UpdateProjectAsync(ConstructionProjectRequest project)
            => await _repository.UpdateAsyncAndElastic(_mapper.MapToRequest(project));

        public async Task DeleteProjectAsync(int id)
            => await _repository.DeleteAsyncAndElastic(id);
    }
}
