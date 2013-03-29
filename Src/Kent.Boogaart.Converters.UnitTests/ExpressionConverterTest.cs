using System;
using System.Windows;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests
{
    public sealed class ExpressionConverterTest : UnitTest
    {
        private ExpressionConverter expressionConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.expressionConverter = new ExpressionConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(this.expressionConverter.Expression);
        }

        [Fact]
        public void Constructor_Expression_ShouldSetExpression()
        {
            this.expressionConverter = new ExpressionConverter(null);
            Assert.Null(this.expressionConverter.Expression);
            this.expressionConverter = new ExpressionConverter("null");
            Assert.Equal("null", this.expressionConverter.Expression);
        }

        [Fact]
        public void Expression_ShouldGetAndSetExpression()
        {
            Assert.Null(this.expressionConverter.Expression);
            this.expressionConverter.Expression = "null";
            Assert.Equal("null", this.expressionConverter.Expression);
            this.expressionConverter.Expression = null;
            Assert.Null(this.expressionConverter.Expression);
        }

        [Fact]
        public void Convert_Single_ShouldThrowIfNoExpression()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => this.expressionConverter.Convert(1, null, null, null));
            Assert.Equal("No Expression has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Single_ShouldEvaluateExpression()
        {
            this.expressionConverter.Expression = "2 * {0}";
            Assert.Equal(10, this.expressionConverter.Convert(5, null, null, null));
            Assert.Equal(-10, this.expressionConverter.Convert(-5, null, null, null));
            Assert.Equal(10d, this.expressionConverter.Convert(5d, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValue()
        {
            Assert.Same(DependencyProperty.UnsetValue, this.expressionConverter.ConvertBack("abc", (Type)null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.expressionConverter.ConvertBack(123, (Type)null, null, null));
        }

        [Fact]
        public void Convert_Multi_ShouldThrowIfNoExpression()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => this.expressionConverter.Convert(new object[] { 123, 89 }, null, null, null));
            Assert.Equal("No Expression has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Multi_ShouldEvaluateExpression()
        {
            this.expressionConverter.Expression = "2 * {0} + {1} - {2}";
            Assert.Equal(22, this.expressionConverter.Convert(new object[] { 10, 3, 1 }, null, null, null));
            Assert.Equal(-22, this.expressionConverter.Convert(new object[] { -10, -3, -1 }, null, null, null));
            Assert.Equal(22d, this.expressionConverter.Convert(new object[] { 10d, 3, 1 }, null, null, null));
        }

        [Fact]
        public void ConvertBack_Multi_ShouldReturnNull()
        {
            Assert.Null(this.expressionConverter.ConvertBack(10, new Type[] { typeof(int), typeof(string) }, null, null));
        }
    }
}