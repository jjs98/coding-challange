using System;
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
                Material = GetAttribute("MAT", "de", article),
                SecondMaterial = GetAttribute("MAT2", "de", article),
                ThirdMaterial = GetAttribute("MAT3", "de", article),
                Alloy = GetAttribute("LEG", "de", article),
                SecondAlloy = GetAttribute("LEG2", "de", article),
                ThirdAlloy = GetAttribute("LEG3", "de", article),
                Collection = GetAttribute("KOLL", article),
                ProductGroup = GetAttribute("WRG_2", article),
                MainProductGroup = GetAttribute("WHG_2", article),
                Target = GetAttribute("ZIEL", "de", article)
            };
        }

        private static string GetAttribute(string key, Article article) => GetAttribute(key, null, article);
        private static string GetAttribute(string key, string language, Article article)
        {
            if(language is null)
                return article.Attributes.FirstOrDefault(x => x.Key == key)?.Value;

            return article.Attributes.FirstOrDefault(x => x.Key == key && x.Language == language)?.Value;
        }
    }
}
