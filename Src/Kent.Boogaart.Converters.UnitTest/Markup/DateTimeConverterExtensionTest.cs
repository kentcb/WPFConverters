using System;
using Xunit;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
    public sealed class DateTimeConverterExtensionTest : UnitTest
    {
        private DateTimeConverterExtension _dateTimeConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _dateTimeConverterExtension = new DateTimeConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverterExtension.SourceKind);
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverterExtension.TargetKind);
            Assert.Equal(DateTimeConversionMode.DoConversion, _dateTimeConverterExtension.ConversionMode);
            Assert.Equal(TimeSpan.Zero, _dateTimeConverterExtension.SourceAdjustment);
            Assert.Equal(TimeSpan.Zero, _dateTimeConverterExtension.TargetAdjustment);
        }

        [Fact]
        public void Constructor_Kinds_ShouldSetKinds()
        {
            _dateTimeConverterExtension = new DateTimeConverterExtension(DateTimeKind.Utc, DateTimeKind.Local);
            Assert.Equal(DateTimeKind.Utc, _dateTimeConverterExtension.SourceKind);
            Assert.Equal(DateTimeKind.Local, _dateTimeConverterExtension.TargetKind);
        }

        [Fact]
        public void SourceKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _dateTimeConverterExtension.SourceKind = (DateTimeKind) 100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void SourceKind_ShouldGetAndSet()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverterExtension.SourceKind);
            _dateTimeConverterExtension.SourceKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, _dateTimeConverterExtension.SourceKind);
        }

        [Fact]
        public void TargetKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _dateTimeConverterExtension.TargetKind = (DateTimeKind)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void TargetKind_ShouldGetAndSet()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverterExtension.TargetKind);
            _dateTimeConverterExtension.TargetKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, _dateTimeConverterExtension.TargetKind);
        }

        [Fact]
        public void ConversionMode_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _dateTimeConverterExtension.ConversionMode = (DateTimeConversionMode) 100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'Kent.Boogaart.Converters.DateTimeConversionMode'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void ConversionMode_ShouldGetAndSet()
        {
            Assert.Equal(DateTimeConversionMode.DoConversion, _dateTimeConverterExtension.ConversionMode);
            _dateTimeConverterExtension.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeConversionMode.SpecifyKindOnly, _dateTimeConverterExtension.ConversionMode);
        }

        [Fact]
        public void SourceAdjustment_ShouldGetAndSet()
        {
            Assert.Equal(TimeSpan.Zero, _dateTimeConverterExtension.SourceAdjustment);
            _dateTimeConverterExtension.SourceAdjustment = TimeSpan.FromDays(1);
            Assert.Equal(TimeSpan.FromDays(1), _dateTimeConverterExtension.SourceAdjustment);
        }

        [Fact]
        public void TargetAdjustment_ShouldGetAndSet()
        {
            Assert.Equal(TimeSpan.Zero, _dateTimeConverterExtension.TargetAdjustment);
            _dateTimeConverterExtension.TargetAdjustment = TimeSpan.FromDays(1);
            Assert.Equal(TimeSpan.FromDays(1), _dateTimeConverterExtension.TargetAdjustment);
        }

        [Fact]
        public void ProvideValue_ShouldYieldDateTimeConverterWithGivenInfo()
        {
            _dateTimeConverterExtension.SourceKind = DateTimeKind.Utc;
            _dateTimeConverterExtension.TargetKind = DateTimeKind.Local;
            _dateTimeConverterExtension.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            _dateTimeConverterExtension.SourceAdjustment = TimeSpan.FromSeconds(3);
            _dateTimeConverterExtension.TargetAdjustment = TimeSpan.FromSeconds(-3);

            DateTimeConverter dateTimeConverter = _dateTimeConverterExtension.ProvideValue(null) as DateTimeConverter;
            Assert.NotNull(dateTimeConverter);
            Assert.Equal(DateTimeKind.Utc, dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Local, dateTimeConverter.TargetKind);
            Assert.Equal(DateTimeConversionMode.SpecifyKindOnly, dateTimeConverter.ConversionMode);
            Assert.Equal(TimeSpan.FromSeconds(3), dateTimeConverter.SourceAdjustment);
            Assert.Equal(TimeSpan.FromSeconds(-3), dateTimeConverter.TargetAdjustment);
        }
    }
}
