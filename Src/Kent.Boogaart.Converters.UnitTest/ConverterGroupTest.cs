using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Xunit;
using Kent.Boogaart.Converters;
using Moq;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class ConverterGroupTest : UnitTest
    {
        private ConverterGroup _converterGroup;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _converterGroup = new ConverterGroup();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.NotNull(_converterGroup.Converters);
            Assert.Equal(0, _converterGroup.Converters.Count);
        }

        [Fact]
        public void Converters_ShouldAllowManipulationOfConverters()
        {
            var converterMock1 = new Mock<IValueConverter>();
            var converterMock2 = new Mock<IValueConverter>();
            var converterMock3 = new Mock<IValueConverter>();

            _converterGroup.Converters.Add(converterMock1.Object);
            _converterGroup.Converters.Add(converterMock2.Object);
            _converterGroup.Converters.Add(converterMock3.Object);

            Assert.Equal(3, _converterGroup.Converters.Count);
            Assert.True(_converterGroup.Converters.Contains(converterMock1.Object));
            Assert.True(_converterGroup.Converters.Contains(converterMock2.Object));
            Assert.True(_converterGroup.Converters.Contains(converterMock3.Object));
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfNoConverters()
        {
            Assert.Equal(DependencyProperty.UnsetValue, _converterGroup.Convert("abc", null, null, null));
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

            _converterGroup.Converters.Add(converterMock.Object);
            Assert.Equal("converted value", _converterGroup.Convert(value, targetType, parameter, cultureInfo));
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

            _converterGroup.Converters.Add(converterMock1.Object);
            _converterGroup.Converters.Add(converterMock2.Object);
            _converterGroup.Converters.Add(converterMock3.Object);
            Assert.Equal("converter3 result", _converterGroup.Convert("start value", null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfNoConverters()
        {
            Assert.Equal(DependencyProperty.UnsetValue, _converterGroup.ConvertBack("abc", null, null, null));
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

            _converterGroup.Converters.Add(converterMock.Object);
            Assert.Equal("converted value", _converterGroup.ConvertBack(value, targetType, parameter, cultureInfo));
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

            _converterGroup.Converters.Add(converterMock1.Object);
            _converterGroup.Converters.Add(converterMock2.Object);
            _converterGroup.Converters.Add(converterMock3.Object);
            Assert.Equal("converter3 result", _converterGroup.ConvertBack("start value", null, null, null));
        }
    }
}