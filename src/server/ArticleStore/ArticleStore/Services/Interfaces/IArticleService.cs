﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleStore.Services.Interfaces
{
    public interface IArticleService
    {
        public Task<bool> CreateArticleAsync(AggregatedArticle article);
        public Task<bool> DeleteArticleAsync(string articleId);
        public IEnumerable<AggregatedArticle> GetArticles();
        public AggregatedArticle GetArticle(string articleId);
        public Task<bool> UpdateArticleAsync(AggregatedArticle article);
        public Task UpdateArticlesAsync(IEnumerable<AggregatedArticle> articles);
    }
}
