using System.Collections.Generic;

namespace ArticleStore.UnitTests.Builder
{
    internal class AggregatedAttributeBuilder
    {
        private readonly List<AggregatedAttribute> _attributes = new();

        public AggregatedAttributeBuilder WithAttribute(string value, string language)
        {
            var attribute = new AggregatedAttribute
            {
                Value = value,
                Language = language
            };

            _attributes.Add(attribute);

            return this;
        }

        public List<AggregatedAttribute> Build()
        {
            return _attributes;
        }
    }
}
