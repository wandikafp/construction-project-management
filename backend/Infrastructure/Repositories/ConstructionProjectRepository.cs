using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObject;
using Infrastructure.Data;
using Infrastructure.Message.Producer;
using Infrastructure.ValueObject;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace Infrastructure.Repositories
{
    public class ConstructionProjectRepository : Repository<ConstructionProject>, IConstructionProjectRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ConstructionProject> _dbSet;
        private readonly ElasticClient _elasticClient;
        private readonly KafkaProducer _kafkaProducer;

        public ConstructionProjectRepository(ApplicationDbContext context, ElasticClient elasticClient, KafkaProducer kafkaProducer) : base(context) // Modify constructor
        {
            _context = context;
            _dbSet = context.Set<ConstructionProject>();
            _elasticClient = elasticClient;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<IEnumerable<ConstructionProject>> GetAllElasticAsync(int pageNumber, int pageSize, string searchString)
        {
            var searchResponse = await _elasticClient.SearchAsync<ConstructionProject>(s => s
                .Query(q => q
                    .MultiMatch(m => m
                        .Query(searchString)
                        .Fields(f => f
                            .Field(p => p.ProjectId.ToString())
                            .Field(p => p.ProjectName)
                            .Field(p => p.ProjectLocation)
                            .Field(p => p.Stage)
                            .Field(p => p.Category)
                            .Field(p => p.OtherCategory)
                            .Field(p => p.ProjectDetails)
                        )
                    )
                )
                .From((pageNumber - 1) * pageSize)
                .Size(pageSize)
            );

            return searchResponse.Documents;
        }

        public async Task<int> GetTotalElasticAsync(string searchString)
        {
            var countResponse = await _elasticClient.CountAsync<ConstructionProject>(c => c
                .Query(q => q
                    .MultiMatch(m => m
                        .Query(searchString)
                        .Fields(f => f
                            .Field(p => p.ProjectId.ToString())
                            .Field(p => p.ProjectName)
                            .Field(p => p.ProjectLocation)
                            .Field(p => p.Stage)
                            .Field(p => p.Category)
                            .Field(p => p.OtherCategory)
                            .Field(p => p.ProjectDetails)
                        )
                    )
                )
            );

            return (int)countResponse.Count;
        }

        public async Task<ConstructionProject?> GetByProjectIdElastic(int projectId)
        {
            var searchResponse = _elasticClient.Get<ConstructionProject>(projectId);
            return searchResponse.Source;
        }

        public async Task<ConstructionProject?> GetDetailAsync(int id)
        {
            return await _dbSet.Include(p => p.Creator).FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<ConstructionProject> AddAsyncAndElastic(ConstructionProject entity)
        {
            entity = await this.AddAsync(entity);
            //add to elastic in the background
            Task.Run(async () =>
            {
                var message = new MessageObject
                {
                    action = "create",
                    project = entity
                };
                await _kafkaProducer.ProduceAsync(message, CancellationToken.None);
            });
            //Task.Run(() =>
            //{
            //    _elasticClient.Index<ConstructionProject>(entity, i => i.Id(entity.ProjectId));
            //});
            return entity;
        }

        public async Task UpdateAsyncAndElastic(ConstructionProject entity)
        {
            await this.UpdateAsync(entity);
            // update in elastic in the background
            Task.Run(async () =>
            {
                var message = new MessageObject
                {
                    action = "update",
                    project = entity
                };
                await _kafkaProducer.ProduceAsync(message, CancellationToken.None);
            });
            //Task.Run(() =>
            //{
            //    _elasticClient.Index<ConstructionProject>(entity, i => i.Id(entity.ProjectId));
            //});
        }

        public async Task DeleteAsyncAndElastic(int id)
        {
            await this.DeleteAsync(id);
            Task.Run(async () =>
            {
                var message = new MessageObject
                {
                    action = "delete",
                    id = id
                };
                await _kafkaProducer.ProduceAsync(message, CancellationToken.None);
            });
            // delete from elastic in the background
            //Task.Run(() => _elasticClient.Delete<ConstructionProject>(id));
        }
    }

}
