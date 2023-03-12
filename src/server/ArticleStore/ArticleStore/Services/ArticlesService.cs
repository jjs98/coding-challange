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
    }
}
