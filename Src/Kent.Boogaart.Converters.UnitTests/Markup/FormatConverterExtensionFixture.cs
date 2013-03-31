namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    using Kent.Boogaart.Converters.Markup;
    using System;
    using Xunit;

    public sealed class FormatConverterExtensionFixture
    {
        [Fact]
        public void ctor_sets_format_string_to_null()
        {
            var converterExtension = new FormatConverterExtension();
            Assert.Null(converterExtension.FormatString);
        }

        [Fact]
        public void ctor_that_takes_format_string_sets_format_string()
        {
            var converterExtension = new FormatConverterExtension("format");
            Assert.Equal("format", converterExtension.FormatString);
        }

        [Fact]
        public void provide_value_throws_if_format_string_is_null()
        {
            var converterExtension = new FormatConverterExtension();
            var ex = Assert.Throws<InvalidOperationException>(() => converterExtension.ProvideValue(null));
            Assert.Equal("No format string has been provided.", ex.Message);
        }

        [Fact]
        public void provide_value_returns_appropriate_format_converter()
        {
            var converterExtension = new FormatConverterExtension
            {
                FormatString = "format"
            };
            var formatConverter = converterExtension.ProvideValue(null) as FormatConverter;

            Assert.NotNull(formatConverter);
            Assert.Equal("format", formatConverter.FormatString);
        }
    }
}
