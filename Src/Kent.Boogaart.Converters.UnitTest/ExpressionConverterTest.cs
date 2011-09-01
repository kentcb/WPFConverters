using System;
using System.Windows;
using Xunit;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class ExpressionConverterTest : UnitTest
    {
        private ExpressionConverter _expressionConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _expressionConverter = new ExpressionConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(_expressionConverter.Expression);
        }

        [Fact]
        public void Constructor_Expression_ShouldSetExpression()
        {
            _expressionConverter = new ExpressionConverter(null);
            Assert.Null(_expressionConverter.Expression);
            _expressionConverter = new ExpressionConverter("null");
            Assert.Equal("null", _expressionConverter.Expression);
        }

        [Fact]
        public void Expression_ShouldGetAndSetExpression()
        {
            Assert.Null(_expressionConverter.Expression);
            _expressionConverter.Expression = "null";
            Assert.Equal("null", _expressionConverter.Expression);
            _expressionConverter.Expression = null;
            Assert.Null(_expressionConverter.Expression);
        }

        [Fact]
        public void Convert_Single_ShouldThrowIfNoExpression()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _expressionConverter.Convert(1, null, null, null));
            Assert.Equal("No Expression has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Single_ShouldEvaluateExpression()
        {
            _expressionConverter.Expression = "2 * {0}";
            Assert.Equal(10, _expressionConverter.Convert(5, null, null, null));
            Assert.Equal(-10, _expressionConverter.Convert(-5, null, null, null));
            Assert.Equal(10d, _expressionConverter.Convert(5d, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValue()
        {
            Assert.Same(DependencyProperty.UnsetValue, _expressionConverter.ConvertBack("abc", (Type) null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _expressionConverter.ConvertBack(123, (Type) null, null, null));
        }

        [Fact]
        public void Convert_Multi_ShouldThrowIfNoExpression()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _expressionConverter.Convert(new object[] { 123, 89 }, null, null, null));
            Assert.Equal("No Expression has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Multi_ShouldEvaluateExpression()
        {
            _expressionConverter.Expression = "2 * {0} + {1} - {2}";
            Assert.Equal(22, _expressionConverter.Convert(new object[] { 10, 3, 1 }, null, null, null));
            Assert.Equal(-22, _expressionConverter.Convert(new object[] { -10, -3, -1 }, null, null, null));
            Assert.Equal(22d, _expressionConverter.Convert(new object[] { 10d, 3, 1 }, null, null, null));
        }

        [Fact]
        public void ConvertBack_Multi_ShouldReturnNull()
        {
            Assert.Null(_expressionConverter.ConvertBack(10, new Type[] { typeof(int), typeof(string) }, null, null));
        }
    }
}