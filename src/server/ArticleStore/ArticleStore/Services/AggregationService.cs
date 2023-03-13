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
                Material = AggregateAttribute("MAT", article.Attributes),
                SecondMaterial = AggregateAttribute("MAT2", article.Attributes),
                ThirdMaterial = AggregateAttribute("MAT3", article.Attributes),
                Alloy = AggregateAttribute("LEG", article.Attributes),
                SecondAlloy = AggregateAttribute("LEG2", article.Attributes),
                ThirdAlloy = AggregateAttribute("LEG3", article.Attributes),
                Collection = GetAttribute("KOLL", article),
                ProductGroup = GetAttribute("WRG_2", article),
                MainProductGroup = GetAttribute("WHG_2", article),
                Target= AggregateAttribute("ZIEL", article.Attributes)
            };
        }

        internal static string GetAttribute(string key, Article article)
        {
            return article.Attributes.FirstOrDefault(x => x.Key == key)?.Value;
        }

        internal static IEnumerable<AggregatedAttribute> AggregateAttribute(string key, IEnumerable<Attribute> attributes)
        {
            var attributesForKey = attributes.Where(x => x.Key == key);
            foreach (var attribute in attributesForKey)
            {
                yield return new AggregatedAttribute()
                {
                    Language =  attribute.Language,
                    Value = attribute.Value,
                };
            }
        }
    }
}
