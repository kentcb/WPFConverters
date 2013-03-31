namespace Kent.Boogaart.Converters.UnitTests
{
    using System;
    using System.Windows;
    using Xunit;

    public sealed class DateTimeConverterFixture
    {
        [Fact]
        public void ctor_sets_source_kind_to_unspecified()
        {
            var converter = new DateTimeConverter();
            Assert.Equal(DateTimeKind.Unspecified, converter.SourceKind);
        }

        [Fact]
        public void ctor_sets_target_kind_to_unspecified()
        {
            var converter = new DateTimeConverter();
            Assert.Equal(DateTimeKind.Unspecified, converter.TargetKind);
        }

        [Fact]
        public void ctor_taking_source_kind_and_target_kind_sets_source_kind_and_target_kind()
        {
            var converter = new DateTimeConverter(DateTimeKind.Local, DateTimeKind.Local);
            Assert.Equal(DateTimeKind.Local, converter.SourceKind);
            Assert.Equal(DateTimeKind.Local, converter.TargetKind);

            converter = new DateTimeConverter(DateTimeKind.Utc, DateTimeKind.Unspecified);
            Assert.Equal(DateTimeKind.Utc, converter.SourceKind);
            Assert.Equal(DateTimeKind.Unspecified, converter.TargetKind);
        }

        [Fact]
        public void source_kind_throws_if_value_is_invalid()
        {
            var converter = new DateTimeConverter();
            var ex = Assert.Throws<ArgumentException>(() => converter.SourceKind = (DateTimeKind)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void target_kind_throws_if_value_is_invalid()
        {
            var converter = new DateTimeConverter();
            var ex = Assert.Throws<ArgumentException>(() => converter.TargetKind = (DateTimeKind)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void conversion_mode_throws_if_value_is_invalid()
        {
            var converter = new DateTimeConverter();
            var ex = Assert.Throws<ArgumentException>(() => converter.ConversionMode = (DateTimeConversionMode)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'Kent.Boogaart.Converters.DateTimeConversionMode'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void convert_returns_unset_value_if_value_is_not_a_date_time()
        {
            var converter = new DateTimeConverter();
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert(123, null, null, null));
        }

        [Fact]
        public void convert_returns_same_value_if_target_kind_is_unspecified()
        {
            var converter = new DateTimeConverter
            {
                TargetKind = DateTimeKind.Unspecified
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = (DateTime)converter.Convert(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Utc, result.Kind);
            Assert.Equal(sourceDateTime, result);
        }

        [Fact]
        public void convert_converts_value_to_target_kind()
        {
            var converter = new DateTimeConverter
            {
                TargetKind = DateTimeKind.Local
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = (DateTime)converter.Convert(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Local, result.Kind);

            converter.TargetKind = DateTimeKind.Utc;
            result = (DateTime)converter.Convert(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Utc, result.Kind);
            Assert.Equal(sourceDateTime, result);

            sourceDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
            result = (DateTime)converter.Convert(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Utc, result.Kind);
            Assert.Equal(sourceDateTime, result);
        }

        [Fact]
        public void convert_does_not_adjust_value_if_conversion_mode_is_specify_kind_only()
        {
            var converter = new DateTimeConverter
            {
                TargetKind = DateTimeKind.Local,
                ConversionMode = DateTimeConversionMode.SpecifyKindOnly
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = (DateTime)converter.Convert(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Local, result.Kind);
            Assert.Equal(sourceDateTime, result);
        }

        [Fact]
        public void convert_applies_any_target_adjustment()
        {
            var converter = new DateTimeConverter
            {
                ConversionMode = DateTimeConversionMode.SpecifyKindOnly,
                TargetAdjustment = TimeSpan.FromMinutes(1)
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = converter.Convert(sourceDateTime, null, null, null);

            Assert.Equal(sourceDateTime.Add(TimeSpan.FromMinutes(1)), result);
        }

        [Fact]
        public void convert_back_returns_unset_value_if_value_is_not_a_date_time()
        {
            var converter = new DateTimeConverter();
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void convert_back_returns_same_value_if_source_kind_is_unspecified()
        {
            var converter = new DateTimeConverter
            {
                SourceKind = DateTimeKind.Unspecified
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = (DateTime)converter.ConvertBack(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Utc, result.Kind);
            Assert.Equal(sourceDateTime, result);
        }

        [Fact]
        public void convert_back_converts_value_to_source_kind()
        {
            var converter = new DateTimeConverter
            {
                SourceKind = DateTimeKind.Local
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = (DateTime)converter.ConvertBack(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Local, result.Kind);

            converter.SourceKind = DateTimeKind.Utc;
            result = (DateTime)converter.ConvertBack(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Utc, result.Kind);
            Assert.Equal(sourceDateTime, result);

            sourceDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
            result = (DateTime)converter.ConvertBack(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Utc, result.Kind);
            Assert.Equal(sourceDateTime, result);
        }

        [Fact]
        public void convert_back_does_not_adjust_value_if_conversion_mode_is_specify_kind_only()
        {
            var converter = new DateTimeConverter
            {
                SourceKind = DateTimeKind.Local,
                ConversionMode = DateTimeConversionMode.SpecifyKindOnly
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = (DateTime)converter.ConvertBack(sourceDateTime, null, null, null);

            Assert.Equal(DateTimeKind.Local, result.Kind);
            Assert.Equal(sourceDateTime, result);

        }

        [Fact]
        public void convert_back_applies_any_source_adjustment()
        {
            var converter = new DateTimeConverter
            {
                ConversionMode = DateTimeConversionMode.SpecifyKindOnly,
                SourceAdjustment = TimeSpan.FromMinutes(1)
            };
            var sourceDateTime = DateTime.UtcNow;
            var result = converter.ConvertBack(sourceDateTime, null, null, null);

            Assert.Equal(sourceDateTime.Add(TimeSpan.FromMinutes(1)), result);
        }
    }
}