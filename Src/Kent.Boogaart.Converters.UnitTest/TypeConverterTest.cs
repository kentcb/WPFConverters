using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using Xunit;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class TypeConverterTest : UnitTest
    {
        private TypeConverter _typeConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _typeConverter = new TypeConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(_typeConverter.SourceType);
            Assert.Null(_typeConverter.TargetType);
        }

        [Fact]
        public void Constructor_SourceAndTargetType_ShouldSetSourceAndTargetType()
        {
            _typeConverter = new TypeConverter(typeof(int), typeof(string));
            Assert.Same(typeof(int), _typeConverter.SourceType);
            Assert.Same(typeof(string), _typeConverter.TargetType);
            _typeConverter = new TypeConverter(typeof(double), typeof(float));
            Assert.Same(typeof(double), _typeConverter.SourceType);
            Assert.Same(typeof(float), _typeConverter.TargetType);
        }

        [Fact]
        public void SourceType_ShouldGetAndSetSourceType()
        {
            Assert.Null(_typeConverter.SourceType);
            _typeConverter.SourceType = typeof(int);
            Assert.Same(typeof(int), _typeConverter.SourceType);
            _typeConverter.SourceType = typeof(double);
            Assert.Same(typeof(double), _typeConverter.SourceType);
        }

        [Fact]
        public void TargetType_ShouldGetAndSetTargetType()
        {
            Assert.Null(_typeConverter.TargetType);
            _typeConverter.TargetType = typeof(int);
            Assert.Same(typeof(int), _typeConverter.TargetType);
            _typeConverter.TargetType = typeof(double);
            Assert.Same(typeof(double), _typeConverter.TargetType);
        }

        [Fact]
        public void Convert_ShouldThrowIfTargetTypeIsNull()
        {
            _typeConverter.SourceType = typeof(int);
            var ex = Assert.Throws<InvalidOperationException>(() => _typeConverter.Convert("123", null, null, null));
            Assert.Equal("No TargetType has been specified.", ex.Message);
        }

        [Fact]
        public void Convert_ShouldReturnNullIfConvertingNull()
        {
            _typeConverter.SourceType = typeof(Type1);
            _typeConverter.TargetType = typeof(Type2);
            Assert.Null(_typeConverter.Convert(null, null, null, null));
        }

        [Fact]
        public void Convert_ShouldAttemptToConvertToTargetType()
        {
            _typeConverter.SourceType = typeof(int);
            _typeConverter.TargetType = typeof(string);
            Assert.Equal("123", _typeConverter.Convert(123, null, null, null));

            _typeConverter.SourceType = typeof(string);
            _typeConverter.TargetType = typeof(int);
            Assert.Equal(123, _typeConverter.Convert("123", null, null, null));
        }

        [Fact]
        public void Convert_ShouldUseSpecifiedCultureDuringConversion()
        {
            CultureInfo cultureInfo = new CultureInfo("de-DE");
            _typeConverter.SourceType = typeof(double);
            _typeConverter.TargetType = typeof(string);
            Assert.Equal("123,1", _typeConverter.Convert(123.1, null, null, cultureInfo));

            _typeConverter.SourceType = typeof(string);
            _typeConverter.TargetType = typeof(double);
            Assert.Equal(123.1, _typeConverter.Convert("123,1", null, null, cultureInfo));
        }

        [Fact]
        public void Convert_ShouldUseTypeConvertersIfNecessary()
        {
            _typeConverter.SourceType = typeof(Type1);
            _typeConverter.TargetType = typeof(Type2);
            Assert.True(_typeConverter.Convert(new Type1(), null, null, null) is Type2);

            _typeConverter.SourceType = typeof(Type2);
            _typeConverter.TargetType = typeof(Type1);
            Assert.True(_typeConverter.Convert(new Type2(), null, null, null) is Type1);
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfConversionIsNotSupported()
        {
            _typeConverter.SourceType = typeof(TypeConverterTest);
            _typeConverter.TargetType = typeof(TypeCode);
            Assert.Same(DependencyProperty.UnsetValue, _typeConverter.Convert(new TypeConverterTest(), null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnUnsetValueIfConversionFails()
        {
            _typeConverter.SourceType = typeof(string);
            _typeConverter.TargetType = typeof(int);
            Assert.Same(DependencyProperty.UnsetValue, _typeConverter.Convert("", null, null,null));
        }

        [Fact]
        public void ConvertBack_ShouldThrowIfTargetTypeIsNull()
        {
            _typeConverter.TargetType = typeof(int);
            var ex = Assert.Throws<InvalidOperationException>(() => _typeConverter.ConvertBack("123", null, null, null));
            Assert.Equal("No SourceType has been specified.", ex.Message);
        }

        [Fact]
        public void ConvertBack_ShouldReturnNullIfConvertingNull()
        {
            _typeConverter.SourceType = typeof(Type2);
            _typeConverter.TargetType = typeof(Type1);
            Assert.Null(_typeConverter.ConvertBack(null, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldAttemptToConvertToSourceType()
        {
            _typeConverter.SourceType = typeof(string);
            _typeConverter.TargetType = typeof(int);
            Assert.Equal("123", _typeConverter.ConvertBack(123, null, null, null));

            _typeConverter.SourceType = typeof(int);
            _typeConverter.TargetType = typeof(string);
            Assert.Equal(123, _typeConverter.ConvertBack("123", null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldUseSpecifiedCultureDuringConversion()
        {
            CultureInfo cultureInfo = new CultureInfo("de-DE");
            _typeConverter.SourceType = typeof(string);
            _typeConverter.TargetType = typeof(double);
            Assert.Equal("123,1", _typeConverter.ConvertBack(123.1, null, null, cultureInfo));

            _typeConverter.SourceType = typeof(double);
            _typeConverter.TargetType = typeof(string);
            Assert.Equal(123.1, _typeConverter.ConvertBack("123,1", null, null, cultureInfo));
        }

        [Fact]
        public void ConvertBack_ShouldUseTypeConvertersIfNecessary()
        {
            _typeConverter.SourceType = typeof(Type2);
            _typeConverter.TargetType = typeof(Type1);
            Assert.True(_typeConverter.ConvertBack(new Type1(), null, null, null) is Type2);

            _typeConverter.SourceType = typeof(Type1);
            _typeConverter.TargetType = typeof(Type2);
            Assert.True(_typeConverter.ConvertBack(new Type2(), null, null, null) is Type1);
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfConversionIsNotSupported()
        {
            _typeConverter.SourceType = typeof(TypeCode);
            _typeConverter.TargetType = typeof(TypeConverterTest);
            Assert.Same(DependencyProperty.UnsetValue, _typeConverter.ConvertBack(new TypeConverterTest(), null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValueIfConversionFails()
        {
            _typeConverter.SourceType = typeof(string);
            _typeConverter.TargetType = typeof(int);
            Assert.Same(DependencyProperty.UnsetValue, _typeConverter.Convert("", null, null, null));
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
