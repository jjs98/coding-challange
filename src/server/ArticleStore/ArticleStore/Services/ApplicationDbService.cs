using ArticleStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleStore.Services
{
    public class ApplicationDbService : IApplicationDbService, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ApplicationDbService> _logger;

        public ApplicationDbService(ApplicationDbContext dbContext, ILogger<ApplicationDbService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> TryCreateArticleAsync(AggregatedArticle article)
        {
            var currentArticle = GetArticle(article.ArticleId);
            if (currentArticle is not null)
            {
                _logger.LogError($"Article with Id {article.ArticleId} already exists.");
                return false;
            }
            await CreateArticle(article);
            return true;
        }

        public async Task<bool> TryDeleteArticleAsync(string articleId)
        {
            var currentArticle = GetArticle(articleId);
            if (currentArticle is null)
            {
                _logger.LogError($"Article with Id {currentArticle.ArticleId} does not exist.");
                return false;
            }
            _dbContext.Articles.Remove(currentArticle);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public IEnumerable<AggregatedArticle> GetArticles()
        {
            return _dbContext.Articles;
        }

        public AggregatedArticle GetArticle(string articleId)
        {
            return _dbContext.Articles.FirstOrDefault(x => x.ArticleId == articleId);
        }

        public async Task<bool> TryUpdateArticleAsync(AggregatedArticle article)
        {
            var currentArticle = GetArticle(article.ArticleId);
            if (currentArticle is null)
            {
                _logger.LogError($"Article with Id {article.ArticleId} does not exist.");
                return false;
            }

            await UpdateArticle(currentArticle, article);
            return true;
        }

        public async Task UpdateOrCreateArticleAsync(AggregatedArticle article)
        {
            var currentArticle = GetArticle(article.ArticleId);
            if (currentArticle is not null)
            {
                await UpdateArticle(currentArticle, article);
            }
            await CreateArticle(article);
        }

        private async Task UpdateArticle(AggregatedArticle currentArticle, AggregatedArticle article)
        {
            currentArticle.Brand = article.Brand;
            currentArticle.Material = article.Material;
            currentArticle.SecondMaterial = article.SecondMaterial;
            currentArticle.ThirdMaterial = article.ThirdMaterial;
            currentArticle.Alloy = article.Alloy;
            currentArticle.SecondAlloy = article.SecondAlloy;
            currentArticle.ThirdAlloy = article.ThirdAlloy;
            currentArticle.ProductGroup = article.ProductGroup;
            currentArticle.MainProductGroup = article.MainProductGroup;
            currentArticle.Collection = article.Collection;
            currentArticle.Target = article.Target;

            await _dbContext.SaveChangesAsync();
        }

        private async Task CreateArticle(AggregatedArticle article)
        {
            await _dbContext.Articles.AddAsync(article);
            await _dbContext.SaveChangesAsync();
        }
    }
}
