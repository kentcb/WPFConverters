using System;
using System.Windows;
using Xunit;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class MapConverterTest : UnitTest
    {
        private MapConverter _mapConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _mapConverter = new MapConverter();
        }

        [Fact]
        public void Constructor_ShouldSetUpDefaults()
        {
            Assert.NotNull(_mapConverter.Mappings);
            Assert.Empty(_mapConverter.Mappings as System.Collections.ICollection);
            Assert.Equal(FallbackBehavior.ReturnUnsetValue, _mapConverter.FallbackBehavior);
        }

        [Fact]
        public void FallbackBehavior_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _mapConverter.FallbackBehavior = (FallbackBehavior) 100);
            Assert.Equal("'100' is not a valid value for property 'FallbackBehavior'.", ex.Message);
        }

        [Fact]
        public void FallbackBehavior_ShouldGetAndSetFallbackBehavior()
        {
            _mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;
            Assert.Equal(FallbackBehavior.ReturnOriginalValue, _mapConverter.FallbackBehavior);
            _mapConverter.FallbackBehavior = FallbackBehavior.ReturnUnsetValue;
            Assert.Equal(FallbackBehavior.ReturnUnsetValue, _mapConverter.FallbackBehavior);
        }

        [Fact]
        public void Mappings_ShouldAllowManipulation()
        {
            Mapping mapping = new Mapping("from", "to");
            _mapConverter.Mappings.Add(mapping);
            Assert.True(_mapConverter.Mappings.Contains(mapping));
        }

        [Fact]
        public void Convert_ShouldHonourFallbackBehaviorIfConversionFails()
        {
            _mapConverter.Mappings.Add(new Mapping("from", "to"));

            Assert.Same(DependencyProperty.UnsetValue, _mapConverter.Convert(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _mapConverter.Convert("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _mapConverter.Convert(123, null, null, null));

            _mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(_mapConverter.Convert(null, null, null, null));
            Assert.Same("abc", _mapConverter.Convert("abc", null, null, null));
            Assert.Equal(123, _mapConverter.Convert(123, null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnToValueIfMappingExists()
        {
            _mapConverter.Mappings.Add(new Mapping("from", "to"));
            _mapConverter.Mappings.Add(new Mapping(null, "NULL"));
            _mapConverter.Mappings.Add(new Mapping(123, 123.5d));

            Assert.Same("to", _mapConverter.Convert("from", null, null, null));
            Assert.Same("NULL", _mapConverter.Convert(null, null, null, null));
            Assert.Equal(123.5d, _mapConverter.Convert(123, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnDefaultValueIfNoMappingExists()
        {
            _mapConverter.Mappings.Add(new Mapping("from", "to"));

            Assert.Same(DependencyProperty.UnsetValue, _mapConverter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _mapConverter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _mapConverter.ConvertBack(123, null, null, null));

            _mapConverter.FallbackBehavior = FallbackBehavior.ReturnOriginalValue;

            Assert.Null(_mapConverter.ConvertBack(null, null, null, null));
            Assert.Same("abc", _mapConverter.ConvertBack("abc", null, null, null));
            Assert.Equal(123, _mapConverter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnToValueIfMappingExists()
        {
            _mapConverter.Mappings.Add(new Mapping("from", "to"));
            _mapConverter.Mappings.Add(new Mapping(null, "NULL"));
            _mapConverter.Mappings.Add(new Mapping(123, 123.5d));

            Assert.Same("from", _mapConverter.ConvertBack("to", null, null, null));
            Assert.Null(_mapConverter.ConvertBack("NULL", null, null, null));
            Assert.Equal(123, _mapConverter.ConvertBack(123.5d, null, null, null));
        }
    }
}
