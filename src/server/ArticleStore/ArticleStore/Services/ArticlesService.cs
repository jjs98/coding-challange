using ArticleStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleStore.Services
{
    public class ArticlesService : IArticleService
    {
        private readonly IApplicationDbService _applicationDbService;

        public ArticlesService(IApplicationDbService applicationDbService)
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

        public async Task<bool> CreateArticleAsync(AggregatedArticle article)
        {
            return await _applicationDbService.CreateArticleAsync(article);
        }

        public async Task<bool> DeleteArticleAsync(string articleId)
        {
            return await _applicationDbService.DeleteArticleAsync(articleId);
        }

        public async Task UpdateArticlesAsync(IEnumerable<AggregatedArticle> articles)
        {
            if (!articles.Any())
            {
                return;
            }

            foreach (var article in articles)
            {
                var couldBeUpdated = await UpdateArticleAsync(article);
                if (!couldBeUpdated)
                {
                    await CreateArticleAsync(article);
                }
            }
        }

        public async Task<bool> UpdateArticleAsync(AggregatedArticle article)
        {
            return await _applicationDbService.UpdateArticleAsync(article);
        }
    }
}
