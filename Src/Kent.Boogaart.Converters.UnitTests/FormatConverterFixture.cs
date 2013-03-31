namespace Kent.Boogaart.Converters.UnitTests
{
    using System;
    using System.Globalization;
    using System.Windows;
    using Xunit;

    public sealed class FormatConverterFixture
    {
        [Fact]
        public void ctor_format_string_is_null_by_default()
        {
            var converter = new FormatConverter();
            Assert.Null(converter.FormatString);
        }

        [Fact]
        public void ctor_that_takes_format_string_assigns_to_format_string()
        {
            var converter = new FormatConverter(null);
            Assert.Null(converter.FormatString);

            converter = new FormatConverter("abc");
            Assert.Equal("abc", converter.FormatString);
        }

        [Fact]
        public void convert_value_throws_if_format_string_is_null()
        {
            var converter = new FormatConverter();
            var ex = Assert.Throws<InvalidOperationException>(() => converter.Convert(27, null, null, null));
            Assert.Equal("No FormatString has been specified.", ex.Message);
        }

        [Fact]
        public void convert_value_formats_the_value_using_the_format_string()
        {
            var converter = new FormatConverter
            {
                FormatString = "{0:00.00}"
            };
            Assert.Equal("01.00", converter.Convert(1d, null, null, null));
        }

        [Fact]
        public void convert_value_uses_the_specified_culture()
        {
            var converter = new FormatConverter
            {
                FormatString = "{0:00.00}"
            };
            Assert.Equal("01,00", converter.Convert(1d, null, null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void convert_back_value_throws_if_there_is_no_target_type()
        {
            var converter = new FormatConverter();
            Assert.Throws<ArgumentNullException>(() => converter.ConvertBack(null, (Type)null, null, null));
        }

        [Fact]
        public void convert_back_value_returns_unset_value_if_the_conversion_fails()
        {
            var converter = new FormatConverter
            {
                FormatString = "{0:00.00}"
            };
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack("abc", typeof(int), null, null));
        }

        [Fact]
        public void convert_back_value_converts_to_target_type_if_possible()
        {
            var converter = new FormatConverter
            {
                FormatString = "{0:00.00}"
            };
            Assert.Equal(123, converter.ConvertBack("123", typeof(int), null, null));
            Assert.Equal(13.80, converter.ConvertBack("13.8", typeof(double), null, null));
        }

        [Fact]
        public void convert_back_value_uses_specified_culture()
        {
            var converter = new FormatConverter
            {
                FormatString = "{0:00.00}"
            };
            Assert.Equal(123, converter.ConvertBack("123", typeof(int), null, new CultureInfo("de-DE")));
            Assert.Equal(13.80, converter.ConvertBack("13,8", typeof(double), null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void convert_values_throws_if_format_string_is_null()
        {
            var converter = new FormatConverter();
            var ex = Assert.Throws<InvalidOperationException>(() => converter.Convert(new object[] { 26, 27 }, null, null, null));
            Assert.Equal("No FormatString has been specified.", ex.Message);
        }

        [Fact]
        public void convert_values_formats_the_value_using_the_format_string()
        {
            var converter = new FormatConverter();
            converter.FormatString = "Value 1: {0} and value 2: {1}";
            Assert.Equal("Value 1: 26 and value 2: 27", converter.Convert(new object[] { 26, 27 }, null, null, null));
        }

        [Fact]
        public void convert_values_uses_the_specified_culture()
        {
            var converter = new FormatConverter();
            converter.FormatString = "Value 1: {0:00.00} and value 2: {1:00.00}";
            Assert.Equal("Value 1: 26,00 and value 2: 27,00", converter.Convert(new object[] { 26, 27 }, null, null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void convert_back_values_returns_null()
        {
            var converter = new FormatConverter();
            Assert.Null(converter.ConvertBack("something", new Type[] { }, null, null));
        }
    }
}
