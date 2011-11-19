using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class TypeConverterTest : UnitTest
    {
        private TypeConverter typeConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.typeConverter = new TypeConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(this.typeConverter.SourceType);
            Assert.Null(this.typeConverter.TargetType);
        }

        [Fact]
        public void Constructor_SourceAndTargetType_ShouldSetSourceAndTargetType()
        {
            this.typeConverter = new TypeConverter(typeof(int), typeof(string));
            Assert.Same(typeof(int), this.typeConverter.SourceType);
            Assert.Same(typeof(string), this.typeConverter.TargetType);
            this.typeConverter = new TypeConverter(typeof(double), typeof(float));
            Assert.Same(typeof(double), this.typeConverter.SourceType);
            Assert.Same(typeof(float), this.typeConverter.TargetType);
        }

        [Fact]
        public void SourceType_ShouldGetAndSetSourceType()
        {
            Assert.Null(this.typeConverter.SourceType);
            this.typeConverter.SourceType = typeof(int);
            Assert.Same(typeof(int), this.typeConverter.SourceType);
            this.typeConverter.SourceType = typeof(double);
            Assert.Same(typeof(double), this.typeConverter.SourceType);
        }

        [Fact]
        public void TargetType_ShouldGetAndSetTargetType()
        {
            Assert.Null(this.typeConverter.TargetType);
            this.typeConverter.TargetType = typeof(int);
            Assert.Same(typeof(int), this.typeConverter.TargetType);
            this.typeConverter.TargetType = typeof(double);
            Assert.Same(typeof(double), this.typeConverter.TargetType);
        }

        [Fact]
        public void Convert_ShouldThrowIfTargetTypeIsNull()
        {
            this.typeConverter.SourceType = typeof(int);
            var ex = Assert.Throws<InvalidOperationException>(() => this.typeConverter.Convert("123", null, null, null));
            Assert.Equal("No TargetType has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_ShouldReturnNullIfConvertingNull()
        {
            this.typeConverter.SourceType = typeof(Type1);
            this.typeConverter.TargetType = typeof(Type2);
            Assert.Null(this.typeConverter.Convert(null, null, null, null));
        }

        [Fact]
        public void Convert_ShouldAttemptToConvertToTargetType()
        {
            this.typeConverter.SourceType = typeof(int);
            this.typeConverter.TargetType = typeof(string);
            Assert.Equal("123", this.typeConverter.Convert(123, null, null, null));

            this.typeConverter.SourceType = typeof(string);
            this.typeConverter.TargetType = typeof(int);
            Assert.Equal(123, this.typeConverter.Convert("123", null, null, null));
        }

        [Fact]
        public void Convert_ShouldUseSpecifiedCultureDuringConversion()
        {
            CultureInfo cultureInfo = new CultureInfo("de-DE");
            this.typeConverter.SourceType = typeof(double);
            this.typeConverter.TargetType = typeof(string);
            Assert.Equal("123,1", this.typeConverter.Convert(123.1, null, null, cultureInfo));

            this.typeConverter.SourceType = typeof(string);
            this.typeConverter.TargetType = typeof(double);
            Assert.Equal(123.1, this.typeConverter.Convert("123,1", null, null, cultureInfo));
        }

        [Fact]
        public void Convert_ShouldUseTypeConvertersIfNecessary()
        {
            this.typeConverter.SourceType = typeof(Type1);
            this.typeConverter.TargetType = typeof(Type2);
            Assert.True(this.typeConverter.Convert(new Type1(), null, null, null) is Type2);

            this.typeConverter.SourceType = typeof(Type2);
            this.typeConverter.TargetType = typeof(Type1);
            Assert.True(this.typeConverter.Convert(new Type2(), null, null, null) is Type1);
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfConversionIsNotSupported()
        {
            this.typeConverter.SourceType = typeof(TypeConverterTest);
            this.typeConverter.TargetType = typeof(TypeCode);
            Assert.Same(DependencyProperty.UnsetValue, this.typeConverter.Convert(new TypeConverterTest(), null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfConversionFails()
        {
            this.typeConverter.SourceType = typeof(string);
            this.typeConverter.TargetType = typeof(int);
            Assert.Same(DependencyProperty.UnsetValue, this.typeConverter.Convert(string.Empty, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldThrowIfTargetTypeIsNull()
        {
            this.typeConverter.TargetType = typeof(int);
            var ex = Assert.Throws<InvalidOperationException>(() => this.typeConverter.ConvertBack("123", null, null, null));
            Assert.Equal("No SourceType has been specified.", ex.Message);
        }

        [Fact]
        public void ConvertBack_ShouldReturnNullIfConvertingNull()
        {
            this.typeConverter.SourceType = typeof(Type2);
            this.typeConverter.TargetType = typeof(Type1);
            Assert.Null(this.typeConverter.ConvertBack(null, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldAttemptToConvertToSourceType()
        {
            this.typeConverter.SourceType = typeof(string);
            this.typeConverter.TargetType = typeof(int);
            Assert.Equal("123", this.typeConverter.ConvertBack(123, null, null, null));

            this.typeConverter.SourceType = typeof(int);
            this.typeConverter.TargetType = typeof(string);
            Assert.Equal(123, this.typeConverter.ConvertBack("123", null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldUseSpecifiedCultureDuringConversion()
        {
            CultureInfo cultureInfo = new CultureInfo("de-DE");
            this.typeConverter.SourceType = typeof(string);
            this.typeConverter.TargetType = typeof(double);
            Assert.Equal("123,1", this.typeConverter.ConvertBack(123.1, null, null, cultureInfo));

            this.typeConverter.SourceType = typeof(double);
            this.typeConverter.TargetType = typeof(string);
            Assert.Equal(123.1, this.typeConverter.ConvertBack("123,1", null, null, cultureInfo));
        }

        [Fact]
        public void ConvertBack_ShouldUseTypeConvertersIfNecessary()
        {
            this.typeConverter.SourceType = typeof(Type2);
            this.typeConverter.TargetType = typeof(Type1);
            Assert.True(this.typeConverter.ConvertBack(new Type1(), null, null, null) is Type2);

            this.typeConverter.SourceType = typeof(Type1);
            this.typeConverter.TargetType = typeof(Type2);
            Assert.True(this.typeConverter.ConvertBack(new Type2(), null, null, null) is Type1);
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfConversionIsNotSupported()
        {
            this.typeConverter.SourceType = typeof(TypeCode);
            this.typeConverter.TargetType = typeof(TypeConverterTest);
            Assert.Same(DependencyProperty.UnsetValue, this.typeConverter.ConvertBack(new TypeConverterTest(), null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfConversionFails()
        {
            this.typeConverter.SourceType = typeof(string);
            this.typeConverter.TargetType = typeof(int);
            Assert.Same(DependencyProperty.UnsetValue, this.typeConverter.Convert(string.Empty, null, null, null));
        }

        #region Supporting Types

        [TypeConverter(typeof(Type1TypeConverter))]
        private class Type1
        {
        }

        private class Type1TypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return (sourceType == typeof(Type2)) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return (destinationType == typeof(Type2)) || base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is Type2)
                {
                    return new Type1();
                }
                else
                {
                    return base.ConvertFrom(context, culture, value);
                }
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(Type2))
                {
                    return new Type2();
                }
                else
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }
            }
        }

        [TypeConverter(typeof(Type2TypeConverter))]
        private class Type2
        {
        }

        private class Type2TypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return (sourceType == typeof(Type1)) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return (destinationType == typeof(Type1)) || base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is Type1)
                {
                    return new Type2();
                }
                else
                {
                    return base.ConvertFrom(context, culture, value);
                }
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(Type1))
                {
                    return new Type1();
                }
                else
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }
            }
        }

        #endregion
    }
}
