using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Moq;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests
{
    public sealed class ConverterGroupTest : UnitTest
    {
        private ConverterGroup converterGroup;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.converterGroup = new ConverterGroup();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.NotNull(this.converterGroup.Converters);
            Assert.Equal(0, this.converterGroup.Converters.Count);
        }

        [Fact]
        public void Converters_ShouldAllowManipulationOfConverters()
        {
            var converterMock1 = new Mock<IValueConverter>();
            var converterMock2 = new Mock<IValueConverter>();
            var converterMock3 = new Mock<IValueConverter>();

            this.converterGroup.Converters.Add(converterMock1.Object);
            this.converterGroup.Converters.Add(converterMock2.Object);
            this.converterGroup.Converters.Add(converterMock3.Object);

            Assert.Equal(3, this.converterGroup.Converters.Count);
            Assert.True(this.converterGroup.Converters.Contains(converterMock1.Object));
            Assert.True(this.converterGroup.Converters.Contains(converterMock2.Object));
            Assert.True(this.converterGroup.Converters.Contains(converterMock3.Object));
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfNoConverters()
        {
            Assert.Equal(DependencyProperty.UnsetValue, this.converterGroup.Convert("abc", null, null, null));
        }

        [Fact]
        public void Convert_ShouldPassParametersIntoConverters()
        {
            object value = "abc";
            Type targetType = typeof(int);
            object parameter = "parameter";
            CultureInfo cultureInfo = new CultureInfo("de-DE");

            var converterMock = new Mock<IValueConverter>();
            converterMock.Setup(x => x.Convert(value, targetType, parameter, cultureInfo)).Returns("converted value");

            this.converterGroup.Converters.Add(converterMock.Object);
            Assert.Equal("converted value", this.converterGroup.Convert(value, targetType, parameter, cultureInfo));
        }

        [Fact]
        public void Convert_ShouldChainConvertersTogetherInOrder()
        {
            var converterMock1 = new Mock<IValueConverter>();
            var converterMock2 = new Mock<IValueConverter>();
            var converterMock3 = new Mock<IValueConverter>();

            converterMock1.Setup(x => x.Convert("start value", null, null, null)).Returns("converter1 result");
            converterMock2.Setup(x => x.Convert("converter1 result", null, null, null)).Returns("converter2 result");
            converterMock3.Setup(x => x.Convert("converter2 result", null, null, null)).Returns("converter3 result");

            this.converterGroup.Converters.Add(converterMock1.Object);
            this.converterGroup.Converters.Add(converterMock2.Object);
            this.converterGroup.Converters.Add(converterMock3.Object);
            Assert.Equal("converter3 result", this.converterGroup.Convert("start value", null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfNoConverters()
        {
            Assert.Equal(DependencyProperty.UnsetValue, this.converterGroup.ConvertBack("abc", null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldPassParametersIntoConverters()
        {
            object value = "abc";
            Type targetType = typeof(int);
            object parameter = "parameter";
            CultureInfo cultureInfo = new CultureInfo("de-DE");

            var converterMock = new Mock<IValueConverter>();
            converterMock.Setup(x => x.ConvertBack(value, targetType, parameter, cultureInfo)).Returns("converted value");

            this.converterGroup.Converters.Add(converterMock.Object);
            Assert.Equal("converted value", this.converterGroup.ConvertBack(value, targetType, parameter, cultureInfo));
        }

        [Fact]
        public void ConvertBack_ShouldChainConvertersTogetherInReverseOrder()
        {
            var converterMock1 = new Mock<IValueConverter>();
            var converterMock2 = new Mock<IValueConverter>();
            var converterMock3 = new Mock<IValueConverter>();

            converterMock3.Setup(x => x.ConvertBack("start value", null, null, null)).Returns("converter1 result");
            converterMock2.Setup(x => x.ConvertBack("converter1 result", null, null, null)).Returns("converter2 result");
            converterMock1.Setup(x => x.ConvertBack("converter2 result", null, null, null)).Returns("converter3 result");

            this.converterGroup.Converters.Add(converterMock1.Object);
            this.converterGroup.Converters.Add(converterMock2.Object);
            this.converterGroup.Converters.Add(converterMock3.Object);
            Assert.Equal("converter3 result", this.converterGroup.ConvertBack("start value", null, null, null));
        }
    }
}