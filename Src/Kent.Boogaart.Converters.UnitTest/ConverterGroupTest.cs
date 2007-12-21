using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using NUnit.Framework;
using Rhino.Mocks;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class ConverterGroupTest : UnitTest
	{
		private ConverterGroup _converterGroup;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_converterGroup = new ConverterGroup();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNotNull(_converterGroup.Converters);
			Assert.AreEqual(0, _converterGroup.Converters.Count);
		}

		[Test]
		public void Converters_ShouldAllowManipulationOfConverters()
		{
			IValueConverter converter1 = Mocks.DynamicMock<IValueConverter>();
			IValueConverter converter2 = Mocks.DynamicMock<IValueConverter>();
			IValueConverter converter3 = Mocks.DynamicMock<IValueConverter>();
			Mocks.ReplayAll();

			_converterGroup.Converters.Add(converter1);
			_converterGroup.Converters.Add(converter2);
			_converterGroup.Converters.Add(converter3);

			Assert.AreEqual(3, _converterGroup.Converters.Count);
			Assert.IsTrue(_converterGroup.Converters.Contains(converter1));
			Assert.IsTrue(_converterGroup.Converters.Contains(converter2));
			Assert.IsTrue(_converterGroup.Converters.Contains(converter3));
		}

		[Test]
		public void Convert_ShouldReturnUnsetValueIfNoConverters()
		{
			Assert.AreEqual(DependencyProperty.UnsetValue, _converterGroup.Convert("abc", null, null, null));
		}

		[Test]
		public void Convert_ShouldPassParametersIntoConverters()
		{
			object value = "abc";
			Type targetType = typeof(int);
			object parameter = "parameter";
			CultureInfo cultureInfo = new CultureInfo("de-DE");

			IValueConverter converter = Mocks.DynamicMock<IValueConverter>();
			Expect.Call(converter.Convert(value, targetType, parameter, cultureInfo)).Return("converted value");
			Mocks.ReplayAll();

			_converterGroup.Converters.Add(converter);
			Assert.AreEqual("converted value", _converterGroup.Convert(value, targetType, parameter, cultureInfo));
		}

		[Test]
		public void Convert_ShouldChainConvertersTogetherInOrder()
		{
			IValueConverter converter1 = Mocks.DynamicMock<IValueConverter>();
			IValueConverter converter2 = Mocks.DynamicMock<IValueConverter>();
			IValueConverter converter3 = Mocks.DynamicMock<IValueConverter>();

			Expect.Call(converter1.Convert("start value", null, null, null)).Repeat.Once().Return("converter1 result");
			Expect.Call(converter2.Convert("converter1 result", null, null, null)).Repeat.Once().Return("converter2 result");
			Expect.Call(converter3.Convert("converter2 result", null, null, null)).Repeat.Once().Return("converter3 result");
			Mocks.ReplayAll();

			_converterGroup.Converters.Add(converter1);
			_converterGroup.Converters.Add(converter2);
			_converterGroup.Converters.Add(converter3);
			Assert.AreEqual("converter3 result", _converterGroup.Convert("start value", null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldReturnUnsetValueIfNoConverters()
		{
			Assert.AreEqual(DependencyProperty.UnsetValue, _converterGroup.ConvertBack("abc", null, null, null));
		}

		[Test]
		public void ConvertBack_ShouldPassParametersIntoConverters()
		{
			object value = "abc";
			Type targetType = typeof(int);
			object parameter = "parameter";
			CultureInfo cultureInfo = new CultureInfo("de-DE");

			IValueConverter converter = Mocks.DynamicMock<IValueConverter>();
			Expect.Call(converter.ConvertBack(value, targetType, parameter, cultureInfo)).Return("converted value");
			Mocks.ReplayAll();

			_converterGroup.Converters.Add(converter);
			Assert.AreEqual("converted value", _converterGroup.ConvertBack(value, targetType, parameter, cultureInfo));
		}

		[Test]
		public void ConvertBack_ShouldChainConvertersTogetherInReverseOrder()
		{
			IValueConverter converter1 = Mocks.DynamicMock<IValueConverter>();
			IValueConverter converter2 = Mocks.DynamicMock<IValueConverter>();
			IValueConverter converter3 = Mocks.DynamicMock<IValueConverter>();

			Expect.Call(converter3.ConvertBack("start value", null, null, null)).Repeat.Once().Return("converter1 result");
			Expect.Call(converter2.ConvertBack("converter1 result", null, null, null)).Repeat.Once().Return("converter2 result");
			Expect.Call(converter1.ConvertBack("converter2 result", null, null, null)).Repeat.Once().Return("converter3 result");
			Mocks.ReplayAll();

			_converterGroup.Converters.Add(converter1);
			_converterGroup.Converters.Add(converter2);
			_converterGroup.Converters.Add(converter3);
			Assert.AreEqual("converter3 result", _converterGroup.ConvertBack("start value", null, null, null));
		}
	}
}