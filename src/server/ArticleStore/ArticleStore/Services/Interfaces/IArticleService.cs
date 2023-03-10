using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleStore.Services.Interfaces
{
    public interface IArticleService
    {
        public IEnumerable<AggregatedArticle> GetArticles();
        public Task UpdateArticlesAsync(IEnumerable<AggregatedArticle> articles);
    }
}
