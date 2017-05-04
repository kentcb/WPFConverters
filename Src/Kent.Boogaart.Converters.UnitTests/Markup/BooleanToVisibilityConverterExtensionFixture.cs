namespace Kent.Boogaart.Converters.UnitTests.Markup
{
    using Kent.Boogaart.Converters.Markup;
    using Xunit;

    public sealed class BooleanToVisibilityConverterExtensionFixture
    {
        [Fact]
        public void ctor_sets_is_reversed_to_false()
        {
            var converterExtension = new BooleanToVisibilityConverterExtension();
            Assert.Equal(false, converterExtension.IsReversed);
        }

        [Fact]
        public void ctor_sets_is_hidden_to_false()
        {
            var converterExtension = new BooleanToVisibilityConverterExtension();
            Assert.Equal(false, converterExtension.UseHidden);
        }

        [Fact]
        public void ctor_that_takes_is_reversed_and_use_hidden_sets_is_reversed_and_use_hidden()
        {
            var converterExtension = new BooleanToVisibilityConverterExtension();
            converterExtension = new BooleanToVisibilityConverterExtension(true, true);
            Assert.True(converterExtension.IsReversed);
            Assert.True(converterExtension.UseHidden);
        }

        [Fact]
        public void provide_value_get_an_appropriate_boolean_to_visibility_converter()
        {
            var converterExtension = new BooleanToVisibilityConverterExtension
            {
                IsReversed = true,
                UseHidden = true
            };
            var converter = converterExtension.ProvideValue(null) as BooleanToVisibilityConverter;

            Assert.NotNull(converter);
            Assert.True(converter.IsReversed);
            Assert.True(converter.UseHidden);
        }
    }
}
