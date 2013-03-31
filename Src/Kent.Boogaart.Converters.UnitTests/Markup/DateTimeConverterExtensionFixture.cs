namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    using Kent.Boogaart.Converters.Markup;
    using System;
    using Xunit;

    public sealed class DateTimeConverterExtensionFixture
    {
        [Fact]
        public void ctor_sets_source_kind_to_unspecified()
        {
            var converterExtension = new DateTimeConverterExtension();
            Assert.Equal(DateTimeKind.Unspecified, converterExtension.SourceKind);
        }

        [Fact]
        public void ctor_sets_target_kind_to_unspecified()
        {
            var converterExtension = new DateTimeConverterExtension();
            Assert.Equal(DateTimeKind.Unspecified, converterExtension.TargetKind);
        }

        [Fact]
        public void ctor_sets_conversion_mode_to_do_conversion()
        {
            var converterExtension = new DateTimeConverterExtension();
            Assert.Equal(DateTimeConversionMode.DoConversion, converterExtension.ConversionMode);
        }

        [Fact]
        public void ctor_sets_source_adjustment_to_zero()
        {
            var converterExtension = new DateTimeConverterExtension();
            Assert.Equal(TimeSpan.Zero, converterExtension.SourceAdjustment);
        }

        [Fact]
        public void ctor_sets_target_adjustment_to_zero()
        {
            var converterExtension = new DateTimeConverterExtension();
            Assert.Equal(TimeSpan.Zero, converterExtension.TargetAdjustment);
        }

        [Fact]
        public void ctor_that_takes_source_kind_and_target_kind_sets_source_kind_and_target_kind()
        {
            var converterExtension = new DateTimeConverterExtension(DateTimeKind.Utc, DateTimeKind.Local);
            Assert.Equal(DateTimeKind.Utc, converterExtension.SourceKind);
            Assert.Equal(DateTimeKind.Local, converterExtension.TargetKind);
        }

        [Fact]
        public void source_kind_throws_if_value_is_invalid()
        {
            var converterExtension = new DateTimeConverterExtension();
            var ex = Assert.Throws<ArgumentException>(() => converterExtension.SourceKind = (DateTimeKind)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void target_kind_throws_if_value_is_invalid()
        {
            var converterExtension = new DateTimeConverterExtension();
            var ex = Assert.Throws<ArgumentException>(() => converterExtension.TargetKind = (DateTimeKind)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void conversion_mode_throws_if_value_is_invalid()
        {
            var converterExtension = new DateTimeConverterExtension();
            var ex = Assert.Throws<ArgumentException>(() => converterExtension.ConversionMode = (DateTimeConversionMode)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'Kent.Boogaart.Converters.DateTimeConversionMode'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void provide_value_returns_appropriate_date_time_converter()
        {
            var converterExtension = new DateTimeConverterExtension
            {
                SourceKind = DateTimeKind.Utc,
                TargetKind = DateTimeKind.Local,
                ConversionMode = DateTimeConversionMode.SpecifyKindOnly,
                SourceAdjustment = TimeSpan.FromSeconds(3),
                TargetAdjustment = TimeSpan.FromSeconds(-3)
            };
            var dateTimeConverter = converterExtension.ProvideValue(null) as DateTimeConverter;

            Assert.NotNull(dateTimeConverter);
            Assert.Equal(DateTimeKind.Utc, dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Local, dateTimeConverter.TargetKind);
            Assert.Equal(DateTimeConversionMode.SpecifyKindOnly, dateTimeConverter.ConversionMode);
            Assert.Equal(TimeSpan.FromSeconds(3), dateTimeConverter.SourceAdjustment);
            Assert.Equal(TimeSpan.FromSeconds(-3), dateTimeConverter.TargetAdjustment);
        }
    }
}