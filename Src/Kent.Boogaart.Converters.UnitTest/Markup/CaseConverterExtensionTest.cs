using System.Windows.Controls;
using Xunit;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
	public sealed class CaseConverterExtensionTest : UnitTest
	{
		private CaseConverterExtension _caseConverterExtension;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_caseConverterExtension = new CaseConverterExtension();
		}

		[Fact]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.Equal(CharacterCasing.Normal, _caseConverterExtension.Casing);
		}

		[Fact]
		public void Constructor_Casing_ShouldSetCasing()
		{
			_caseConverterExtension = new CaseConverterExtension(CharacterCasing.Upper);
			Assert.Equal(CharacterCasing.Upper, _caseConverterExtension.Casing);
		}

		[Fact]
		public void Casing_ShouldGetAndSet()
		{
			Assert.Equal(CharacterCasing.Normal, _caseConverterExtension.Casing);
			_caseConverterExtension.Casing = CharacterCasing.Upper;
			Assert.Equal(CharacterCasing.Upper, _caseConverterExtension.Casing);
		}

		[Fact]
		public void ProvideValue_ShouldYieldCaseConverterWithGivenCasing()
		{
			_caseConverterExtension.Casing = CharacterCasing.Upper;
			CaseConverter caseConverter = _caseConverterExtension.ProvideValue(null) as CaseConverter;
			Assert.NotNull(caseConverter);
			Assert.Equal(CharacterCasing.Upper, caseConverter.Casing);
		}
	}
}
