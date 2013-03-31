namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    using Kent.Boogaart.Converters.Markup;
    using System;
    using System.Windows.Controls;
    using Xunit;

    public sealed class CaseConverterExtensionFixture
    {
        [Fact]
        public void ctor_sets_source_casing_to_normal()
        {
            var converterExtension = new CaseConverterExtension();
            Assert.Equal(CharacterCasing.Normal, converterExtension.SourceCasing);
        }

        [Fact]
        public void ctor_sets_target_casing_to_normal()
        {
            var converterExtension = new CaseConverterExtension();
            Assert.Equal(CharacterCasing.Normal, converterExtension.TargetCasing);
        }

        [Fact]
        public void ctor_that_takes_casing_sets_source_casing_and_target_casing()
        {
            var converterExtension = new CaseConverterExtension(CharacterCasing.Upper);
            Assert.Equal(CharacterCasing.Upper, converterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Upper, converterExtension.TargetCasing);
        }

        [Fact]
        public void ctor_that_takes_source_casing_and_target_casing_sets_source_casing_and_target_casing()
        {
            var converterExtension = new CaseConverterExtension(CharacterCasing.Upper, CharacterCasing.Lower);
            Assert.Equal(CharacterCasing.Upper, converterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Lower, converterExtension.TargetCasing);
        }

        [Fact]
        public void source_casing_throws_if_invalid()
        {
            var converterExtension = new CaseConverterExtension();
            var ex = Assert.Throws<ArgumentException>(() => converterExtension.SourceCasing = (CharacterCasing)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.Windows.Controls.CharacterCasing'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void target_casing_throws_if_invalid()
        {
            var converterExtension = new CaseConverterExtension();
            var ex = Assert.Throws<ArgumentException>(() => converterExtension.TargetCasing = (CharacterCasing)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.Windows.Controls.CharacterCasing'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void casing_throws_if_invalid()
        {
            var converterExtension = new CaseConverterExtension();
            var ex = Assert.Throws<ArgumentException>(() => converterExtension.Casing = (CharacterCasing)100);
            Assert.Equal("Enum value '100' is not defined for enumeration 'System.Windows.Controls.CharacterCasing'.\r\nParameter name: value", ex.Message);
        }

        [Fact]
        public void casing_sets_source_casing_and_target_casing()
        {
            var converterExtension = new CaseConverterExtension();
            Assert.Equal(CharacterCasing.Normal, converterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Normal, converterExtension.TargetCasing);

            converterExtension.Casing = CharacterCasing.Upper;
            Assert.Equal(CharacterCasing.Upper, converterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Upper, converterExtension.TargetCasing);

            converterExtension.Casing = CharacterCasing.Lower;
            Assert.Equal(CharacterCasing.Lower, converterExtension.SourceCasing);
            Assert.Equal(CharacterCasing.Lower, converterExtension.TargetCasing);
        }

        [Fact]
        public void provide_value_returns_appropriate_case_converter()
        {
            var converterExtension = new CaseConverterExtension
            {
                SourceCasing = CharacterCasing.Upper,
                TargetCasing = CharacterCasing.Lower
            };
            var caseConverter = converterExtension.ProvideValue(null) as CaseConverter;

            Assert.NotNull(caseConverter);
            Assert.Equal(CharacterCasing.Upper, caseConverter.SourceCasing);
            Assert.Equal(CharacterCasing.Lower, caseConverter.TargetCasing);
        }
    }
}
