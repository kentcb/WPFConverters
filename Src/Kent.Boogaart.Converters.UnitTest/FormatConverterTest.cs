using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using NUnit.Framework;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class FormatConverterTest : UnitTest
	{
		private FormatConverter _formatConverter;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_formatConverter = new FormatConverter();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNull(_formatConverter.FormatString);
		}

		[Test]
		public void Constructor_FormatString_ShouldSetFormatString()
		{
			_formatConverter = new FormatConverter(null);
			Assert.IsNull(_formatConverter.FormatString);
			_formatConverter = new FormatConverter("abc");
			Assert.AreEqual("abc", _formatConverter.FormatString);
		}

		[Test]
		public void FormatString_ShouldGetAndSetFormatString()
		{
			Assert.IsNull(_formatConverter.FormatString);
			_formatConverter.FormatString = "abc";
			Assert.AreEqual("abc", _formatConverter.FormatString);
			_formatConverter.FormatString = null;
			Assert.IsNull(_formatConverter.FormatString);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage="No FormatString has been specified.")]
		public void Convert_Single_ShouldThrowIfNoFormatString()
		{
			_formatConverter.Convert(27, null, null, null);
		}

		[Test]
		public void Convert_Single_ShouldFormatValue()
		{
			_formatConverter.FormatString = "{0:00.00}";
			Assert.AreEqual("01.00", _formatConverter.Convert(1d, null, null, null));
		}

		[Test]
		public void Convert_Single_ShouldUseSpecifiedCulture()
		{
			_formatConverter.FormatString = "{0:00.00}";
			Assert.AreEqual("01,00", _formatConverter.Convert(1d, null, null, new CultureInfo("de-DE")));
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConvertBack_Single_ShouldThrowIfNoTargetType()
		{
			_formatConverter.ConvertBack(null, (Type) null, null, null);
		}

		[Test]
		public void ConvertBack_Single_ShouldReturnUnsetValueIfConversionFails()
		{
			_formatConverter.FormatString = "{0:00.00}";
			Assert.AreSame(DependencyProperty.UnsetValue, _formatConverter.ConvertBack("abc", typeof(int), null, null));
		}

		[Test]
		public void ConvertBack_Single_ShouldConvertBackIfPossible()
		{
			_formatConverter.FormatString = "{0:00.00}";
			Assert.AreEqual(123, _formatConverter.ConvertBack("123", typeof(double), null, null));
			Assert.AreEqual(13.80, _formatConverter.ConvertBack("13.8", typeof(double), null, null));
		}

		[Test]
		public void ConvertBack_Single_ShouldUseSpecifiedCulture()
		{
			_formatConverter.FormatString = "{0:00.00}";
			Assert.AreEqual(123, _formatConverter.ConvertBack("123", typeof(double), null, new CultureInfo("de-DE")));
			Assert.AreEqual(13.80, _formatConverter.ConvertBack("13,8", typeof(double), null, new CultureInfo("de-DE")));
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "No FormatString has been specified.")]
		public void Convert_Multi_ShouldThrowIfNoFormatString()
		{
			_formatConverter.Convert(new object[] { 26, 27 }, null, null, null);
		}

		[Test]
		public void Convert_Multi_ShouldFormatValue()
		{
			_formatConverter.FormatString = "Value 1: {0} and value 2: {1}";
			Assert.AreEqual("Value 1: 26 and value 2: 27", _formatConverter.Convert(new object[] { 26, 27 }, null, null, null));
		}

		[Test]
		public void Convert_Multi_ShouldUseSpecifiedCulture()
		{
			_formatConverter.FormatString = "Value 1: {0:00.00} and value 2: {1:00.00}";
			Assert.AreEqual("Value 1: 26,00 and value 2: 27,00", _formatConverter.Convert(new object[] { 26, 27 }, null, null, new CultureInfo("de-DE")));
		}

		[Test]
		public void ConvertBack_Multi_ShouldReturnNull()
		{
			Assert.IsNull(_formatConverter.ConvertBack("something", new Type[] { }, null, null));
		}
	}
}
