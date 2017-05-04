namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    using Kent.Boogaart.Converters.Markup;
    using System;
    using Xunit;

    public sealed class ExpressionConverterExtensionFixture
    {
        [Fact]
        public void ctor_sets_expression_to_null()
        {
            var converterExtension = new ExpressionConverterExtension();
            Assert.Null(converterExtension.Expression);
        }

        [Fact]
        public void ctor_that_takes_expression_sets_expression()
        {
            var converterExtension = new ExpressionConverterExtension("expr");
            Assert.Equal("expr", converterExtension.Expression);
        }

        [Fact]
        public void provide_value_throws_if_expression_is_null()
        {
            var converterExtension = new ExpressionConverterExtension();
            var ex = Assert.Throws<InvalidOperationException>(() => converterExtension.ProvideValue(null));
            Assert.Equal("No expression has been provided.", ex.Message);
        }

        [Fact]
        public void provide_value_returns_appropriate_expression_converter()
        {
            var converterExtension = new ExpressionConverterExtension
            {
                Expression = "324 * 21 / {0}"
            };
            var expressionConverter = converterExtension.ProvideValue(null) as ExpressionConverter;

            Assert.NotNull(expressionConverter);
            Assert.Equal("324 * 21 / {0}", expressionConverter.Expression);
        }
    }
}
