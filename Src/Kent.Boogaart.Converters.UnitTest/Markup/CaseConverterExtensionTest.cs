using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using NUnit.Framework;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
	[TestFixture]
	public sealed class CaseConverterExtensionTest : UnitTest
	{
		private CaseConverterExtension _caseConverterExtension;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_caseConverterExtension = new CaseConverterExtension();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.AreEqual(CharacterCasing.Normal, _caseConverterExtension.Casing);
		}

		[Test]
		public void Constructor_Casing_ShouldSetCasing()
		{
			_caseConverterExtension = new CaseConverterExtension(CharacterCasing.Upper);
			Assert.AreEqual(CharacterCasing.Upper, _caseConverterExtension.Casing);
		}

		[Test]
		public void Casing_ShouldGetAndSet()
		{
			Assert.AreEqual(CharacterCasing.Normal, _caseConverterExtension.Casing);
			_caseConverterExtension.Casing = CharacterCasing.Upper;
			Assert.AreEqual(CharacterCasing.Upper, _caseConverterExtension.Casing);
		}

		[Test]
		public void ProvideValue_ShouldYieldCaseConverterWithGivenCasing()
		{
			_caseConverterExtension.Casing = CharacterCasing.Upper;
			CaseConverter caseConverter = _caseConverterExtension.ProvideValue(null) as CaseConverter;
			Assert.IsNotNull(caseConverter);
			Assert.AreEqual(CharacterCasing.Upper, caseConverter.Casing);
		}
	}
}
