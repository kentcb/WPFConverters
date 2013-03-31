namespace Kent.Boogaart.Converters.UnitTests
{
    using Xunit;

    public sealed class MappingFixture
    {
        [Fact]
        public void ctor_sets_from_to_null_by_default()
        {
            var mapping = new Mapping();
            Assert.Null(mapping.From);
        }

        [Fact]
        public void ctor_sets_to_to_null_by_default()
        {
            var mapping = new Mapping();
            Assert.Null(mapping.To);
        }

        [Fact]
        public void ctor_that_takes_from_and_to_sets_from_and_to()
        {
            var from = new object();
            var to = new object();
            var mapping = new Mapping(from, to);
            Assert.Same(from, mapping.From);
            Assert.Same(to, mapping.To);
        }

        [Fact]
        public void from_assigns_given_value()
        {
            var mapping = new Mapping();
            var from = new object();
            mapping.From = from;
            Assert.Same(from, mapping.From);
            mapping.From = null;
            Assert.Null(mapping.From);
        }

        [Fact]
        public void to_assigns_given_value()
        {
            var mapping = new Mapping();
            var to = new object();
            mapping.To = to;
            Assert.Same(to, mapping.To);
            mapping.To = null;
            Assert.Null(mapping.To);
        }
    }
}
