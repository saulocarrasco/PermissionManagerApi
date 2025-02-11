
using Elastic.Clients.Elasticsearch;
using PermissionsManager.Application.Contracts;
using PermissionsManager.Domain.Contracts;

namespace PermissionsManager.Infrastructure.ElasticSearch
{
    public class ElasticSearchService(ElasticsearchClient elasticSearchClient) : IElasticSearchService
    {
        public async Task<bool> Index<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var response = await elasticSearchClient.IndexAsync(document: entity, index: typeof(TEntity).Name.ToLower());
            return response.IsValidResponse && response.IsSuccess();
        }

        public async Task<bool> Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var response = await elasticSearchClient.UpdateAsync<TEntity, TEntity>(typeof(TEntity).Name.ToLower(), entity.Id, u => u
            .Doc(entity));
            return response.IsValidResponse && response.IsSuccess();
        }

        public async Task Search<TEntity>(string term)
        {
            var response = await elasticSearchClient.SearchAsync<TEntity>(s => s
                .Index(typeof(TEntity).Name.ToLower())
                .Query(q => q
                .MultiMatch(mm => mm
                    .Query(term)
                    .Fields(Fields.FromString("*text")
                    ))
                ));

            if (response.IsValidResponse)
            {
                var tweet = response.Documents.FirstOrDefault();
            }
        }
    }
}
