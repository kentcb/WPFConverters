namespace Kent.Boogaart.Converters.UnitTests
{
    using System;
    using Xunit;

    public sealed class ExpressionConverterFixture
    {
        [Fact]
        public void ctor_expression_is_null_by_default()
        {
            var converter = new ExpressionConverter();
            Assert.Null(converter.Expression);
        }

        [Fact]
        public void ctor_that_takes_expression_assigns_expression()
        {
            var converter = new ExpressionConverter(null);
            Assert.Null(converter.Expression);

            converter = new ExpressionConverter("null");
            Assert.Equal("null", converter.Expression);
        }

        [Fact]
        public void convert_value_throws_if_expression_is_null()
        {
            var converter = new ExpressionConverter();
            var ex = Assert.Throws<InvalidOperationException>(() => converter.Convert(1, null, null, null));
            Assert.Equal("No Expression has been specified.", ex.Message);
        }

        [Fact]
        public void convert_value_evaluates_expression_to_obtain_value()
        {
            var converter = new ExpressionConverter();
            converter.Expression = "2 * {0}";

            Assert.Equal(10, converter.Convert(5, null, null, null));
            Assert.Equal(-10, converter.Convert(-5, null, null, null));
            Assert.Equal(10d, converter.Convert(5d, null, null, null));
        }

        [Fact]
        public void convert_back_value_returns_unset_value()
        {
            var converter = new ExpressionConverter();
            Assert.Same(System.Windows.DependencyProperty.UnsetValue, converter.ConvertBack("abc", (Type)null, null, null));
            Assert.Same(System.Windows.DependencyProperty.UnsetValue, converter.ConvertBack(123, (Type)null, null, null));
        }

        [Fact]
        public void convert_values_throws_if_expression_is_null()
        {
            var converter = new ExpressionConverter();
            var ex = Assert.Throws<InvalidOperationException>(() => converter.Convert(new object[] { 123, 89 }, null, null, null));
            Assert.Equal("No Expression has been specified.", ex.Message);
        }

        [Fact]
        public void convert_values_evaluates_expression_to_obtain_value()
        {
            var converter = new ExpressionConverter();
            converter.Expression = "2 * {0} + {1} - {2}";

            Assert.Equal(22, converter.Convert(new object[] { 10, 3, 1 }, null, null, null));
            Assert.Equal(-22, converter.Convert(new object[] { -10, -3, -1 }, null, null, null));
            Assert.Equal(22d, converter.Convert(new object[] { 10d, 3, 1 }, null, null, null));
        }

        [Fact]
        public void convert_back_values_returns_null()
        {
            var converter = new ExpressionConverter();
            Assert.Null(converter.ConvertBack(10, new Type[] { typeof(int), typeof(string) }, null, null));
        }
    }
}