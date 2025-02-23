using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueObject;

namespace Domain.Repositories
{
    public interface IConstructionProjectRepository : IRepository<ConstructionProject>
    {
        Task<IEnumerable<ConstructionProject>> GetAllElasticAsync(int pageNumber, int pageSize, string searchString);
        Task<int> GetTotalElasticAsync(string searchString);
        Task<ConstructionProject?> GetByProjectIdElastic(int projectId);
        Task<ConstructionProject?> GetDetailAsync(int id);
        Task<ConstructionProject> AddAsyncAndElastic(ConstructionProject entity);
        Task UpdateAsyncAndElastic(ConstructionProject entity);
        Task DeleteAsyncAndElastic(int id);
    }
}
