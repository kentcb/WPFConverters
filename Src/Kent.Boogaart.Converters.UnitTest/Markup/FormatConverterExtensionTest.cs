using System;
using Xunit;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
    public sealed class FormatConverterExtensionTest : UnitTest
    {
        private FormatConverterExtension _formatConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _formatConverterExtension = new FormatConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(_formatConverterExtension.FormatString);
        }

        [Fact]
        public void Constructor_FormatString_ShouldSetExpression()
        {
            _formatConverterExtension = new FormatConverterExtension("format");
            Assert.Equal("format", _formatConverterExtension.FormatString);
        }

        [Fact]
        public void FormatString_ShouldGetAndSet()
        {
            Assert.Null(_formatConverterExtension.FormatString);
            _formatConverterExtension.FormatString = "format";
            Assert.Equal("format", _formatConverterExtension.FormatString);
        }

        [Fact]
        public void ProvideValue_ShouldThrowIfNoFormatString()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _formatConverterExtension.ProvideValue(null));
            Assert.Equal("No format string has been provided.", ex.Message);
        }

        [Fact]
        public void ProvideValue_ShouldYieldFormatConverterWithGivenFormatString()
        {
            _formatConverterExtension.FormatString = "format";
            FormatConverter formatConverter = _formatConverterExtension.ProvideValue(null) as FormatConverter;
            Assert.NotNull(formatConverter);
            Assert.Equal("format", formatConverter.FormatString);
        }
    }
}
