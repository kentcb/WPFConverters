namespace Kent.Boogaart.Converters.UnitTests
{
    using Moq;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Xunit;

    public sealed class ConverterGroupFixture
    {
        [Fact]
        public void ctor_results_in_empty_converters_collection()
        {
            var converter = new ConverterGroup();
            Assert.Empty(converter.Converters);
        }

        [Fact]
        public void converters_allows_converters_to_be_added()
        {
            var converter = new ConverterGroup();
            var converterMock1 = new Mock<IValueConverter>();
            var converterMock2 = new Mock<IValueConverter>();
            var converterMock3 = new Mock<IValueConverter>();

            converter.Converters.Add(converterMock1.Object);
            converter.Converters.Add(converterMock2.Object);
            converter.Converters.Add(converterMock3.Object);

            Assert.Equal(3, converter.Converters.Count);
            Assert.True(converter.Converters.Contains(converterMock1.Object));
            Assert.True(converter.Converters.Contains(converterMock2.Object));
            Assert.True(converter.Converters.Contains(converterMock3.Object));
        }

        [Fact]
        public void convert_returns_unset_value_if_there_are_no_converters()
        {
            var converter = new ConverterGroup();
            Assert.Equal(DependencyProperty.UnsetValue, converter.Convert("abc", null, null, null));
        }

        [Fact]
        public void convert_passes_parameters_into_converters()
        {
            var converter = new ConverterGroup();
            var value = "abc";
            var targetType = typeof(int);
            var parameter = "parameter";
            var cultureInfo = new CultureInfo("de-DE");
            var converterMock = new Mock<IValueConverter>();
            converterMock.Setup(x => x.Convert(value, targetType, parameter, cultureInfo)).Verifiable();
            converter.Converters.Add(converterMock.Object);

            converter.Convert(value, targetType, parameter, cultureInfo);

            converterMock.Verify();
        }

        [Fact]
        public void convert_chains_converters_together_in_order()
        {
            var converter = new ConverterGroup();
            var converterMock1 = new Mock<IValueConverter>();
            var converterMock2 = new Mock<IValueConverter>();
            var converterMock3 = new Mock<IValueConverter>();
            converterMock1.Setup(x => x.Convert("start value", null, null, null)).Returns("converter1 result").Verifiable();
            converterMock2.Setup(x => x.Convert("converter1 result", null, null, null)).Returns("converter2 result").Verifiable();
            converterMock3.Setup(x => x.Convert("converter2 result", null, null, null)).Returns("converter3 result").Verifiable();
            converter.Converters.Add(converterMock1.Object);
            converter.Converters.Add(converterMock2.Object);
            converter.Converters.Add(converterMock3.Object);

            Assert.Equal("converter3 result", converter.Convert("start value", null, null, null));

            converterMock1.Verify();
            converterMock2.Verify();
            converterMock3.Verify();
        }

        [Fact]
        public void convert_back_returns_unset_value_if_there_are_no_converters()
        {
            var converter = new ConverterGroup();
            Assert.Equal(DependencyProperty.UnsetValue, converter.ConvertBack("abc", null, null, null));
        }

        [Fact]
        public void convert_back_passes_parameters_into_converters()
        {
            var converter = new ConverterGroup();
            var value = "abc";
            var targetType = typeof(int);
            var parameter = "parameter";
            var cultureInfo = new CultureInfo("de-DE");
            var converterMock = new Mock<IValueConverter>();
            converterMock.Setup(x => x.ConvertBack(value, targetType, parameter, cultureInfo)).Verifiable();
            converter.Converters.Add(converterMock.Object);

            converter.ConvertBack(value, targetType, parameter, cultureInfo);

            converterMock.Verify();
        }

        [Fact]
        public void convert_back_chains_converters_together_in_reverse_order()
        {
            var converter = new ConverterGroup();
            var converterMock1 = new Mock<IValueConverter>();
            var converterMock2 = new Mock<IValueConverter>();
            var converterMock3 = new Mock<IValueConverter>();
            converterMock3.Setup(x => x.ConvertBack("start value", null, null, null)).Returns("converter1 result").Verifiable();
            converterMock2.Setup(x => x.ConvertBack("converter1 result", null, null, null)).Returns("converter2 result").Verifiable();
            converterMock1.Setup(x => x.ConvertBack("converter2 result", null, null, null)).Returns("converter3 result").Verifiable();
            converter.Converters.Add(converterMock1.Object);
            converter.Converters.Add(converterMock2.Object);
            converter.Converters.Add(converterMock3.Object);

            Assert.Equal("converter3 result", converter.ConvertBack("start value", null, null, null));

            converterMock1.Verify();
            converterMock2.Verify();
            converterMock3.Verify();
        }
    }
}