using ArticleStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("Count")]
        public ActionResult<IEnumerable<int>> GetArticlesCount()
        {
            return Ok(_articleService.GetArticles().Count());
        }

        [HttpGet]
        public ActionResult<IEnumerable<AggregatedArticle>> GetArticles()
        {
            return Ok(_articleService.GetArticles());
        }

        [HttpGet("{articleId:int}")]
        public ActionResult<AggregatedArticle> GetArticle(string articleId)
        {
            return Ok(_articleService.GetArticle(articleId));
        }

        [HttpPut]
        public async Task<ActionResult> CreateArticle([FromBody] AggregatedArticle article)
        {
            await _articleService.CreateArticleAsync(article);
            return Ok();
        }

        [HttpDelete("{articleId:int}")]
        public async Task<ActionResult> DeleteArticle(string articleId)
        {
            await _articleService.DeleteArticleAsync(articleId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateArticle([FromBody] AggregatedArticle article)
        {
            await _articleService.UpdateArticleAsync(article);
            return Ok();
        }
    }
}