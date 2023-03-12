using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleStore.Services.Interfaces
{
    public interface IArticleService
    {
        public Task<bool> TryCreateArticleAsync(AggregatedArticle article);
        public Task<bool> TryDeleteArticleAsync(string articleId);
        public IEnumerable<AggregatedArticle> GetArticles();
        public AggregatedArticle GetArticle(string articleId);
        public Task<bool> TryUpdateArticleAsync(AggregatedArticle article);
    }
}
