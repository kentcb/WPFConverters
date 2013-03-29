using System;
using Kent.Boogaart.Converters.Markup;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    public sealed class ExpressionConverterExtensionTest : UnitTest
    {
        private ExpressionConverterExtension expressionConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.expressionConverterExtension = new ExpressionConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(this.expressionConverterExtension.Expression);
        }

        [Fact]
        public void Constructor_Expression_ShouldSetExpression()
        {
            this.expressionConverterExtension = new ExpressionConverterExtension("expr");
            Assert.Equal("expr", this.expressionConverterExtension.Expression);
        }

        [Fact]
        public void Expression_ShouldGetAndSet()
        {
            Assert.Null(this.expressionConverterExtension.Expression);
            this.expressionConverterExtension.Expression = "expr";
            Assert.Equal("expr", this.expressionConverterExtension.Expression);
        }

        [Fact]
        public void ProvideValue_ShouldThrowIfNoExpression()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => this.expressionConverterExtension.ProvideValue(null));
            Assert.Equal("No expression has been provided.", ex.Message);
        }

        [Fact]
        public void ProvideValue_ShouldYieldExpressionConverterWithGivenExpression()
        {
            this.expressionConverterExtension.Expression = "324 * 21 / {0}";
            ExpressionConverter expressionConverter = this.expressionConverterExtension.ProvideValue(null) as ExpressionConverter;
            Assert.NotNull(expressionConverter);
            Assert.Equal("324 * 21 / {0}", expressionConverter.Expression);
        }
    }
}
