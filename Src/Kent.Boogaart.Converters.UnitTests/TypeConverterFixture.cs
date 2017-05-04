namespace Kent.Boogaart.Converters.UnitTests
{
    using System;
    using System.Globalization;
    using System.Windows;
    using Xunit;

    public sealed class TypeConverterFixture
    {
        [Fact]
        public void ctor_sets_source_type_to_null()
        {
            var converter = new TypeConverter();
            Assert.Null(converter.SourceType);
        }

        [Fact]
        public void ctor_sets_target_type_to_null()
        {
            var converter = new TypeConverter();
            Assert.Null(converter.TargetType);
        }

        [Fact]
        public void ctor_that_takes_source_type_and_target_type_sets_source_type_and_target_type()
        {
            var converter = new TypeConverter(typeof(int), typeof(string));
            Assert.Same(typeof(int), converter.SourceType);
            Assert.Same(typeof(string), converter.TargetType);
        }

        [Fact]
        public void convert_throws_if_target_type_is_null()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(int)
            };
            var ex = Assert.Throws<InvalidOperationException>(() => converter.Convert("123", null, null, null));
            Assert.Equal("No TargetType has been specified.", ex.Message);
        }

        [Fact]
        public void convert_returns_null_if_value_is_null()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(int),
                TargetType = typeof(string)
            };
            Assert.Null(converter.Convert(null, null, null, null));
        }

        [Fact]
        public void convert_converts_the_value_to_the_target_type_where_possible()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(int),
                TargetType = typeof(string)
            };
            Assert.Equal("123", converter.Convert(123, null, null, null));
        }

        [Fact]
        public void convert_uses_specified_culture_during_conversion()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(double),
                TargetType = typeof(string)
            };
            var cultureInfo = new CultureInfo("de-DE");
            Assert.Equal("123,1", converter.Convert(123.1, null, null, cultureInfo));
        }

        [Fact]
        public void convert_uses_type_converters_if_necessary()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(Type1),
                TargetType = typeof(Type2)
            };
            Assert.True(converter.Convert(new Type1(), null, null, null) is Type2);

            converter = new TypeConverter
            {
                SourceType = typeof(Type2),
                TargetType = typeof(Type1)
            };
            Assert.True(converter.Convert(new Type2(), null, null, null) is Type1);
        }

        [Fact]
        public void convert_returns_unset_value_if_conversion_is_not_supported()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(TypeConverterFixture),
                TargetType = typeof(TypeCode)
            };
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert(new TypeConverterFixture(), null, null, null));
        }

        [Fact]
        public void convert_returns_unset_value_if_conversion_fails()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(string),
                TargetType = typeof(int)
            };
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert(string.Empty, null, null, null));
        }

        [Fact]
        public void convert_back_throws_if_source_type_is_null()
        {
            var converter = new TypeConverter
            {
                TargetType = typeof(int)
            };
            var ex = Assert.Throws<InvalidOperationException>(() => converter.ConvertBack("123", null, null, null));
            Assert.Equal("No SourceType has been specified.", ex.Message);
        }

        [Fact]
        public void convert_back_returns_null_if_value_is_null()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(string),
                TargetType = typeof(int)
            };
            Assert.Null(converter.ConvertBack(null, null, null, null));
        }

        [Fact]
        public void convert_back_converts_the_value_to_the_source_type_where_possible()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(string),
                TargetType = typeof(int)
            };
            Assert.Equal("123", converter.ConvertBack(123, null, null, null));
        }

        [Fact]
        public void convert_back_uses_specified_culture_during_conversion()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(string),
                TargetType = typeof(double)
            };
            var cultureInfo = new CultureInfo("de-DE");
            Assert.Equal("123,1", converter.ConvertBack(123.1, null, null, cultureInfo));
        }

        [Fact]
        public void convert_back_uses_type_converters_if_necessary()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(Type2),
                TargetType = typeof(Type1)
            };
            Assert.True(converter.ConvertBack(new Type1(), null, null, null) is Type2);

            converter = new TypeConverter
            {
                SourceType = typeof(Type1),
                TargetType = typeof(Type2)
            };
            Assert.True(converter.ConvertBack(new Type2(), null, null, null) is Type1);
        }

        [Fact]
        public void convert_back_returns_unset_value_if_conversion_is_not_supported()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(TypeCode),
                TargetType = typeof(TypeConverterFixture)
            };
            Assert.Same(DependencyProperty.UnsetValue, converter.ConvertBack(new TypeConverterFixture(), null, null, null));
        }

        [Fact]
        public void convert_back_returns_unset_value_if_conversion_fails()
        {
            var converter = new TypeConverter
            {
                SourceType = typeof(string),
                TargetType = typeof(int)
            };
            Assert.Same(DependencyProperty.UnsetValue, converter.Convert(string.Empty, null, null, null));
        }

        #region Supporting Types

        [System.ComponentModel.TypeConverter(typeof(Type1TypeConverter))]
        private class Type1
        {
        }

        private class Type1TypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                return (sourceType == typeof(Type2)) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
            {
                return (destinationType == typeof(Type2)) || base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, CultureInfo culture, object value)
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

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
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

        [System.ComponentModel.TypeConverter(typeof(Type2TypeConverter))]
        private class Type2
        {
        }

        private class Type2TypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType)
            {
                return (sourceType == typeof(Type1)) || base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, Type destinationType)
            {
                return (destinationType == typeof(Type1)) || base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, CultureInfo culture, object value)
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

            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
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
