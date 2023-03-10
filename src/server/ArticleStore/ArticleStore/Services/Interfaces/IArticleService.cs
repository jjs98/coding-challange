using System.Collections.Generic;

namespace ArticleStore.Services.Interfaces
{
    public interface IArticleService
    {
        public IEnumerable<AggregatedArticle> GetArticles();
    }
}
