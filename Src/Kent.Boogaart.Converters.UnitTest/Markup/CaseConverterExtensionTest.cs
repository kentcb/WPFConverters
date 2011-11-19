using System.Windows.Controls;
using Xunit;
using Kent.Boogaart.Converters.Markup;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
    public sealed class CaseConverterExtensionTest : UnitTest
    {
        private CaseConverterExtension caseConverterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.caseConverterExtension = new CaseConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.Casing);
        }

        [Fact]
        public void Constructor_Casing_ShouldSetCasing()
        {
            this.caseConverterExtension = new CaseConverterExtension(CharacterCasing.Upper);
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.Casing);
        }

        [Fact]
        public void Casing_ShouldGetAndSet()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.Casing);
            this.caseConverterExtension.Casing = CharacterCasing.Upper;
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.Casing);
        }

        [Fact]
        public void ProvideValue_ShouldYieldCaseConverterWithGivenCasing()
        {
            this.caseConverterExtension.Casing = CharacterCasing.Upper;
            CaseConverter caseConverter = this.caseConverterExtension.ProvideValue(null) as CaseConverter;
            Assert.NotNull(caseConverter);
            Assert.Equal(CharacterCasing.Upper, caseConverter.Casing);
        }
    }
}
