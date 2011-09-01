using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Xunit;
using Kent.Boogaart.Converters;
using Moq;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class MultiConverterGroupTest : UnitTest
    {
        private MultiConverterGroup _multiConverterGroup;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _multiConverterGroup = new MultiConverterGroup();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.NotNull(_multiConverterGroup.Steps);
            Assert.Equal(0, _multiConverterGroup.Steps.Count);
        }

        [Fact]
        public void Steps_ShouldAllowManipulationOfSteps()
        {
            MultiConverterGroupStep step1 = new MultiConverterGroupStep();
            MultiConverterGroupStep step2 = new MultiConverterGroupStep();
            MultiConverterGroupStep step3 = new MultiConverterGroupStep();

            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);
            _multiConverterGroup.Steps.Add(step3);

            Assert.Equal(3, _multiConverterGroup.Steps.Count);
            Assert.True(_multiConverterGroup.Steps.Contains(step1));
            Assert.True(_multiConverterGroup.Steps.Contains(step2));
            Assert.True(_multiConverterGroup.Steps.Contains(step3));
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfNoSteps()
        {
            Assert.Equal(DependencyProperty.UnsetValue, _multiConverterGroup.Convert(new object[] { }, null, null, null));
        }

        [Fact]
        public void Convert_ShouldThrowIfLastStepHasMoreThanOneConverterInIt()
        {
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();

            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);

            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);

            var ex = Assert.Throws<InvalidOperationException>(() => _multiConverterGroup.Convert(new object[] { }, null, null, null));
            Assert.Equal("The final step in a MultiConverterGroup must have a single converter added to it.", ex.Message);
        }

        [Fact]
        public void Convert_ShouldThrowIfAnyStepHasNoConvertersInIt()
        {
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();
            var step3 = new MultiConverterGroupStep();

            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step3.Converters.Add(new Mock<IMultiValueConverter>().Object);

            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);
            _multiConverterGroup.Steps.Add(step3);

            var ex = Assert.Throws<InvalidOperationException>(() => _multiConverterGroup.Convert(new object[] { }, null, null, null));
            Assert.Equal("Each step in a MultiConverterGroup must have at least one converter added to it.", ex.Message);
        }

        [Fact]
        public void Convert_ShouldExecuteStepsInOrder()
        {
            var values = new object[] { "abc", 123 };
            var targetType = typeof(int);
            var parameter = "parameter";
            var cultureInfo = new CultureInfo("de-DE");

            var converterMock1 = new Mock<IMultiValueConverter>();
            converterMock1.Setup(x => x.Convert(values, targetType, parameter, cultureInfo)).Returns("converted value1");
            var converterMock2 = new Mock<IMultiValueConverter>();
            converterMock2.Setup(x => x.Convert(values, targetType, parameter, cultureInfo)).Returns("converted value2");
            var converterMock3 = new Mock<IMultiValueConverter>();
            converterMock3.Setup(x => x.Convert(new object[] { "converted value1", "converted value2" }, targetType, parameter, cultureInfo)).Returns("final converted value");

            var step1 = new MultiConverterGroupStep();
            step1.Converters.Add(converterMock1.Object);
            step1.Converters.Add(converterMock2.Object);
            var step2 = new MultiConverterGroupStep();
            step2.Converters.Add(converterMock3.Object);
            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);

            Assert.Equal("final converted value", _multiConverterGroup.Convert(values, targetType, parameter, cultureInfo));
        }

        [Fact]
        public void ConvertBack_ShouldReturnNullIfNoSteps()
        {
            Assert.Null(_multiConverterGroup.ConvertBack(new object[] { }, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldThrowIfLastStepHasMoreThanOneConverterInIt()
        {
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();

            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);

            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);

            var ex = Assert.Throws<InvalidOperationException>(() => _multiConverterGroup.ConvertBack(new object[] { }, null, null, null));
            Assert.Equal("The final step in a MultiConverterGroup must have a single converter added to it.", ex.Message);
        }

        [Fact]
        public void ConvertBack_ShouldThrowIfAnyStepHasNoConvertersInIt()
        {
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();
            var step3 = new MultiConverterGroupStep();

            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step3.Converters.Add(new Mock<IMultiValueConverter>().Object);

            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);
            _multiConverterGroup.Steps.Add(step3);

            var ex = Assert.Throws<InvalidOperationException>(() => _multiConverterGroup.ConvertBack(new object[] { }, null, null, null));
            Assert.Equal("Each step in a MultiConverterGroup must have at least one converter added to it.", ex.Message);
        }

        [Fact]
        public void ConvertBack_ShouldThrowIfNumberOfValuesProducedByStepDoesNotEqualNumberOfConvertersInNextStep()
        {
            object[] values = new object[] { "abc", 123 };
            Type[] targetTypes = new Type[] { };
            object parameter = "parameter";
            CultureInfo cultureInfo = new CultureInfo("de-DE");

            var converterMock3 = new Mock<IMultiValueConverter>();
            converterMock3.Setup(x => x.ConvertBack("final converted value", targetTypes, parameter, cultureInfo)).Returns(new object[] { "converted value1", "converted value2", "too", "many", "values" });
            var converterMock1 = new Mock<IMultiValueConverter>();
            converterMock1.Setup(x => x.ConvertBack("converted value1", targetTypes, parameter, cultureInfo)).Returns(values);
            var converterMock2 = new Mock<IMultiValueConverter>();
            converterMock2.Setup(x => x.ConvertBack("converted value2", targetTypes, parameter, cultureInfo)).Throws<Exception>();

            MultiConverterGroupStep step1 = new MultiConverterGroupStep();
            step1.Converters.Add(converterMock1.Object);
            step1.Converters.Add(converterMock2.Object);
            MultiConverterGroupStep step2 = new MultiConverterGroupStep();
            step2.Converters.Add(converterMock3.Object);
            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);

            var ex = Assert.Throws<InvalidOperationException>(() => _multiConverterGroup.ConvertBack("final converted value", targetTypes, parameter, cultureInfo));
            Assert.Equal("Step with index 1 produced 5 values but the subsequent step (index 0) requires 2 values.", ex.Message);
        }

        [Fact]
        public void ConvertBack_ShouldExecuteStepsInReverseOrder()
        {
            object[] values = new object[] { "abc", 123 };
            Type[] targetTypes = new Type[] { };
            object parameter = "parameter";
            CultureInfo cultureInfo = new CultureInfo("de-DE");

            var converterMock3 = new Mock<IMultiValueConverter>();
            converterMock3.Setup(x => x.ConvertBack("final converted value", targetTypes, parameter, cultureInfo)).Returns(new object[] { "converted value1", "converted value2" });
            var converterMock1 = new Mock<IMultiValueConverter>();
            converterMock1.Setup(x => x.ConvertBack("converted value1", targetTypes, parameter, cultureInfo)).Returns(values);
            var converterMock2 = new Mock<IMultiValueConverter>();
            converterMock2.Setup(x => x.ConvertBack("converted value2", targetTypes, parameter, cultureInfo)).Throws<Exception>();

            MultiConverterGroupStep step1 = new MultiConverterGroupStep();
            step1.Converters.Add(converterMock1.Object);
            step1.Converters.Add(converterMock2.Object);
            MultiConverterGroupStep step2 = new MultiConverterGroupStep();
            step2.Converters.Add(converterMock3.Object);
            _multiConverterGroup.Steps.Add(step1);
            _multiConverterGroup.Steps.Add(step2);

            Assert.Equal(values, _multiConverterGroup.ConvertBack("final converted value", targetTypes, parameter, cultureInfo));
        }
    }
}