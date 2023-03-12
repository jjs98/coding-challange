using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticleStore
{
    public class Article
    {
        public string Id { get; set; }

        public string ArticleId { get; set; }

        public IEnumerable<Attribute> Attributes { get; set; }
    }

    public class Attribute
    {
        public string Key { get; set; }
        public string Language { get; set; }
        public string Value { get; set; }
        public string Source { get; set; }
        public string Label { get; set; }
    }

    public class AggregatedArticle
    {
        [Key]
        public string ArticleId { get; set; }
        public string Brand { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<AggregatedAttribute> Material { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<AggregatedAttribute> SecondMaterial { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<AggregatedAttribute> ThirdMaterial { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<AggregatedAttribute> Alloy { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<AggregatedAttribute> SecondAlloy { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<AggregatedAttribute> ThirdAlloy { get; set; }
        public string Collection { get; set; }
        public string ProductGroup { get; set; }
        public string MainProductGroup { get; set; }
        [Column(TypeName = "jsonb")]
        public IEnumerable<AggregatedAttribute> Target { get; set; }
    }

    public class AggregatedAttribute
    {
        public string Language { get; set; }
        public string Value { get; set; }
    }
}