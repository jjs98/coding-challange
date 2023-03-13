using ArticleStore.Services;
using ArticleStore.UnitTests.Builder;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ArticleStore.UnitTests
{
    [TestClass]
    public class ArticleServiceTests
    {
        private string _filePath;

        [TestInitialize]
        public void Setup()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "exampleArticle.json");
        }

        [TestMethod]
        public void GetAttribute_Success()
        {
            var articlesAsJson = File.ReadAllText(_filePath);

            var deserializeSucceed = ArticleService.TryDeserializeArticles(articlesAsJson, out var actualArticles);
            Assert.IsTrue(deserializeSucceed, "Deserialization did not succeed.");
            Assert.IsNotNull(actualArticles, "Articles are null unexpected.");
            var article = actualArticles[0];
            Assert.AreEqual("88664035", article.ArticleId);
            AssertAttribute("MRK", "ESPRIT", null, article.Attributes);
            AssertAttribute("WRG_2", "Uhr mit Lederband", null, article.Attributes);
            AssertAttribute("WHG_2", "Damenuhren", null, article.Attributes);
            AssertAttribute("MAT", "Edelstahl", "de", article.Attributes);
            AssertAttribute("ZIEL", "Damen", "de", article.Attributes);
        }

        private static void AssertAttribute(string key, string value, string language, IEnumerable<Attribute> attributes)
        {
            var attribute = attributes.FirstOrDefault(x=>x.Key == key && x.Language == language && x.Value == value);
            Assert.IsNotNull(attribute, "Attribute {0}:{1}:{2} was not deserialized correctly.", key, value, language);
        }
    }
}