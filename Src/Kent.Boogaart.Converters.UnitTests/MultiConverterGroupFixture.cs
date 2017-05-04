namespace Kent.Boogaart.Converters.UnitTests
{
    using Moq;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Xunit;

    public sealed class MultiConverterGroupFixture
    {
        [Fact]
        public void ctor_results_in_empty_steps_collection()
        {
            var converter = new MultiConverterGroup();
            Assert.Empty(converter.Steps);
        }

        [Fact]
        public void steps_allows_steps_to_be_added()
        {
            var converter = new MultiConverterGroup();
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();
            var step3 = new MultiConverterGroupStep();
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);
            converter.Steps.Add(step3);

            Assert.Equal(3, converter.Steps.Count);
            Assert.True(converter.Steps.Contains(step1));
            Assert.True(converter.Steps.Contains(step2));
            Assert.True(converter.Steps.Contains(step3));
        }

        [Fact]
        public void convert_returns_unset_value_if_there_are_no_steps()
        {
            var converter = new MultiConverterGroup();
            Assert.Equal(DependencyProperty.UnsetValue, converter.Convert(new object[] { }, null, null, null));
        }

        [Fact]
        public void convert_throws_if_last_step_has_more_than_one_converter_in_it()
        {
            var converter = new MultiConverterGroup();
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);

            var ex = Assert.Throws<InvalidOperationException>(() => converter.Convert(new object[] { }, null, null, null));
            Assert.Equal("The final step in a MultiConverterGroup must have a single converter added to it.", ex.Message);
        }

        [Fact]
        public void convert_throws_if_any_step_has_no_converter_in_it()
        {
            var converter = new MultiConverterGroup();
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();
            var step3 = new MultiConverterGroupStep();
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step3.Converters.Add(new Mock<IMultiValueConverter>().Object);
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);
            converter.Steps.Add(step3);

            var ex = Assert.Throws<InvalidOperationException>(() => converter.Convert(new object[] { }, null, null, null));
            Assert.Equal("Each step in a MultiConverterGroup must have at least one converter added to it.", ex.Message);
        }

        [Fact]
        public void convert_executes_steps_in_order()
        {
            var converter = new MultiConverterGroup();
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
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);

            Assert.Equal("final converted value", converter.Convert(values, targetType, parameter, cultureInfo));
        }

        [Fact]
        public void convert_back_returns_null_if_there_are_no_steps()
        {
            var converter = new MultiConverterGroup();
            Assert.Null(converter.ConvertBack(new object[] { }, null, null, null));
        }

        [Fact]
        public void convert_back_throws_if_last_step_has_more_than_one_converter_in_it()
        {
            var converter = new MultiConverterGroup();
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step2.Converters.Add(new Mock<IMultiValueConverter>().Object);
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);

            var ex = Assert.Throws<InvalidOperationException>(() => converter.ConvertBack(new object[] { }, null, null, null));
            Assert.Equal("The final step in a MultiConverterGroup must have a single converter added to it.", ex.Message);
        }

        [Fact]
        public void convert_back_throws_if_any_step_has_no_converter_in_it()
        {
            var converter = new MultiConverterGroup();
            var step1 = new MultiConverterGroupStep();
            var step2 = new MultiConverterGroupStep();
            var step3 = new MultiConverterGroupStep();
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step1.Converters.Add(new Mock<IMultiValueConverter>().Object);
            step3.Converters.Add(new Mock<IMultiValueConverter>().Object);
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);
            converter.Steps.Add(step3);

            var ex = Assert.Throws<InvalidOperationException>(() => converter.ConvertBack(new object[] { }, null, null, null));
            Assert.Equal("Each step in a MultiConverterGroup must have at least one converter added to it.", ex.Message);
        }

        [Fact]
        public void convert_back_throws_if_number_of_values_produced_by_step_does_not_match_number_of_converters_in_next_step()
        {
            var converter = new MultiConverterGroup();
            var values = new object[] { "abc", 123 };
            var targetTypes = new Type[] { };
            var parameter = "parameter";
            var cultureInfo = new CultureInfo("de-DE");
            var converterMock3 = new Mock<IMultiValueConverter>();
            converterMock3.Setup(x => x.ConvertBack("final converted value", targetTypes, parameter, cultureInfo)).Returns(new object[] { "converted value1", "converted value2", "too", "many", "values" });
            var converterMock1 = new Mock<IMultiValueConverter>();
            converterMock1.Setup(x => x.ConvertBack("converted value1", targetTypes, parameter, cultureInfo)).Returns(values);
            var converterMock2 = new Mock<IMultiValueConverter>();
            converterMock2.Setup(x => x.ConvertBack("converted value2", targetTypes, parameter, cultureInfo)).Throws<Exception>();
            var step1 = new MultiConverterGroupStep();
            step1.Converters.Add(converterMock1.Object);
            step1.Converters.Add(converterMock2.Object);
            var step2 = new MultiConverterGroupStep();
            step2.Converters.Add(converterMock3.Object);
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);

            var ex = Assert.Throws<InvalidOperationException>(() => converter.ConvertBack("final converted value", targetTypes, parameter, cultureInfo));
            Assert.Equal("Step with index 1 produced 5 values but the subsequent step (index 0) requires 2 values.", ex.Message);
        }

        [Fact]
        public void convert_back_executes_steps_in_reverse_order()
        {
            var converter = new MultiConverterGroup();
            var values = new object[] { "abc", 123 };
            var targetTypes = new Type[] { };
            var parameter = "parameter";
            var cultureInfo = new CultureInfo("de-DE");
            var converterMock3 = new Mock<IMultiValueConverter>();
            converterMock3.Setup(x => x.ConvertBack("final converted value", targetTypes, parameter, cultureInfo)).Returns(new object[] { "converted value1", "converted value2" });
            var converterMock1 = new Mock<IMultiValueConverter>();
            converterMock1.Setup(x => x.ConvertBack("converted value1", targetTypes, parameter, cultureInfo)).Returns(values);
            var converterMock2 = new Mock<IMultiValueConverter>();
            converterMock2.Setup(x => x.ConvertBack("converted value2", targetTypes, parameter, cultureInfo)).Throws<Exception>();
            var step1 = new MultiConverterGroupStep();
            step1.Converters.Add(converterMock1.Object);
            step1.Converters.Add(converterMock2.Object);
            var step2 = new MultiConverterGroupStep();
            step2.Converters.Add(converterMock3.Object);
            converter.Steps.Add(step1);
            converter.Steps.Add(step2);

            Assert.Equal(values, converter.ConvertBack("final converted value", targetTypes, parameter, cultureInfo));
        }
    }
}