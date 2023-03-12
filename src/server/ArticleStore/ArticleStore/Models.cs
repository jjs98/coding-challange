using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ArticleStore
{
    public class Article
    {
        public string Id { get; set; }

        public string ArticleId { get; set; }

        public Attributes[] Attributes { get; set; }
    }

    public class Attributes
    {
        public string Key { get; set; }
        public string Source { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public string Language { get; set; }
    }

    public class AggregatedArticle : IDisposable
    {
        [Key]
        public string ArticleId { get; set; }
        public string Brand { get; set; }
        public string Material { get; set; }
        public string SecondMaterial { get; set; }
        public string ThirdMaterial { get; set; }
        public string Alloy { get; set; }
        public string SecondAlloy { get; set; }
        public string ThirdAlloy { get; set; }
        public string Collection { get; set; }
        public string ProductGroup { get; set; }
        public string MainProductGroup { get; set; }
        public string Target { get; set; }


        public JsonDocument Data { get; set; }

        public void Dispose() => Data?.Dispose();
    }
}