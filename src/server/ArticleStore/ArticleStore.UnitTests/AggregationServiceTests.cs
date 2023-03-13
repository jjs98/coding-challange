using ArticleStore.Services;
using ArticleStore.UnitTests.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ArticleStore.UnitTests
{
    [TestClass]
    public class AggregationServiceTests
    {
        [TestMethod]
        public void GetAttribute_Success()
        {
            var key = "MRK";
            var value = "ESPRIT";
            var articles = new ArticleBuilder()
                .WithAttribute(key, value)
                .WithAttribute("ZIEL", "Damen", "de")
                .AddArticle()
                .Build();

            var actualValue = AggregationService.GetAttribute(key, articles[0]);
            Assert.IsNotNull(actualValue, "Attribute was not found.");
            Assert.AreEqual(value, actualValue, "Found attribute is not correct.");
        }

        [TestMethod]
        public void GetAttribute_Fail()
        {
            var key = "MRK";
            var articles = new ArticleBuilder()
                .WithAttribute("ZIEL", "Damen", "de")
                .AddArticle()
                .Build();

            var actualValue = AggregationService.GetAttribute(key, articles[0]);
            Assert.IsNull(actualValue, "Attribute was found unexpected.");
        }

        [TestMethod]
        public void AggregateAttribute_Success()
        {
            var key = "MAT";
            var value = "silver";
            var language = "en";
            var secondValue = "silber";
            var secondLanguage = "de";
            var articles = new ArticleBuilder()
                .WithAttribute(key, value, language)
                .WithAttribute(key, secondValue, secondLanguage)
                .WithAttribute("ZIEL", "Damen", "de")
                .AddArticle()
                .Build();
            var attributes = new AggregatedAttributeBuilder()
                .WithAttribute(value, language)
                .WithAttribute(secondValue, secondLanguage)
                .Build();

            var actualAttributes = AggregationService.AggregateAttribute(key, articles[0].Attributes);
            AssertAggregatedAttributes(attributes, actualAttributes);
        }

        [TestMethod]
        public void AggregateAttribute_Fail()
        {
            var key = "MAT";
            var articles = new ArticleBuilder()
                .WithAttribute("ZIEL", "Damen", "de")
                .AddArticle()
                .Build();

            var actualAttributes = AggregationService.AggregateAttribute(key, articles[0].Attributes);
            Assert.IsFalse(actualAttributes.Any(), "Attributes found unexpected.");
        }

        [DataTestMethod]
        [DataRow("1", "MRK", "ESPRIT", null)]
        [DataRow("2", "KOLL", "Winter-Kollektion", null)]
        [DataRow("3", "WRG_2", "Uhr mit Lederband", null)]
        [DataRow("4", "WHG_2", "Damenuhren", null)]
        [DataRow("5", "MAT", "Silver", "en")]
        [DataRow("6", "MAT2", "Titanium", "en")]
        [DataRow("7", "MAT3", "Steel", "en")]
        [DataRow("8", "LEG", "925", "en")]
        [DataRow("9", "LEG2", "858", "en")]
        [DataRow("10", "LEG3", "325", "en")]
        [DataRow("11", "ZIEL", "Woman", "en")]
        public void AggregateArticle_Success(string articleId, string attributeKey, string attributeValue, string attributeLanguage)
        {
            var articles = CreateTestArticle("1", "MRK", "ESPRIT", null);
            var actualArticles = AggregationService.AggregateArticles(articles);
            AssertArticle(articles[0], actualArticles.ElementAt(0));
        }

        private static void AssertAggregatedAttributes(List<AggregatedAttribute> attributes, IEnumerable<AggregatedAttribute> actualAttributes)
        {
            Assert.IsTrue(actualAttributes.Any(), "Attributes should not be empty.");
            Assert.AreEqual(attributes.Count, actualAttributes.Count(), "Not all attribute are aggregated.");
            for (int i = 0; i < attributes.Count; i++)
            {
                Assert.AreEqual(attributes[i].Value, actualAttributes.ElementAt(i).Value);
                Assert.AreEqual(attributes[i].Language, actualAttributes.ElementAt(i).Language);
            }
        }

        private static List<Article> CreateTestArticle(string articleId, string attributeKey, string attributeValue, string attributeLanguage)
        {
            return new ArticleBuilder()
            .WithArticleId(articleId)
                .WithAttribute(attributeKey, attributeValue, attributeLanguage)
                .AddArticle()
                .Build();
        }

        private static void AssertArticle(Article article, AggregatedArticle actualArticle)
        {
            Assert.AreEqual(article.ArticleId, actualArticle.ArticleId);
            AssertAttributes(article.Attributes, actualArticle);
        }

        private static void AssertAttributes(IEnumerable<Attribute> attributes, AggregatedArticle actualArticle)
        {
            foreach (var attribute in attributes)
            {
                switch (attribute.Key)
                {
                    case "MRK":
                        Assert.AreEqual(attribute.Value, actualArticle.Brand);
                        break;
                    case "KOLL":
                        Assert.AreEqual(attribute.Value, actualArticle.Collection);
                        break;
                    case "WRG_2":
                        Assert.AreEqual(attribute.Value, actualArticle.ProductGroup);
                        break;
                    case "WHG_2":
                        Assert.AreEqual(attribute.Value, actualArticle.MainProductGroup);
                        break;
                    case "MAT":
                        AssertAttribute(attribute, actualArticle.Material);
                        break;
                    case "MAT2":
                        AssertAttribute(attribute, actualArticle.SecondMaterial);
                        break;
                    case "MAT3":
                        AssertAttribute(attribute, actualArticle.ThirdMaterial);
                        break;
                    case "LEG":
                        AssertAttribute(attribute, actualArticle.Alloy);
                        break;
                    case "LEG2":
                        AssertAttribute(attribute, actualArticle.SecondAlloy);
                        break;
                    case "LEG3":
                        AssertAttribute(attribute, actualArticle.ThirdAlloy);
                        break;
                    case "ZIEL":
                        AssertAttribute(attribute, actualArticle.Target);
                        break;
                }
            }
        }

        private static void AssertAttribute(Attribute expectedAttribute, IEnumerable<AggregatedAttribute> actualAttributes)
        {
            var attribute = actualAttributes.FirstOrDefault(x => x.Language == expectedAttribute.Language && x.Value == expectedAttribute.Language);
            Assert.IsNotNull(attribute, "Attribute was not aggregated.");
        }
    }
}