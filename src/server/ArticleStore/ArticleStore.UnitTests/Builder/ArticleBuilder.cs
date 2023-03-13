using System.Collections.Generic;

namespace ArticleStore.UnitTests.Builder
{
    internal class ArticleBuilder
    {
        private readonly List<Article> _articles = new();
        private readonly List<Attribute> _attributes = new();

        private string _articleId;
        private string _id;

        public ArticleBuilder WithArticleId(string articleId)
        {
            _articleId = articleId;
            return this;
        }

        public ArticleBuilder WithId(string id)
        {
            _id = id;
            return this;
        }

        public ArticleBuilder WithAttribute(string key, string value, string language = null, string label = null, string source = null)
        {
            var attribute = new Attribute
            {
                Key = key,
                Value = value,
                Language = language,
                Label = label,
                Source = source
            };

            _attributes.Add(attribute);

            return this;
        }

        public ArticleBuilder AddArticle()
        {
            var article = new Article
            {
                ArticleId = _articleId ?? "0",
                Id = _id ?? "0",
                Attributes = _attributes.ToArray()
            };

            _articles.Add(article);

            _articleId = null;
            _id = null;
            _attributes.Clear();

            return this;
        }

        public List<Article> Build()
        {
            return _articles;
        }
    }
}
