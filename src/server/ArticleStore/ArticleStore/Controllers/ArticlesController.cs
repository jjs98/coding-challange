using ArticleStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArticleStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IArticleService _articleService;

        public ArticlesController(ILogger<ArticlesController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        [HttpPost]
        public void CreateArticles()
        {
            _articleService.UpdateArticlesAsync(new[] { new AggregatedArticle() { ArticleId = "1" } });
        }

        [HttpGet]
        public IEnumerable<AggregatedArticle> GetArticles()
        {
            return _articleService.GetArticles();
        }
    }
}