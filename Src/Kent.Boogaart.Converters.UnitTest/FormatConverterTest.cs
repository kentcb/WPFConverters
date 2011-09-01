using System;
using System.Globalization;
using System.Windows;
using Xunit;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class FormatConverterTest : UnitTest
    {
        private FormatConverter _formatConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _formatConverter = new FormatConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(_formatConverter.FormatString);
        }

        [Fact]
        public void Constructor_FormatString_ShouldSetFormatString()
        {
            _formatConverter = new FormatConverter(null);
            Assert.Null(_formatConverter.FormatString);
            _formatConverter = new FormatConverter("abc");
            Assert.Equal("abc", _formatConverter.FormatString);
        }

        [Fact]
        public void FormatString_ShouldGetAndSetFormatString()
        {
            Assert.Null(_formatConverter.FormatString);
            _formatConverter.FormatString = "abc";
            Assert.Equal("abc", _formatConverter.FormatString);
            _formatConverter.FormatString = null;
            Assert.Null(_formatConverter.FormatString);
        }

        [Fact]
        public void Convert_Single_ShouldThrowIfNoFormatString()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _formatConverter.Convert(27, null, null, null));
            Assert.Equal("No FormatString has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Single_ShouldFormatValue()
        {
            _formatConverter.FormatString = "{0:00.00}";
            Assert.Equal("01.00", _formatConverter.Convert(1d, null, null, null));
        }

        [Fact]
        public void Convert_Single_ShouldUseSpecifiedCulture()
        {
            _formatConverter.FormatString = "{0:00.00}";
            Assert.Equal("01,00", _formatConverter.Convert(1d, null, null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void ConvertBack_Single_ShouldThrowIfNoTargetType()
        {
            Assert.Throws<ArgumentNullException>(() => _formatConverter.ConvertBack(null, (Type)null, null, null));
        }

        [Fact]
        public void ConvertBack_Single_ShouldReturnUnsetValueIfConversionFails()
        {
            _formatConverter.FormatString = "{0:00.00}";
            Assert.Same(DependencyProperty.UnsetValue, _formatConverter.ConvertBack("abc", typeof(int), null, null));
        }

        [Fact]
        public void ConvertBack_Single_ShouldConvertBackIfPossible()
        {
            _formatConverter.FormatString = "{0:00.00}";
            Assert.Equal(123.0, _formatConverter.ConvertBack("123", typeof(double), null, null));
            Assert.Equal(13.80, _formatConverter.ConvertBack("13.8", typeof(double), null, null));
        }

        [Fact]
        public void ConvertBack_Single_ShouldUseSpecifiedCulture()
        {
            _formatConverter.FormatString = "{0:00.00}";
            Assert.Equal(123.0, _formatConverter.ConvertBack("123", typeof(double), null, new CultureInfo("de-DE")));
            Assert.Equal(13.80, _formatConverter.ConvertBack("13,8", typeof(double), null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void Convert_Multi_ShouldThrowIfNoFormatString()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _formatConverter.Convert(new object[] { 26, 27 }, null, null, null));
            Assert.Equal("No FormatString has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Multi_ShouldFormatValue()
        {
            _formatConverter.FormatString = "Value 1: {0} and value 2: {1}";
            Assert.Equal("Value 1: 26 and value 2: 27", _formatConverter.Convert(new object[] { 26, 27 }, null, null, null));
        }

        [Fact]
        public void Convert_Multi_ShouldUseSpecifiedCulture()
        {
            _formatConverter.FormatString = "Value 1: {0:00.00} and value 2: {1:00.00}";
            Assert.Equal("Value 1: 26,00 and value 2: 27,00", _formatConverter.Convert(new object[] { 26, 27 }, null, null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void ConvertBack_Multi_ShouldReturnNull()
        {
            Assert.Null(_formatConverter.ConvertBack("something", new Type[] { }, null, null));
        }
    }
}
