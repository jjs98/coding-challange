using ArticleStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ArticleStore.Services
{
    public class UpdateService : IUpdateService, IDisposable
    {
        private readonly Timer _timer;
        private readonly HttpClient _client;
        private readonly ILogger<UpdateService> _logger;
        public Article[] Articles { get; set; }

        public UpdateService(ILogger<UpdateService> logger)
        {
            _logger = logger;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            Articles = Array.Empty<Article>();

            _timer = new Timer(async state => await FetchData(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        }

        public void Dispose()
        {
            _timer.Dispose();
            _client.Dispose();
        }

        private async Task FetchData()
        {
            var uri = new Uri("https://christ-coding-challenge.test.pub.k8s.christ.de/Article/GetArticles");
            try
            {
                var json = await _client.GetStringAsync(uri);
                if (json != null)
                {
                    if (TryDeserialize(json, out var articles))
                    {
                        Articles = articles;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Fetching articles from Christ failed.");
            }
        }

        private static bool TryDeserialize(string json, out Article[] articles)
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
