using System;
using Kent.Boogaart.Converters.Markup;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
    public sealed class FormatConverterExtensionTest : UnitTest
    {
        private FormatConverterExtension formatConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.formatConverterExtension = new FormatConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(this.formatConverterExtension.FormatString);
        }

        [Fact]
        public void Constructor_FormatString_ShouldSetExpression()
        {
            this.formatConverterExtension = new FormatConverterExtension("format");
            Assert.Equal("format", this.formatConverterExtension.FormatString);
        }

        [Fact]
        public void FormatString_ShouldGetAndSet()
        {
            Assert.Null(this.formatConverterExtension.FormatString);
            this.formatConverterExtension.FormatString = "format";
            Assert.Equal("format", this.formatConverterExtension.FormatString);
        }

        [Fact]
        public void ProvideValue_ShouldThrowIfNoFormatString()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => this.formatConverterExtension.ProvideValue(null));
            Assert.Equal("No format string has been provided.", ex.Message);
        }

        [Fact]
        public void ProvideValue_ShouldYieldFormatConverterWithGivenFormatString()
        {
            this.formatConverterExtension.FormatString = "format";
            FormatConverter formatConverter = this.formatConverterExtension.ProvideValue(null) as FormatConverter;
            Assert.NotNull(formatConverter);
            Assert.Equal("format", formatConverter.FormatString);
        }
    }
}
