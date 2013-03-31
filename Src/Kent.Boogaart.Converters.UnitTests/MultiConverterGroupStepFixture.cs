namespace Kent.Boogaart.Converters.UnitTests
{
    using Moq;
    using System.Windows.Data;
    using Xunit;

    public sealed class MultiConverterGroupStepFixture
    {
        [Fact]
        public void ctor_results_in_empty_converters()
        {
            var converter = new MultiConverterGroupStep();
            Assert.Empty(converter.Converters);
        }

        [Fact]
        public void converters_allows_converters_to_be_added()
        {
            var converter = new MultiConverterGroupStep();
            var converterMock1 = new Mock<IMultiValueConverter>();
            var converterMock2 = new Mock<IMultiValueConverter>();
            var converterMock3 = new Mock<IMultiValueConverter>();
            converter.Converters.Add(converterMock1.Object);
            converter.Converters.Add(converterMock2.Object);
            converter.Converters.Add(converterMock3.Object);

            Assert.Equal(3, converter.Converters.Count);
            Assert.True(converter.Converters.Contains(converterMock1.Object));
            Assert.True(converter.Converters.Contains(converterMock2.Object));
            Assert.True(converter.Converters.Contains(converterMock3.Object));
        }
    }
}
