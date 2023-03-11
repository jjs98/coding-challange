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
                Brand = GetAttribute("MRK", article),
                Material = GetAttribute("MAT", article),
                SecondMaterial = GetAttribute("MAT2", article),
                ThirdMaterial = GetAttribute("MAT3", article),
                Alloy = GetAttribute("LEG", article),
                SecondAlloy = GetAttribute("LEG2", article),
                ThirdAlloy = GetAttribute("LEG3", article),
                Collection = GetAttribute("KOLL", article),
                ProductGroup = GetAttribute("WRG_2", article),
                MainProductGroup = GetAttribute("WHG_2", article),
                Target = GetAttribute("ZIEL", article)
            };
        }

        private static string GetAttribute(string key, Article article)
        {
            return article.Attributes.FirstOrDefault(x => x.Key == key && x.Language == "de")?.Value;
        }
    }
}
