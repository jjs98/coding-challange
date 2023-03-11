using ArticleStore.Services.Interfaces;
using Newtonsoft.Json;
using System;
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


        public FetchDataService(IArticleService articleService)
        {
            _articleService = articleService;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            _timer = new Timer(async state => await FetchDataAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
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
                await _articleService.UpdateArticlesAsync(aggregatedArticles);
            }
        }

        private static bool TryDeserializeArticles(string json, out Article[] articles)
        {
            try
            {
                articles = JsonConvert.DeserializeObject<Article[]>(json);
                return true;
            }
            catch
            {
                articles = Array.Empty<Article>();
                return false;
            }
        }
    }
}
