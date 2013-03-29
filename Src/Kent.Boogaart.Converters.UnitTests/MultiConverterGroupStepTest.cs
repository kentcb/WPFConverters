using System.Windows.Data;
using Moq;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests
{
    public sealed class MultiConverterGroupStepTest : UnitTest
    {
        private MultiConverterGroupStep multiConverterGroupStep;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.multiConverterGroupStep = new MultiConverterGroupStep();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.NotNull(this.multiConverterGroupStep.Converters);
            Assert.Equal(0, this.multiConverterGroupStep.Converters.Count);
        }

        [Fact]
        public void Converters_ShouldAllowManipulationOfConverters()
        {
            var converterMock1 = new Mock<IMultiValueConverter>();
            var converterMock2 = new Mock<IMultiValueConverter>();
            var converterMock3 = new Mock<IMultiValueConverter>();

            this.multiConverterGroupStep.Converters.Add(converterMock1.Object);
            this.multiConverterGroupStep.Converters.Add(converterMock2.Object);
            this.multiConverterGroupStep.Converters.Add(converterMock3.Object);

            Assert.Equal(3, this.multiConverterGroupStep.Converters.Count);
            Assert.True(this.multiConverterGroupStep.Converters.Contains(converterMock1.Object));
            Assert.True(this.multiConverterGroupStep.Converters.Contains(converterMock2.Object));
            Assert.True(this.multiConverterGroupStep.Converters.Contains(converterMock3.Object));
        }
    }
}
