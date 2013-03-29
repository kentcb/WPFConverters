using System;
using System.Windows.Markup;
using Kent.Boogaart.Converters.Markup;
using Moq;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    public sealed class TypeConverterExtensionTest : UnitTest
    {
        private TypeConverterExtension typeConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.typeConverterExtension = new TypeConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Null(this.typeConverterExtension.SourceType);
            Assert.Null(this.typeConverterExtension.SourceTypeName);
            Assert.Null(this.typeConverterExtension.TargetType);
            Assert.Null(this.typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void Constructor_Types_ShouldSetTypes()
        {
            this.typeConverterExtension = new TypeConverterExtension(typeof(int), typeof(string));
            Assert.Equal(typeof(int), this.typeConverterExtension.SourceType);
            Assert.Null(this.typeConverterExtension.SourceTypeName);
            Assert.Equal(typeof(string), this.typeConverterExtension.TargetType);
            Assert.Null(this.typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void Constructor_TypeNames_ShouldSetTypeNames()
        {
            this.typeConverterExtension = new TypeConverterExtension("sys:Int32", "sys:String");
            Assert.Null(this.typeConverterExtension.SourceType);
            Assert.Equal("sys:Int32", this.typeConverterExtension.SourceTypeName);
            Assert.Null(this.typeConverterExtension.TargetType);
            Assert.Equal("sys:String", this.typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void SourceType_ShouldGetAndSet()
        {
            Assert.Null(this.typeConverterExtension.SourceType);
            this.typeConverterExtension.SourceType = typeof(int);
            Assert.Equal(typeof(int), this.typeConverterExtension.SourceType);
        }

        [Fact]
        public void TargetType_ShouldGetAndSet()
        {
            Assert.Null(this.typeConverterExtension.TargetType);
            this.typeConverterExtension.TargetType = typeof(int);
            Assert.Equal(typeof(int), this.typeConverterExtension.TargetType);
        }

        [Fact]
        public void SourceTypeName_ShouldGetAndSet()
        {
            Assert.Null(this.typeConverterExtension.SourceTypeName);
            this.typeConverterExtension.SourceTypeName = "sys:Int32";
            Assert.Equal("sys:Int32", this.typeConverterExtension.SourceTypeName);
        }

        [Fact]
        public void TargetTypeName_ShouldGetAndSet()
        {
            Assert.Null(this.typeConverterExtension.TargetTypeName);
            this.typeConverterExtension.TargetTypeName = "sys:Int32";
            Assert.Equal("sys:Int32", this.typeConverterExtension.TargetTypeName);
        }

        [Fact]
        public void ProvideValue_ShouldProvideUninitializedTypeConverterIsNoInfoGiven()
        {
            TypeConverter typeConverter = this.typeConverterExtension.ProvideValue(null) as TypeConverter;
            Assert.Null(typeConverter.SourceType);
            Assert.Null(typeConverter.TargetType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithSourceType()
        {
            this.typeConverterExtension.SourceType = typeof(int);
            TypeConverter typeConverter = this.typeConverterExtension.ProvideValue(null) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.SourceType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithTargetType()
        {
            this.typeConverterExtension.TargetType = typeof(int);
            TypeConverter typeConverter = this.typeConverterExtension.ProvideValue(null) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.TargetType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithSourceTypeName()
        {
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            this.typeConverterExtension.SourceTypeName = "sys:Int32";
            var typeConverter = this.typeConverterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.SourceType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithTargetTypeName()
        {
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            this.typeConverterExtension.TargetTypeName = "sys:Int32";
            TypeConverter typeConverter = this.typeConverterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;
            Assert.Equal(typeof(int), typeConverter.TargetType);
        }

        [Fact]
        public void ProvideValue_ShouldWorkWithCombinationsOfTypeAndTypeName()
        {
            var xamlTypeResolverMock = new Mock<IXamlTypeResolver>();
            var serviceProviderMock = new Mock<IServiceProvider>();

            xamlTypeResolverMock.Setup(x => x.Resolve("sys:Int32")).Returns(typeof(int));
            serviceProviderMock.Setup(x => x.GetService(typeof(IXamlTypeResolver))).Returns(xamlTypeResolverMock.Object);

            this.typeConverterExtension.SourceType = typeof(string);
            this.typeConverterExtension.TargetTypeName = "sys:Int32";
            TypeConverter typeConverter = this.typeConverterExtension.ProvideValue(serviceProviderMock.Object) as TypeConverter;
            Assert.Equal(typeof(string), typeConverter.SourceType);
            Assert.Equal(typeof(int), typeConverter.TargetType);
        }
    }
}
