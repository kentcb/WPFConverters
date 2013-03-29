using Xunit;

namespace Kent.Boogaart.Converters.UnitTests
{
    public sealed class MappingTest : UnitTest
    {
        private Mapping mapping;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.mapping = new Mapping();
        }

        [Fact]
        public void Constructor_ShouldSetUpDefaults()
        {
            Assert.Null(this.mapping.From);
            Assert.Null(this.mapping.To);
        }

        [Fact]
        public void Constructor_FromTo_ShouldAssignToFromAndTo()
        {
            object from = new object();
            object to = new object();
            this.mapping = new Mapping(from, to);
            Assert.Same(from, this.mapping.From);
            Assert.Same(to, this.mapping.To);
        }

        [Fact]
        public void From_ShouldAssignToFrom()
        {
            object from = new object();
            this.mapping.From = from;
            Assert.Same(from, this.mapping.From);
            this.mapping.From = null;
            Assert.Null(this.mapping.From);
        }

        [Fact]
        public void To_ShouldAssignToTo()
        {
            object to = new object();
            this.mapping.To = to;
            Assert.Same(to, this.mapping.To);
            this.mapping.To = null;
            Assert.Null(this.mapping.To);
        }
    }
}
