using System;
using Xunit;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
    public sealed class ExpressionConverterExtensionTest : UnitTest
    {
        private ExpressionConverterExtension _expressionConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _expressionConverterExtension = new ExpressionConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(_expressionConverterExtension.Expression);
        }

        [Fact]
        public void Constructor_Expression_ShouldSetExpression()
        {
            _expressionConverterExtension = new ExpressionConverterExtension("expr");
            Assert.Equal("expr", _expressionConverterExtension.Expression);
        }

        [Fact]
        public void Expression_ShouldGetAndSet()
        {
            Assert.Null(_expressionConverterExtension.Expression);
            _expressionConverterExtension.Expression = "expr";
            Assert.Equal("expr", _expressionConverterExtension.Expression);
        }

        [Fact]
        public void ProvideValue_ShouldThrowIfNoExpression()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _expressionConverterExtension.ProvideValue(null));
            Assert.Equal("No expression has been provided.", ex.Message);
        }

        [Fact]
        public void ProvideValue_ShouldYieldExpressionConverterWithGivenExpression()
        {
            _expressionConverterExtension.Expression = "324 * 21 / {0}";
            ExpressionConverter expressionConverter = _expressionConverterExtension.ProvideValue(null) as ExpressionConverter;
            Assert.NotNull(expressionConverter);
            Assert.Equal("324 * 21 / {0}", expressionConverter.Expression);
        }
    }
}
