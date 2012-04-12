using Kent.Boogaart.Converters.Markup;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Markup
{
    public sealed class BooleanToVisibilityConverterExtensionTest : UnitTest
    {
        private BooleanToVisibilityConverterExtension converterExtension;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.converterExtension = new BooleanToVisibilityConverterExtension();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(false, this.converterExtension.IsReversed);
            Assert.Equal(false, this.converterExtension.UseHidden);
        }

        [Fact]
        public void Constructor_Args_ShouldSetProperties()
        {
            this.converterExtension = new BooleanToVisibilityConverterExtension(true, true);
            Assert.True(this.converterExtension.IsReversed);
            Assert.True(this.converterExtension.UseHidden);
        }

        [Fact]
        public void IsReversed_ShouldGetAndSetIsReversed()
        {
            Assert.False(this.converterExtension.IsReversed);
            this.converterExtension.IsReversed = true;
            Assert.True(this.converterExtension.IsReversed);
        }

        [Fact]
        public void UseHidden_ShouldGetAndSetUseHidden()
        {
            Assert.False(this.converterExtension.UseHidden);
            this.converterExtension.UseHidden = true;
            Assert.True(this.converterExtension.UseHidden);
        }

        [Fact]
        public void ProvideValue_ShouldYieldBooleanToVisibilityConverterWithGivenCasing()
        {
            this.converterExtension.IsReversed = true;
            this.converterExtension.UseHidden = true;
            var converter = this.converterExtension.ProvideValue(null) as BooleanToVisibilityConverter;
            Assert.NotNull(converter);
            Assert.True(converter.IsReversed);
            Assert.True(converter.UseHidden);
        }
    }
}
