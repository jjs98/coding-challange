using ArticleStore.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleStore.Services
{
    public class ArticlesService : IArticleService
    {
        private readonly ApplicationDbContext _dbContext;

        public ArticlesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<AggregatedArticle> GetArticles()
        {
            _dbContext.Database.EnsureCreated();
            return _dbContext.Articles;
        }

        public async Task UpdateArticlesAsync(IEnumerable<AggregatedArticle> articles)
        {
            if (!articles.Any())
            {
                return;
            }

            _dbContext.Database.EnsureCreated();
            foreach (var article in articles)
            {
                var currentArticle = _dbContext.Articles.FirstOrDefault(x => x.ArticleId == article.ArticleId);
                if (currentArticle != null)
                {
                    if (currentArticle == article)
                    {
                        continue;
                    }
                    else
                    {
                        currentArticle = article;
                    }
                }
                else
                {
                    await _dbContext.AddAsync(article);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
