namespace Kent.Boogaart.Converters.UnitTests
{
    using System;
    using System.Windows;
    using Xunit;

    public sealed class MapConverterFixture
    {
        [Fact]
        public void ctor_sets_mappings_to_empty_collection()
        {
            var converter = new MapConverter();
            Assert.Empty(converter.Mappings);
        }

        [Fact]
        public void ctor_sets_fallback_behavior_to_return_unset_value()
        {
            var converter = new MapConverter();
            Assert.Equal(FallbackBehavior.ReturnUnsetValue, converter.FallbackBehavior);
        }

        [Fact]
        public void fallback_behavior_throws_if_value_is_invalid()
        {
            var converter = new MapConverter();
            var ex = Assert.Throws<ArgumentException>(() => converter.FallbackBehavior = (FallbackBehavior)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'Kent.Boogaart.Converters.FallbackBehavior'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void mappings_can_be_added()
        {
            var converter = new MapConverter();
            var mapping = new Mapping("from", "to");
            converter.Mappings.Add(mapping);
            Assert.True(converter.Mappings.Contains(mapping));
        }

        [Fact]
        public void convert_honors_fallback_behavior_if_conversion_fails()
        {
            var converter = new MapConverter();
            converter.Mappings.Add(new Mapping("from", "to"));

            converter.FallbackBehavior = FallbackBehavior.ReturnUnsetValue;

            Assert.Same(DependencyProperty.UnsetValue, converter.Convert(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert(123, null, null, null));

            converter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(converter.Convert(null, null, null, null));
            Assert.Same("abc", converter.Convert("abc", null, null, null));
            Assert.Equal(123, converter.Convert(123, null, null, null));

            converter.FallbackBehavior = FallbackBehavior.ReturnFallbackValue;
            converter.FallbackValue = "my fallback value";

            Assert.Equal("my fallback value", converter.Convert(null, null, null, null));
            Assert.Equal("my fallback value", converter.Convert("abc", null, null, null));
            Assert.Equal("my fallback value", converter.Convert(123, null, null, null));
        }

        [Fact]
        public void convert_returns_mapped_value_if_mapping_exists()
        {
            var converter = new MapConverter();
            converter.Mappings.Add(new Mapping("from", "to"));
            converter.Mappings.Add(new Mapping(null, "NULL"));
            converter.Mappings.Add(new Mapping(123, 123.5d));

            Assert.Same("to", converter.Convert("from", null, null, null));
            Assert.Same("NULL", converter.Convert(null, null, null, null));
            Assert.Equal(123.5d, converter.Convert(123, null, null, null));
        }

        [Fact]
        public void convert_back_honors_fallback_behavior_if_conversion_fails()
        {
            var converter = new MapConverter();
            converter.Mappings.Add(new Mapping("from", "to"));

            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack(123, null, null, null));

            converter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(converter.ConvertBack(null, null, null, null));
            Assert.Same("abc", converter.ConvertBack("abc", null, null, null));
            Assert.Equal(123, converter.ConvertBack(123, null, null, null));

            converter.FallbackBehavior = FallbackBehavior.ReturnFallbackValue;
            converter.FallbackValue = "my fallback value";

            Assert.Equal("my fallback value", converter.ConvertBack(null, null, null, null));
            Assert.Equal("my fallback value", converter.ConvertBack("abc", null, null, null));
            Assert.Equal("my fallback value", converter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void convert_back_returns_default_value_if_no_mapping_exists()
        {
            var converter = new MapConverter();
            converter.Mappings.Add(new Mapping("from", "to"));

            converter.FallbackBehavior = FallbackBehavior.ReturnUnsetValue;

            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack(123, null, null, null));

            converter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(converter.ConvertBack(null, null, null, null));
            Assert.Same("abc", converter.ConvertBack("abc", null, null, null));
            Assert.Equal(123, converter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void convert_back_returns_mapped_value_if_mapping_exists()
        {
            var converter = new MapConverter();
            converter.Mappings.Add(new Mapping("from", "to"));
            converter.Mappings.Add(new Mapping(null, "NULL"));
            converter.Mappings.Add(new Mapping(123, 123.5d));

            Assert.Same("from", converter.ConvertBack("to", null, null, null));
            Assert.Null(converter.ConvertBack("NULL", null, null, null));
            Assert.Equal(123, converter.ConvertBack(123.5d, null, null, null));
        }
    }
}
