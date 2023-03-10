using System.Collections.Generic;
using System.Linq;

namespace ArticleStore.Services
{
    public static class AggregationService
    {
        public static IEnumerable<AggregatedArticle> AggregateArticles(IEnumerable<Article> articles)
        {
            if (!articles.Any())
            {
                yield return new AggregatedArticle();
            }

            foreach (var article in articles)
            {
                yield return AggregateArticle(article);
            }
        }

        public static AggregatedArticle AggregateArticle(Article article)
        {
            return new AggregatedArticle()
            {
                ArticleId = article.ArticleId,
                Brand = article.Attributes.FirstOrDefault(x => x.Key == "MRK")?.Value,
                Material = article.Attributes.FirstOrDefault(x => x.Key == "MAT")?.Value,
                SecondMaterial = article.Attributes.FirstOrDefault(x => x.Key == "MAT2")?.Value,
                ThirdMaterial = article.Attributes.FirstOrDefault(x => x.Key == "MA3")?.Value,
                Alloy = article.Attributes.FirstOrDefault(x => x.Key == "LEG")?.Value,
                SecondAlloy = article.Attributes.FirstOrDefault(x => x.Key == "LEG2")?.Value,
                ThirdAlloy = article.Attributes.FirstOrDefault(x => x.Key == "LEG3")?.Value,
                Collection = article.Attributes.FirstOrDefault(x => x.Key == "KOLL")?.Value,
                ProductGroup = article.Attributes.FirstOrDefault(x => x.Key == "WRG_2")?.Value,
                MainProductGroup = article.Attributes.FirstOrDefault(x => x.Key == "WHG_2")?.Value,
                Target = article.Attributes.FirstOrDefault(x => x.Key == "ZIEL")?.Value
            };
        }
    }
}
