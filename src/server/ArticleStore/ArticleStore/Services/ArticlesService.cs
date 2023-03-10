using ArticleStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArticleStore.Services
{
    public class ArticlesService : IArticleService
    {
        private readonly IUpdateService _updateService;

        public ArticlesService(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        public IEnumerable<AggregatedArticle> GetArticles()
        {
            if (_updateService.Articles.Any())
            {
                foreach (var article in _updateService.Articles)
                {
                    yield return new AggregatedArticle()
                    {
                        ArticleId = article.ArticleId,
                        Brand = article.Attributes.FirstOrDefault(x => x.Key == "MRK")?.Value,
                        Material = article.Attributes.FirstOrDefault(x => x.Key == "MAT")?.Value,
                        SecondMaterial = article.Attributes.FirstOrDefault(x => x.Key == "MAT2")?.Value,
                        ThirdMaterial = article.Attributes.FirstOrDefault(x => x.Key == "MA3")?.Value,
                        Alloy = article.Attributes.FirstOrDefault(x => x.Key == "LEG")?.Value,
                        SecondAlloy = article.Attributes.FirstOrDefault(x => x.Key == "LEG2")?.Value,
                        ThirdAlloy= article.Attributes.FirstOrDefault(x => x.Key == "LEG3")?.Value,
                        Collection = article.Attributes.FirstOrDefault(x => x.Key == "KOLL")?.Value,
                        ProductGroup = article.Attributes.FirstOrDefault(x => x.Key == "WRG_2")?.Value,
                        MainProductGroup = article.Attributes.FirstOrDefault(x => x.Key == "WHG_2")?.Value,
                        Target = article.Attributes.FirstOrDefault(x => x.Key == "ZIEL")?.Value
                    };
                }
            }
            yield return new AggregatedArticle();
        }
    }
}
