using System;
using System.Windows;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class DateTimeConverterTest : UnitTest
    {
        private DateTimeConverter dateTimeConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.dateTimeConverter = new DateTimeConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverter.TargetKind);
        }

        [Fact]
        public void Constructor_SourceAndTargetKind_ShouldSetSourceAndTargetKind()
        {
            this.dateTimeConverter = new DateTimeConverter(DateTimeKind.Local, DateTimeKind.Local);
            Assert.Equal(DateTimeKind.Local, this.dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Local, this.dateTimeConverter.TargetKind);
            this.dateTimeConverter = new DateTimeConverter(DateTimeKind.Utc, DateTimeKind.Unspecified);
            Assert.Equal(DateTimeKind.Utc, this.dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverter.TargetKind);
        }

        [Fact]
        public void SourceKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.dateTimeConverter.SourceKind = (DateTimeKind)100);
            Assert.Equal("'100' is not a valid value for property 'SourceKind'.", ex.Message);
        }

        [Fact]
        public void SourceKind_ShouldGetAndSetSourceKind()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverter.SourceKind);
            this.dateTimeConverter.SourceKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, this.dateTimeConverter.SourceKind);
            this.dateTimeConverter.SourceKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, this.dateTimeConverter.SourceKind);
        }

        [Fact]
        public void TargetKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.dateTimeConverter.TargetKind = (DateTimeKind)100);
            Assert.Equal("'100' is not a valid value for property 'TargetKind'.", ex.Message);
        }

        [Fact]
        public void TargetKind_ShouldGetAndSetTargetKind()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverter.TargetKind);
            this.dateTimeConverter.TargetKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, this.dateTimeConverter.TargetKind);
            this.dateTimeConverter.TargetKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, this.dateTimeConverter.TargetKind);
        }

        [Fact]
        public void ConversionMode_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.dateTimeConverter.ConversionMode = (DateTimeConversionMode)100);
            Assert.Equal("'100' is not a valid value for property 'ConversionMode'.", ex.Message);
        }

        [Fact]
        public void ConversionMode_ShouldGetAndSetConversionMode()
        {
            Assert.Equal(DateTimeConversionMode.DoConversion, this.dateTimeConverter.ConversionMode);
            this.dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeConversionMode.SpecifyKindOnly, this.dateTimeConverter.ConversionMode);
        }

        [Fact]
        public void SourceAdjustment_ShouldGetAndSetSourceAdjustment()
        {
            Assert.Equal(TimeSpan.Zero, this.dateTimeConverter.SourceAdjustment);
            this.dateTimeConverter.SourceAdjustment = TimeSpan.FromDays(2);
            Assert.Equal(TimeSpan.FromDays(2), this.dateTimeConverter.SourceAdjustment);
        }

        [Fact]
        public void TargetAdjustment_ShouldGetAndSetTargetAdjustment()
        {
            Assert.Equal(TimeSpan.Zero, this.dateTimeConverter.TargetAdjustment);
            this.dateTimeConverter.TargetAdjustment = TimeSpan.FromDays(2);
            Assert.Equal(TimeSpan.FromDays(2), this.dateTimeConverter.TargetAdjustment);
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfValueIsNotADateTime()
        {
            Assert.Same(DependencyProperty.UnsetValue, this.dateTimeConverter.Convert(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.dateTimeConverter.Convert("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.dateTimeConverter.Convert(123, null, null, null));
        }

        [Fact]
        public void Convert_ShouldNotConvertIfTargetKindIsUnspecified()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverter.TargetKind);
            DateTime sourceDateTime = DateTime.UtcNow;
            Assert.Equal(DateTimeKind.Utc, ((DateTime)this.dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
            Assert.Equal(sourceDateTime, this.dateTimeConverter.Convert(sourceDateTime, null, null, null));
        }

        [Fact]
        public void Convert_ShouldConvertDateTimesToTargetKind()
        {
            DateTime sourceDateTime = DateTime.UtcNow;
            this.dateTimeConverter.TargetKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, ((DateTime)this.dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);

            this.dateTimeConverter.TargetKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, ((DateTime)this.dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
            Assert.Equal(sourceDateTime, this.dateTimeConverter.Convert(sourceDateTime, null, null, null));

            sourceDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
            Assert.Equal(DateTimeKind.Utc, ((DateTime)this.dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
        }

        [Fact]
        public void Convert_ShouldNotAdjustValueIsConversionModeIfSpecifyKindOnly()
        {
            DateTime sourceDateTime = DateTime.UtcNow;
            this.dateTimeConverter.TargetKind = DateTimeKind.Local;
            this.dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeKind.Local, ((DateTime)this.dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
            Assert.Equal(sourceDateTime, this.dateTimeConverter.Convert(sourceDateTime, null, null, null));
        }

        [Fact]
        public void Convert_ShouldApplyTargetAdjustment()
        {
            DateTime sourceDateTime = DateTime.UtcNow;
            this.dateTimeConverter.TargetAdjustment = TimeSpan.FromMinutes(1);
            this.dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;

            Assert.Equal(sourceDateTime.Add(TimeSpan.FromMinutes(1)), this.dateTimeConverter.Convert(sourceDateTime, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfValueIsNotADateTime()
        {
            Assert.Same(DependencyProperty.UnsetValue, this.dateTimeConverter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.dateTimeConverter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, this.dateTimeConverter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldNotConvertIfSourceKindIsUnspecified()
        {
            Assert.Equal(DateTimeKind.Unspecified, this.dateTimeConverter.SourceKind);
            DateTime targetDateTime = DateTime.UtcNow;
            Assert.Equal(DateTimeKind.Utc, ((DateTime)this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
            Assert.Equal(targetDateTime, this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldConvertDateTimesToSourceKind()
        {
            DateTime targetDateTime = DateTime.UtcNow;
            this.dateTimeConverter.SourceKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, ((DateTime)this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);

            this.dateTimeConverter.SourceKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, ((DateTime)this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
            Assert.Equal(targetDateTime, this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null));

            targetDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
            Assert.Equal(DateTimeKind.Utc, ((DateTime)this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
        }

        [Fact]
        public void ConvertBack_ShouldNotAdjustValueIsConversionModeIfSpecifyKindOnly()
        {
            DateTime targetDateTime = DateTime.UtcNow;
            this.dateTimeConverter.SourceKind = DateTimeKind.Local;
            this.dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeKind.Local, ((DateTime)this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
            Assert.Equal(targetDateTime, this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldApplySourceAdjustment()
        {
            DateTime targetDateTime = DateTime.UtcNow;
            this.dateTimeConverter.SourceAdjustment = TimeSpan.FromMinutes(1);
            this.dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;

            Assert.Equal(targetDateTime.Add(TimeSpan.FromMinutes(1)), this.dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
        }
    }
}