using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using NUnit.Framework;
using Rhino.Mocks;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
	[TestFixture]
	public sealed class TypeConverterExtensionTest : UnitTest
	{
		private TypeConverterExtension _typeConverterExtension;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_typeConverterExtension = new TypeConverterExtension();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNull(_typeConverterExtension.SourceType);
			Assert.IsNull(_typeConverterExtension.SourceTypeName);
			Assert.IsNull(_typeConverterExtension.TargetType);
			Assert.IsNull(_typeConverterExtension.TargetTypeName);
		}

		[Test]
		public void Constructor_Types_ShouldSetTypes()
		{
			_typeConverterExtension = new TypeConverterExtension(typeof(int), typeof(string));
			Assert.AreEqual(typeof(int), _typeConverterExtension.SourceType);
			Assert.IsNull(_typeConverterExtension.SourceTypeName);
			Assert.AreEqual(typeof(string), _typeConverterExtension.TargetType);
			Assert.IsNull(_typeConverterExtension.TargetTypeName);
		}

		[Test]
		public void Constructor_TypeNames_ShouldSetTypeNames()
		{
			_typeConverterExtension = new TypeConverterExtension("sys:Int32", "sys:String");
			Assert.IsNull(_typeConverterExtension.SourceType);
			Assert.AreEqual("sys:Int32", _typeConverterExtension.SourceTypeName);
			Assert.IsNull(_typeConverterExtension.TargetType);
			Assert.AreEqual("sys:String", _typeConverterExtension.TargetTypeName);
		}

		[Test]
		public void SourceType_ShouldGetAndSet()
		{
			Assert.IsNull(_typeConverterExtension.SourceType);
			_typeConverterExtension.SourceType = typeof(int);
			Assert.AreEqual(typeof(int), _typeConverterExtension.SourceType);
		}

		[Test]
		public void TargetType_ShouldGetAndSet()
		{
			Assert.IsNull(_typeConverterExtension.TargetType);
			_typeConverterExtension.TargetType = typeof(int);
			Assert.AreEqual(typeof(int), _typeConverterExtension.TargetType);
		}

		[Test]
		public void SourceTypeName_ShouldGetAndSet()
		{
			Assert.IsNull(_typeConverterExtension.SourceTypeName);
			_typeConverterExtension.SourceTypeName = "sys:Int32";
			Assert.AreEqual("sys:Int32", _typeConverterExtension.SourceTypeName);
		}

		[Test]
		public void TargetTypeName_ShouldGetAndSet()
		{
			Assert.IsNull(_typeConverterExtension.TargetTypeName);
			_typeConverterExtension.TargetTypeName = "sys:Int32";
			Assert.AreEqual("sys:Int32", _typeConverterExtension.TargetTypeName);
		}

		[Test]
		public void ProvideValue_ShouldProvideUninitializedTypeConverterIsNoInfoGiven()
		{
			TypeConverter typeConverter = _typeConverterExtension.ProvideValue(null) as TypeConverter;
			Assert.IsNull(typeConverter.SourceType);
			Assert.IsNull(typeConverter.TargetType);
		}

		[Test]
		public void ProvideValue_ShouldWorkWithSourceType()
		{
			_typeConverterExtension.SourceType = typeof(int);
			TypeConverter typeConverter = _typeConverterExtension.ProvideValue(null) as TypeConverter;
			Assert.AreEqual(typeof(int), typeConverter.SourceType);
		}

		[Test]
		public void ProvideValue_ShouldWorkWithTargetType()
		{
			_typeConverterExtension.TargetType = typeof(int);
			TypeConverter typeConverter = _typeConverterExtension.ProvideValue(null) as TypeConverter;
			Assert.AreEqual(typeof(int), typeConverter.TargetType);
		}

		[Test]
		public void ProvideValue_ShouldWorkWithSourceTypeName()
		{
			IXamlTypeResolver xamlTypeResolver = Mocks.DynamicMock<IXamlTypeResolver>();
			Expect.Call(xamlTypeResolver.Resolve("sys:Int32")).Repeat.Any().Return(typeof(int));
			IServiceProvider serviceProvider = Mocks.DynamicMock<IServiceProvider>();
			Expect.Call(serviceProvider.GetService(typeof(IXamlTypeResolver))).Repeat.Any().Return(xamlTypeResolver);
			Mocks.ReplayAll();

			_typeConverterExtension.SourceTypeName = "sys:Int32";
			TypeConverter typeConverter = _typeConverterExtension.ProvideValue(serviceProvider) as TypeConverter;
			Assert.AreEqual(typeof(int), typeConverter.SourceType);
		}

		[Test]
		public void ProvideValue_ShouldWorkWithTargetTypeName()
		{
			IXamlTypeResolver xamlTypeResolver = Mocks.DynamicMock<IXamlTypeResolver>();
			Expect.Call(xamlTypeResolver.Resolve("sys:Int32")).Repeat.Any().Return(typeof(int));
			IServiceProvider serviceProvider = Mocks.DynamicMock<IServiceProvider>();
			Expect.Call(serviceProvider.GetService(typeof(IXamlTypeResolver))).Repeat.Any().Return(xamlTypeResolver);
			Mocks.ReplayAll();

			_typeConverterExtension.TargetTypeName = "sys:Int32";
			TypeConverter typeConverter = _typeConverterExtension.ProvideValue(serviceProvider) as TypeConverter;
			Assert.AreEqual(typeof(int), typeConverter.TargetType);
		}

		[Test]
		public void ProvideValue_ShouldWorkWithCombinationsOfTypeAndTypeName()
		{
			IXamlTypeResolver xamlTypeResolver = Mocks.DynamicMock<IXamlTypeResolver>();
			Expect.Call(xamlTypeResolver.Resolve("sys:Int32")).Repeat.Any().Return(typeof(int));
			IServiceProvider serviceProvider = Mocks.DynamicMock<IServiceProvider>();
			Expect.Call(serviceProvider.GetService(typeof(IXamlTypeResolver))).Repeat.Any().Return(xamlTypeResolver);
			Mocks.ReplayAll();

			_typeConverterExtension.SourceType = typeof(string);
			_typeConverterExtension.TargetTypeName = "sys:Int32";
			TypeConverter typeConverter = _typeConverterExtension.ProvideValue(serviceProvider) as TypeConverter;
			Assert.AreEqual(typeof(string), typeConverter.SourceType);
			Assert.AreEqual(typeof(int), typeConverter.TargetType);
		}
	}
}
