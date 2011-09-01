using System;
using System.Windows.Markup;
using Kent.Boogaart.Converters.Markup;
using Moq;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
    public sealed class TypeConverterExtensionTest : UnitTest
    {
        private TypeConverterExtension _typeConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _typeConverterExtension = new TypeConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(_typeConverterExtension.SourceType);
            Assert.Null(_typeConverterExtension.SourceTypeName);
            Assert.Null(_typeConverterExtension.TargetType);
            Assert.Null(_typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void Constructor_Types_ShouldSetTypes()
        {
            _typeConverterExtension = new TypeConverterExtension(typeof(int), typeof(string));
            Assert.Equal(typeof(int), _typeConverterExtension.SourceType);
            Assert.Null(_typeConverterExtension.SourceTypeName);
            Assert.Equal(typeof(string), _typeConverterExtension.TargetType);
            Assert.Null(_typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void Constructor_TypeNames_ShouldSetTypeNames()
        {
            _typeConverterExtension = new TypeConverterExtension("sys:Int32", "sys:String");
            Assert.Null(_typeConverterExtension.SourceType);
            Assert.Equal("sys:Int32", _typeConverterExtension.SourceTypeName);
            Assert.Null(_typeConverterExtension.TargetType);
            Assert.Equal("sys:String", _typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void SourceType_ShouldGetAndSet()
        {
            Assert.Null(_typeConverterExtension.SourceType);
            _typeConverterExtension.SourceType = typeof(int);
            Assert.Equal(typeof(int), _typeConverterExtension.SourceType);
        }

        [Fact]
        public void TargetType_ShouldGetAndSet()
        {
            Assert.Null(_typeConverterExtension.TargetType);
            _typeConverterExtension.TargetType = typeof(int);
            Assert.Equal(typeof(int), _typeConverterExtension.TargetType);
        }

        [Fact]
        public void SourceTypeName_ShouldGetAndSet()
        {
            Assert.Null(_typeConverterExtension.SourceTypeName);
            _typeConverterExtension.SourceTypeName = "sys:Int32";
            Assert.Equal("sys:Int32", _typeConverterExtension.SourceTypeName);
        }

        [Fact]
        public void TargetTypeName_ShouldGetAndSet()
        {
            Assert.Null(_typeConverterExtension.TargetTypeName);
            _typeConverterExtension.TargetTypeName = "sys:Int32";
            Assert.Equal("sys:Int32", _typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void ProvideValue_ShouldProvideUninitializedTypeConverterIsNoInfoGiven()
        {
            TypeConverter typeConverter = _typeConverterExtension.ProvideValue(null) as TypeConverter;
            Assert.Null(typeConverter.SourceType);
            Assert.Null(typeConverter.TargetType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithSourceType()
        {
            _typeConverterExtension.SourceType = typeof(int);
            TypeConverter typeConverter = _typeConverterExtension.ProvideValue(null) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.SourceType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithTargetType()
        {
            _typeConverterExtension.TargetType = typeof(int);
            TypeConverter typeConverter = _typeConverterExtension.ProvideValue(null) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.TargetType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithSourceTypeName()
        {
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            _typeConverterExtension.SourceTypeName = "sys:Int32";
            var typeConverter = _typeConverterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.SourceType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithTargetTypeName()
        {
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            _typeConverterExtension.TargetTypeName = "sys:Int32";
            TypeConverter typeConverter = _typeConverterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.TargetType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithCombinationsOfTypeAndTypeName()
        {
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            _typeConverterExtension.SourceType = typeof(string);
            _typeConverterExtension.TargetTypeName = "sys:Int32";
            TypeConverter typeConverter = _typeConverterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;
            Assert.Equal(typeof(string), typeConverter.SourceType);
            Assert.Equal(typeof(int), typeConverter.TargetType);
        }
    }
}
