using System;
using Kent.Boogaart.Converters.Markup;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    public sealed class DateTimeConverterExtensionTest : UnitTest
    {
        private DateTimeConverterExtension dateTimeConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.dateTimeConverterExtension = new DateTimeConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverterExtension.SourceKind);
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverterExtension.TargetKind);
            Assert.Equal(DateTimeConversionMode.DoConversion, this.dateTimeConverterExtension.ConversionMode);
            Assert.Equal(TimeSpan.Zero, this.dateTimeConverterExtension.SourceAdjustment);
            Assert.Equal(TimeSpan.Zero, this.dateTimeConverterExtension.TargetAdjustment);
        }

        [Fact]
        public void Constructor_Kinds_ShouldSetKinds()
        {
            this.dateTimeConverterExtension = new DateTimeConverterExtension(DateTimeKind.Utc, DateTimeKind.Local);
            Assert.Equal(DateTimeKind.Utc, this.dateTimeConverterExtension.SourceKind);
            Assert.Equal(DateTimeKind.Local, this.dateTimeConverterExtension.TargetKind);
        }

        [Fact]
        public void SourceKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.dateTimeConverterExtension.SourceKind = (DateTimeKind)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void SourceKind_ShouldGetAndSet()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverterExtension.SourceKind);
            this.dateTimeConverterExtension.SourceKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, this.dateTimeConverterExtension.SourceKind);
        }

        [Fact]
        public void TargetKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.dateTimeConverterExtension.TargetKind = (DateTimeKind)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.DateTimeKind'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void TargetKind_ShouldGetAndSet()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverterExtension.TargetKind);
            this.dateTimeConverterExtension.TargetKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, this.dateTimeConverterExtension.TargetKind);
        }

        [Fact]
        public void ConversionMode_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.dateTimeConverterExtension.ConversionMode = (DateTimeConversionMode)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'Kent.Boogaart.Converters.DateTimeConversionMode'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void ConversionMode_ShouldGetAndSet()
        {
            Assert.Equal(DateTimeConversionMode.DoConversion, this.dateTimeConverterExtension.ConversionMode);
            this.dateTimeConverterExtension.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeConversionMode.SpecifyKindOnly, this.dateTimeConverterExtension.ConversionMode);
        }

        [Fact]
        public void SourceAdjustment_ShouldGetAndSet()
        {
            Assert.Equal(TimeSpan.Zero, this.dateTimeConverterExtension.SourceAdjustment);
            this.dateTimeConverterExtension.SourceAdjustment = TimeSpan.FromDays(1);
            Assert.Equal(TimeSpan.FromDays(1), this.dateTimeConverterExtension.SourceAdjustment);
        }

        [Fact]
        public void TargetAdjustment_ShouldGetAndSet()
        {
            Assert.Equal(TimeSpan.Zero, this.dateTimeConverterExtension.TargetAdjustment);
            this.dateTimeConverterExtension.TargetAdjustment = TimeSpan.FromDays(1);
            Assert.Equal(TimeSpan.FromDays(1), this.dateTimeConverterExtension.TargetAdjustment);
        }

        [Fact]
        public void ProvideValue_ShouldYieldDateTimeConverterWithGivenInfo()
        {
            this.dateTimeConverterExtension.SourceKind = DateTimeKind.Utc;
            this.dateTimeConverterExtension.TargetKind = DateTimeKind.Local;
            this.dateTimeConverterExtension.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            this.dateTimeConverterExtension.SourceAdjustment = TimeSpan.FromSeconds(3);
            this.dateTimeConverterExtension.TargetAdjustment = TimeSpan.FromSeconds(-3);

            DateTimeConverter dateTimeConverter = this.dateTimeConverterExtension.ProvideValue(null) as DateTimeConverter;
            Assert.NotNull(dateTimeConverter);
            Assert.Equal(DateTimeKind.Utc, dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Local, dateTimeConverter.TargetKind);
            Assert.Equal(DateTimeConversionMode.SpecifyKindOnly, dateTimeConverter.ConversionMode);
            Assert.Equal(TimeSpan.FromSeconds(3), dateTimeConverter.SourceAdjustment);
            Assert.Equal(TimeSpan.FromSeconds(-3), dateTimeConverter.TargetAdjustment);
        }
    }
}
