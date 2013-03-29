using System;
using System.Globalization;
using System.Windows;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests
{
    public sealed class FormatConverterTest : UnitTest
    {
        private FormatConverter formatConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.formatConverter = new FormatConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(this.formatConverter.FormatString);
        }

        [Fact]
        public void Constructor_FormatString_ShouldSetFormatString()
        {
            this.formatConverter = new FormatConverter(null);
            Assert.Null(this.formatConverter.FormatString);
            this.formatConverter = new FormatConverter("abc");
            Assert.Equal("abc", this.formatConverter.FormatString);
        }

        [Fact]
        public void FormatString_ShouldGetAndSetFormatString()
        {
            Assert.Null(this.formatConverter.FormatString);
            this.formatConverter.FormatString = "abc";
            Assert.Equal("abc", this.formatConverter.FormatString);
            this.formatConverter.FormatString = null;
            Assert.Null(this.formatConverter.FormatString);
        }

        [Fact]
        public void Convert_Single_ShouldThrowIfNoFormatString()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => this.formatConverter.Convert(27, null, null, null));
            Assert.Equal("No FormatString has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Single_ShouldFormatValue()
        {
            this.formatConverter.FormatString = "{0:00.00}";
            Assert.Equal("01.00", this.formatConverter.Convert(1d, null, null, null));
        }

        [Fact]
        public void Convert_Single_ShouldUseSpecifiedCulture()
        {
            this.formatConverter.FormatString = "{0:00.00}";
            Assert.Equal("01,00", this.formatConverter.Convert(1d, null, null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void ConvertBack_Single_ShouldThrowIfNoTargetType()
        {
            Assert.Throws<ArgumentNullException>(() => this.formatConverter.ConvertBack(null, (Type)null, null, null));
        }

        [Fact]
        public void ConvertBack_Single_ShouldReturnUnsetValueIfConversionFails()
        {
            this.formatConverter.FormatString = "{0:00.00}";
            Assert.Same(DependencyProperty.UnsetValue, this.formatConverter.ConvertBack("abc", typeof(int), null, null));
        }

        [Fact]
        public void ConvertBack_Single_ShouldConvertBackIfPossible()
        {
            this.formatConverter.FormatString = "{0:00.00}";
            Assert.Equal(123.0, this.formatConverter.ConvertBack("123", typeof(double), null, null));
            Assert.Equal(13.80, this.formatConverter.ConvertBack("13.8", typeof(double), null, null));
        }

        [Fact]
        public void ConvertBack_Single_ShouldUseSpecifiedCulture()
        {
            this.formatConverter.FormatString = "{0:00.00}";
            Assert.Equal(123.0, this.formatConverter.ConvertBack("123", typeof(double), null, new CultureInfo("de-DE")));
            Assert.Equal(13.80, this.formatConverter.ConvertBack("13,8", typeof(double), null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void Convert_Multi_ShouldThrowIfNoFormatString()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => this.formatConverter.Convert(new object[] { 26, 27 }, null, null, null));
            Assert.Equal("No FormatString has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_Multi_ShouldFormatValue()
        {
            this.formatConverter.FormatString = "Value 1: {0} and value 2: {1}";
            Assert.Equal("Value 1: 26 and value 2: 27", this.formatConverter.Convert(new object[] { 26, 27 }, null, null, null));
        }

        [Fact]
        public void Convert_Multi_ShouldUseSpecifiedCulture()
        {
            this.formatConverter.FormatString = "Value 1: {0:00.00} and value 2: {1:00.00}";
            Assert.Equal("Value 1: 26,00 and value 2: 27,00", this.formatConverter.Convert(new object[] { 26, 27 }, null, null, new CultureInfo("de-DE")));
        }

        [Fact]
        public void ConvertBack_Multi_ShouldReturnNull()
        {
            Assert.Null(this.formatConverter.ConvertBack("something", new Type[] { }, null, null));
        }
    }
}
