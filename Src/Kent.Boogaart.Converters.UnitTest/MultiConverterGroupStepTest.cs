using System.Windows.Data;
using Xunit;
using Kent.Boogaart.Converters;
using Moq;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class MultiConverterGroupStepTest : UnitTest
    {
        private MultiConverterGroupStep _multiConverterGroupStep;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _multiConverterGroupStep = new MultiConverterGroupStep();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.NotNull(_multiConverterGroupStep.Converters);
            Assert.Equal(0, _multiConverterGroupStep.Converters.Count);
        }

        [Fact]
        public void Converters_ShouldAllowManipulationOfConverters()
        {
            var converterMock1 = new Mock<IMultiValueConverter>();
            var converterMock2 = new Mock<IMultiValueConverter>();
            var converterMock3 = new Mock<IMultiValueConverter>();

            _multiConverterGroupStep.Converters.Add(converterMock1.Object);
            _multiConverterGroupStep.Converters.Add(converterMock2.Object);
            _multiConverterGroupStep.Converters.Add(converterMock3.Object);

            Assert.Equal(3, _multiConverterGroupStep.Converters.Count);
            Assert.True(_multiConverterGroupStep.Converters.Contains(converterMock1.Object));
            Assert.True(_multiConverterGroupStep.Converters.Contains(converterMock2.Object));
            Assert.True(_multiConverterGroupStep.Converters.Contains(converterMock3.Object));
        }
    }
}
