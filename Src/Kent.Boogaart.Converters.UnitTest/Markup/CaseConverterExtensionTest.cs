using System;
using System.Windows.Controls;
using Kent.Boogaart.Converters.Markup;
using Xunit;

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
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.TargetCasing);
        }

        [Fact]
        public void Constructor_Casing_ShouldSetSourceAndTargetCasings()
        {
            this.caseConverterExtension = new CaseConverterExtension(CharacterCasing.Upper);
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.TargetCasing);
        }

        [Fact]
        public void Constructor_Casings_ShouldSetCasings()
        {
            this.caseConverterExtension = new CaseConverterExtension(CharacterCasing.Upper, CharacterCasing.Lower);
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Lower, this.caseConverterExtension.TargetCasing);
        }

        [Fact]
        public void SourceCasing_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.caseConverterExtension.SourceCasing = (CharacterCasing)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.Windows.Controls.CharacterCasing'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void SourceCasing_ShouldGetAndSetSourceCasing()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.SourceCasing);
            this.caseConverterExtension.SourceCasing = CharacterCasing.Upper;
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.SourceCasing);
            this.caseConverterExtension.SourceCasing = CharacterCasing.Lower;
            Assert.Equal(CharacterCasing.Lower, this.caseConverterExtension.SourceCasing);
        }

        [Fact]
        public void TargetCasing_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.caseConverterExtension.TargetCasing = (CharacterCasing)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.Windows.Controls.CharacterCasing'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void TargetCasing_ShouldGetAndSetTargetCasing()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.TargetCasing);
            this.caseConverterExtension.TargetCasing = CharacterCasing.Upper;
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.TargetCasing);
            this.caseConverterExtension.TargetCasing = CharacterCasing.Lower;
            Assert.Equal(CharacterCasing.Lower, this.caseConverterExtension.TargetCasing);
        }

        [Fact]
        public void Casing_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => this.caseConverterExtension.Casing = (CharacterCasing)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.Windows.Controls.CharacterCasing'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void Casing_ShouldSetSourceAndTargetCasings()
        {
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Normal, this.caseConverterExtension.TargetCasing);
            this.caseConverterExtension.Casing = CharacterCasing.Upper;
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Upper, this.caseConverterExtension.TargetCasing);
            this.caseConverterExtension.Casing = CharacterCasing.Lower;
            Assert.Equal(CharacterCasing.Lower, this.caseConverterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Lower, this.caseConverterExtension.TargetCasing);
        }

        [Fact]
        public void ProvideValue_ShouldYieldCaseConverterWithGivenCasing()
        {
            this.caseConverterExtension.SourceCasing = CharacterCasing.Upper;
            this.caseConverterExtension.TargetCasing = CharacterCasing.Lower;
            var caseConverter = this.caseConverterExtension.ProvideValue(null) as CaseConverter;
            Assert.NotNull(caseConverter);
            Assert.Equal(CharacterCasing.Upper, caseConverter.SourceCasing);
            Assert.Equal(CharacterCasing.Lower, caseConverter.TargetCasing);
        }
    }
}
