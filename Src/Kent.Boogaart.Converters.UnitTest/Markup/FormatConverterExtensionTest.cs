using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
	[TestFixture]
	public sealed class FormatConverterExtensionTest : UnitTest
	{
		private FormatConverterExtension _formatConverterExtension;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_formatConverterExtension = new FormatConverterExtension();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.IsNull(_formatConverterExtension.FormatString);
		}

		[Test]
		public void Constructor_FormatString_ShouldSetExpression()
		{
			_formatConverterExtension = new FormatConverterExtension("format");
			Assert.AreEqual("format", _formatConverterExtension.FormatString);
		}

		[Test]
		public void FormatString_ShouldGetAndSet()
		{
			Assert.IsNull(_formatConverterExtension.FormatString);
			_formatConverterExtension.FormatString = "format";
			Assert.AreEqual("format", _formatConverterExtension.FormatString);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "No format string has been provided.")]
		public void ProvideValue_ShouldThrowIfNoFormatString()
		{
			_formatConverterExtension.ProvideValue(null);
		}

		[Test]
		public void ProvideValue_ShouldYieldFormatConverterWithGivenFormatString()
		{
			_formatConverterExtension.FormatString = "format";
			FormatConverter formatConverter = _formatConverterExtension.ProvideValue(null) as FormatConverter;
			Assert.IsNotNull(formatConverter);
			Assert.AreEqual("format", formatConverter.FormatString);
		}
	}
}
