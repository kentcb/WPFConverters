using System;
using System.Windows;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests
{
    public sealed class MapConverterTest : UnitTest
    {
        private MapConverter mapConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.mapConverter = new MapConverter();
        }

        [Fact]
        public void Constructor_ShouldSetUpDefaults()
        {
            Assert.NotNull(this.mapConverter.Mappings);
            Assert.Empty(this.mapConverter.Mappings as System.Collections.ICollection);
            Assert.Equal(FallbackBehavior.ReturnUnsetValue, this.mapConverter.FallbackBehavior);
        }

        [Fact]
        public void FallbackBehavior_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.mapConverter.FallbackBehavior = (FallbackBehavior)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'Kent.Boogaart.Converters.FallbackBehavior'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void FallbackBehavior_ShouldGetAndSetFallbackBehavior()
        {
            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;
            Assert.Equal(FallbackBehavior.ReturnOriginalValue, this.mapConverter.FallbackBehavior);
            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnUnsetValue;
            Assert.Equal(FallbackBehavior.ReturnUnsetValue, this.mapConverter.FallbackBehavior);
            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnFallbackValue;
            Assert.Equal(FallbackBehavior.ReturnFallbackValue, this.mapConverter.FallbackBehavior);
        }

        [Fact]
        public void Mappings_ShouldAllowManipulation()
        {
            Mapping mapping = new Mapping("from", "to");
            this.mapConverter.Mappings.Add(mapping);
            Assert.True(this.mapConverter.Mappings.Contains(mapping));
        }

        [Fact]
        public void Convert_ShouldHonourFallbackBehaviorIfConversionFails()
        {
            this.mapConverter.Mappings.Add(new Mapping("from", "to"));

            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.Convert(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.Convert("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.Convert(123, null, null, null));

            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(this.mapConverter.Convert(null, null, null, null));
            Assert.Same("abc", this.mapConverter.Convert("abc", null, null, null));
            Assert.Equal(123, this.mapConverter.Convert(123, null, null, null));

            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnFallbackValue;
            this.mapConverter.FallbackValue = "my fallback value";

            Assert.Equal("my fallback value", this.mapConverter.Convert(null, null, null, null));
            Assert.Equal("my fallback value", this.mapConverter.Convert("abc", null, null, null));
            Assert.Equal("my fallback value", this.mapConverter.Convert(123, null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnToValueIfMappingExists()
        {
            this.mapConverter.Mappings.Add(new Mapping("from", "to"));
            this.mapConverter.Mappings.Add(new Mapping(null, "NULL"));
            this.mapConverter.Mappings.Add(new Mapping(123, 123.5d));

            Assert.Same("to", this.mapConverter.Convert("from", null, null, null));
            Assert.Same("NULL", this.mapConverter.Convert(null, null, null, null));
            Assert.Equal(123.5d, this.mapConverter.Convert(123, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnDefaultValueIfNoMappingExists()
        {
            this.mapConverter.Mappings.Add(new Mapping("from", "to"));

            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.ConvertBack(123, null, null, null));

            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(this.mapConverter.ConvertBack(null, null, null, null));
            Assert.Same("abc", this.mapConverter.ConvertBack("abc", null, null, null));
            Assert.Equal(123, this.mapConverter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnToValueIfMappingExists()
        {
            this.mapConverter.Mappings.Add(new Mapping("from", "to"));
            this.mapConverter.Mappings.Add(new Mapping(null, "NULL"));
            this.mapConverter.Mappings.Add(new Mapping(123, 123.5d));

            Assert.Same("from", this.mapConverter.ConvertBack("to", null, null, null));
            Assert.Null(this.mapConverter.ConvertBack("NULL", null, null, null));
            Assert.Equal(123, this.mapConverter.ConvertBack(123.5d, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldHonourFallbackBehaviorIfConversionFails()
        {
            this.mapConverter.Mappings.Add(new Mapping("from", "to"));

            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.mapConverter.ConvertBack(123, null, null, null));

            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(this.mapConverter.ConvertBack(null, null, null, null));
            Assert.Same("abc", this.mapConverter.ConvertBack("abc", null, null, null));
            Assert.Equal(123, this.mapConverter.ConvertBack(123, null, null, null));

            this.mapConverter.FallbackBehavior = FallbackBehavior.ReturnFallbackValue;
            this.mapConverter.FallbackValue = "my fallback value";

            Assert.Equal("my fallback value", this.mapConverter.ConvertBack(null, null, null, null));
            Assert.Equal("my fallback value", this.mapConverter.ConvertBack("abc", null, null, null));
            Assert.Equal("my fallback value", this.mapConverter.ConvertBack(123, null, null, null));
        }
    }
}
