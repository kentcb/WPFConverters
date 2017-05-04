namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    using Kent.Boogaart.Converters.Markup;
    using Moq;
    using System;
    using System.Windows.Markup;
    using Xunit;

    public sealed class TypeConverterExtensionFixture
    {
        [Fact]
        public void ctor_sets_source_type_to_null()
        {
            var converterExtension = new TypeConverterExtension();
            Assert.Null(converterExtension.SourceType);
        }

        [Fact]
        public void ctor_sets_source_type_name_to_null()
        {
            var converterExtension = new TypeConverterExtension();
            Assert.Null(converterExtension.SourceTypeName);
        }

        [Fact]
        public void ctor_sets_target_type_to_null()
        {
            var converterExtension = new TypeConverterExtension();
            Assert.Null(converterExtension.TargetType);
        }

        [Fact]
        public void ctor_sets_target_type_name_to_null()
        {
            var converterExtension = new TypeConverterExtension();
            Assert.Null(converterExtension.TargetTypeName);
        }

        [Fact]
        public void ctor_that_takes_source_type_and_target_type_sets_source_type_and_target_type()
        {
            var converterExtension = new TypeConverterExtension(typeof(int), typeof(string));
            Assert.Equal(typeof(int), converterExtension.SourceType);
            Assert.Null(converterExtension.SourceTypeName);
            Assert.Equal(typeof(string), converterExtension.TargetType);
            Assert.Null(converterExtension.TargetTypeName);
        }

        [Fact]
        public void ctor_that_takes_source_type_name_and_target_type_name_sets_source_type_name_and_target_type_name()
        {
            var converterExtension = new TypeConverterExtension("sys:Int32", "sys:String");
            Assert.Null(converterExtension.SourceType);
            Assert.Equal("sys:Int32", converterExtension.SourceTypeName);
            Assert.Null(converterExtension.TargetType);
            Assert.Equal("sys:String", converterExtension.TargetTypeName);
        }

        [Fact]
        public void provide_value_returns_default_type_converter_if_no_source_or_target_type_information_is_provided()
        {
            var converterExtension = new TypeConverterExtension();
            var typeConverter = converterExtension.ProvideValue(null) as TypeConverter;

            Assert.Null(typeConverter.SourceType);
            Assert.Null(typeConverter.TargetType);
        }

        [Fact]
        public void provide_value_returns_appropriate_type_converter_if_source_type_is_set()
        {
            var converterExtension = new TypeConverterExtension
            {
                SourceType = typeof(int)
            };
            var typeConverter = converterExtension.ProvideValue(null) as TypeConverter;

            Assert.Equal(typeof(int), typeConverter.SourceType);
        }

        [Fact]
        public void provide_value_returns_appropriate_type_converter_if_target_type_is_set()
        {
            var converterExtension = new TypeConverterExtension
            {
                TargetType = typeof(int)
            };
            var typeConverter = converterExtension.ProvideValue(null) as TypeConverter;

            Assert.Equal(typeof(int), typeConverter.TargetType);
        }

        [Fact]
        public void provide_value_returns_appropriate_type_converter_if_source_type_name_is_set()
        {
            var converterExtension = new TypeConverterExtension
            {
                SourceTypeName = "sys:Int32"
            };
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            var typeConverter = converterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;

            Assert.Equal(typeof(int), typeConverter.SourceType);
        }

        [Fact]
        public void provide_value_returns_appropriate_type_converter_if_target_type_name_is_set()
        {
            var converterExtension = new TypeConverterExtension
            {
                TargetTypeName = "sys:Int32"
            };
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            var typeConverter = converterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;

            Assert.Equal(typeof(int), typeConverter.TargetType);
        }

        [Fact]
        public void provide_value_returns_appropriate_type_converter_if_source_type_and_target_type_name_is_set()
        {
            var converterExtension = new TypeConverterExtension
            {
                SourceType = typeof(string),
                TargetTypeName = "sys:Int32"
            };
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();
            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            var typeConverter = converterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;

            Assert.Equal(typeof(string), typeConverter.SourceType);
            Assert.Equal(typeof(int), typeConverter.TargetType);
        }
    }
}
