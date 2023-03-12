using ArticleStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ArticleStore.Services
{
    public class FetchDataService : IDisposable
    {
        private readonly Timer _timer;
        private readonly HttpClient _client;
        private readonly IArticleService _articleService;
        private readonly ILogger<FetchDataService> _logger;

        public FetchDataService(IArticleService articleService, ILogger<FetchDataService> logger)
        {
            _articleService = articleService;
            _logger = logger;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            _timer = new Timer(async state => await FetchDataAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(300));
        }

        public void Dispose()
        {
            _timer.Dispose();
            _client.Dispose();
        }

        public async Task FetchDataAsync()
        {
            var uri = new Uri("https://christ-coding-challenge.test.pub.k8s.christ.de/Article/GetArticles");

            var json = await _client.GetStringAsync(uri);
            if (json != null && TryDeserializeArticles(json, out var articles))
            {
                var aggregatedArticles = AggregationService.AggregateArticles(articles);
                await UpdateArticlesAsync(aggregatedArticles);
            }
        }

        private bool TryDeserializeArticles(string json, out Article[] articles)
        {
            try
            {
                articles = JsonConvert.DeserializeObject<Article[]>(json);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while deserialize the json.");
                articles = Array.Empty<Article>();
                return false;
            }
        }

        private async Task UpdateArticlesAsync(IEnumerable<AggregatedArticle> articles)
        {
            if (!articles.Any())
                return;

            foreach (var article in articles)
            {
                var articleWasUpdated = await _articleService.TryUpdateArticleAsync(article);
                if (articleWasUpdated)
                    continue;

                await _articleService.TryCreateArticleAsync(article);
            }
        }
    }
}
