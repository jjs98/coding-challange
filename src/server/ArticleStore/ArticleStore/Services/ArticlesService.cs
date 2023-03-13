using ArticleStore.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleStore.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IApplicationDbService _applicationDbService;

        public ArticleService(IApplicationDbService applicationDbService)
        {
            _applicationDbService = applicationDbService;
        }

        public IEnumerable<AggregatedArticle> GetArticles()
        {
            return _applicationDbService.GetArticles();
        }
        public AggregatedArticle GetArticle(string articleId)
        {
            return _applicationDbService.GetArticle(articleId);
        }

        public async Task<bool> TryCreateArticleAsync(AggregatedArticle article)
        {
            return await _applicationDbService.TryCreateArticleAsync(article);
        }

        public async Task<bool> TryDeleteArticleAsync(string articleId)
        {
            return await _applicationDbService.TryDeleteArticleAsync(articleId);
        }

        public async Task<bool> TryUpdateArticleAsync(AggregatedArticle article)
        {
            return await _applicationDbService.TryUpdateArticleAsync(article);
        }

        public async Task UpdateOrCreateArticleAsync(AggregatedArticle article)
        {
            await _applicationDbService.UpdateOrCreateArticleAsync(article);
        }

        public static bool TryDeserializeArticles(string json, out Article[] articles)
        {
            try
            {
                articles = JsonConvert.DeserializeObject<Article[]>(json);
                return true;
            }
            catch
            {
                articles = Array.Empty<Article>();
                return false;
            }
        }
    }
}
