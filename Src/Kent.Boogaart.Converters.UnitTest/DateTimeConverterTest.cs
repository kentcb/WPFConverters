using System;
using System.Windows;
using Xunit;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class DateTimeConverterTest : UnitTest
    {
        private DateTimeConverter _dateTimeConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _dateTimeConverter = new DateTimeConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
        }

        [Fact]
        public void Constructor_SourceAndTargetKind_ShouldSetSourceAndTargetKind()
        {
            _dateTimeConverter = new DateTimeConverter(DateTimeKind.Local, DateTimeKind.Local);
            Assert.Equal(DateTimeKind.Local, _dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Local, _dateTimeConverter.TargetKind);
            _dateTimeConverter = new DateTimeConverter(DateTimeKind.Utc, DateTimeKind.Unspecified);
            Assert.Equal(DateTimeKind.Utc, _dateTimeConverter.SourceKind);
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
        }

        [Fact]
        public void SourceKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _dateTimeConverter.SourceKind = (DateTimeKind) 100);
            Assert.Equal("'100' is not a valid value for property 'SourceKind'.", ex.Message);
        }

        [Fact]
        public void SourceKind_ShouldGetAndSetSourceKind()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverter.SourceKind);
            _dateTimeConverter.SourceKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, _dateTimeConverter.SourceKind);
            _dateTimeConverter.SourceKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, _dateTimeConverter.SourceKind);
        }

        [Fact]
        public void TargetKind_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _dateTimeConverter.TargetKind = (DateTimeKind) 100);
            Assert.Equal("'100' is not a valid value for property 'TargetKind'.", ex.Message);
        }

        [Fact]
        public void TargetKind_ShouldGetAndSetTargetKind()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
            _dateTimeConverter.TargetKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, _dateTimeConverter.TargetKind);
            _dateTimeConverter.TargetKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, _dateTimeConverter.TargetKind);
        }

        [Fact]
        public void ConversionMode_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _dateTimeConverter.ConversionMode = (DateTimeConversionMode) 100);
            Assert.Equal("'100' is not a valid value for property 'ConversionMode'.", ex.Message);
        }

        [Fact]
        public void ConversionMode_ShouldGetAndSetConversionMode()
        {
            Assert.Equal(DateTimeConversionMode.DoConversion, _dateTimeConverter.ConversionMode);
            _dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeConversionMode.SpecifyKindOnly, _dateTimeConverter.ConversionMode);
        }

        [Fact]
        public void SourceAdjustment_ShouldGetAndSetSourceAdjustment()
        {
            Assert.Equal(TimeSpan.Zero, _dateTimeConverter.SourceAdjustment);
            _dateTimeConverter.SourceAdjustment = TimeSpan.FromDays(2);
            Assert.Equal(TimeSpan.FromDays(2), _dateTimeConverter.SourceAdjustment);
        }

        [Fact]
        public void TargetAdjustment_ShouldGetAndSetTargetAdjustment()
        {
            Assert.Equal(TimeSpan.Zero, _dateTimeConverter.TargetAdjustment);
            _dateTimeConverter.TargetAdjustment = TimeSpan.FromDays(2);
            Assert.Equal(TimeSpan.FromDays(2), _dateTimeConverter.TargetAdjustment);
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfValueIsNotADateTime()
        {
            Assert.Same(DependencyProperty.UnsetValue, _dateTimeConverter.Convert(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _dateTimeConverter.Convert("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _dateTimeConverter.Convert(123, null, null, null));
        }

        [Fact]
        public void Convert_ShouldNotConvertIfTargetKindIsUnspecified()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverter.TargetKind);
            DateTime sourceDateTime = DateTime.UtcNow;
            Assert.Equal(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
            Assert.Equal(sourceDateTime, _dateTimeConverter.Convert(sourceDateTime, null, null, null));
        }

        [Fact]
        public void Convert_ShouldConvertDateTimesToTargetKind()
        {
            DateTime sourceDateTime = DateTime.UtcNow;
            _dateTimeConverter.TargetKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);

            _dateTimeConverter.TargetKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
            Assert.Equal(sourceDateTime, _dateTimeConverter.Convert(sourceDateTime, null, null, null));

            sourceDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
            Assert.Equal(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
        }

        [Fact]
        public void Convert_ShouldNotAdjustValueIsConversionModeIfSpecifyKindOnly()
        {
            DateTime sourceDateTime = DateTime.UtcNow;
            _dateTimeConverter.TargetKind = DateTimeKind.Local;
            _dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeKind.Local, ((DateTime) _dateTimeConverter.Convert(sourceDateTime, null, null, null)).Kind);
            Assert.Equal(sourceDateTime, _dateTimeConverter.Convert(sourceDateTime, null, null, null));
        }

        [Fact]
        public void Convert_ShouldApplyTargetAdjustment()
        {
            DateTime sourceDateTime = DateTime.UtcNow;
            _dateTimeConverter.TargetAdjustment = TimeSpan.FromMinutes(1);
            _dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;

            Assert.Equal(sourceDateTime.Add(TimeSpan.FromMinutes(1)), _dateTimeConverter.Convert(sourceDateTime, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfValueIsNotADateTime()
        {
            Assert.Same(DependencyProperty.UnsetValue, _dateTimeConverter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _dateTimeConverter.ConvertBack("abc", null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _dateTimeConverter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldNotConvertIfSourceKindIsUnspecified()
        {
            Assert.Equal(DateTimeKind.Unspecified, _dateTimeConverter.SourceKind);
            DateTime targetDateTime = DateTime.UtcNow;
            Assert.Equal(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
            Assert.Equal(targetDateTime, _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldConvertDateTimesToSourceKind()
        {
            DateTime targetDateTime = DateTime.UtcNow;
            _dateTimeConverter.SourceKind = DateTimeKind.Local;
            Assert.Equal(DateTimeKind.Local, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);

            _dateTimeConverter.SourceKind = DateTimeKind.Utc;
            Assert.Equal(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
            Assert.Equal(targetDateTime, _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));

            targetDateTime = new DateTime(Environment.TickCount, DateTimeKind.Unspecified);
            Assert.Equal(DateTimeKind.Utc, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
        }

        [Fact]
        public void ConvertBack_ShouldNotAdjustValueIsConversionModeIfSpecifyKindOnly()
        {
            DateTime targetDateTime = DateTime.UtcNow;
            _dateTimeConverter.SourceKind = DateTimeKind.Local;
            _dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;
            Assert.Equal(DateTimeKind.Local, ((DateTime) _dateTimeConverter.ConvertBack(targetDateTime, null, null, null)).Kind);
            Assert.Equal(targetDateTime, _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldApplySourceAdjustment()
        {
            DateTime targetDateTime = DateTime.UtcNow;
            _dateTimeConverter.SourceAdjustment = TimeSpan.FromMinutes(1);
            _dateTimeConverter.ConversionMode = DateTimeConversionMode.SpecifyKindOnly;

            Assert.Equal(targetDateTime.Add(TimeSpan.FromMinutes(1)), _dateTimeConverter.ConvertBack(targetDateTime, null, null, null));
        }
    }
}