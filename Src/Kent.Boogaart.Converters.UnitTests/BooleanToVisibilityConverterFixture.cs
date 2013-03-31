namespace Kent.Boogaart.Converters.UnitTests
{
    using System.Windows;
    using Xunit;

    public sealed class BooleanToVisibilityConverterFixture
    {
        [Fact]
        public void ctor_defaults_is_reversed_to_false()
        {
            var converter = new BooleanToVisibilityConverter();
            Assert.False(converter.IsReversed);
        }

        [Fact]
        public void ctor_defaults_use_hidden_to_false()
        {
            var converter = new BooleanToVisibilityConverter();
            Assert.False(converter.UseHidden);
        }

        [Fact]
        public void ctor_assigns_specified_is_reversed_value()
        {
            var converter = new BooleanToVisibilityConverter(true, false);

            Assert.True(converter.IsReversed);
        }

        [Fact]
        public void ctor_assigns_specified_use_hidden_value()
        {
            var converter = new BooleanToVisibilityConverter(false, true);

            Assert.True(converter.UseHidden);
        }

        [Fact]
        public void convert_returns_visible_if_value_is_true()
        {
            var converter = new BooleanToVisibilityConverter();

            Assert.Equal(Visibility.Visible, converter.Convert(true, null, null, null));
        }

        [Fact]
        public void convert_returns_collapsed_if_value_is_false_and_use_hidden_is_false()
        {
            var converter = new BooleanToVisibilityConverter();

            Assert.Equal(Visibility.Collapsed, converter.Convert(false, null, null, null));
        }

        [Fact]
        public void convert_returns_hidden_if_value_is_false_and_use_hidden_is_true()
        {
            var converter = new BooleanToVisibilityConverter
            {
                UseHidden = true
            };

            Assert.Equal(Visibility.Hidden, converter.Convert(false, null, null, null));
        }

        [Fact]
        public void convert_returns_visible_if_value_is_false_and_is_reversed_is_true()
        {
            var converter = new BooleanToVisibilityConverter
            {
                IsReversed = true
            };

            Assert.Equal(Visibility.Visible, converter.Convert(false, null, null, null));
        }

        [Fact]
        public void convert_returns_collapsed_if_value_is_true_and_is_reversed_is_true()
        {
            var converter = new BooleanToVisibilityConverter
            {
                IsReversed = true
            };

            Assert.Equal(Visibility.Collapsed, converter.Convert(true, null, null, null));
        }

        [Fact]
        public void convert_returns_hidden_if_value_is_false_and_is_reversed_is_true_and_use_hidden_is_true()
        {
            var converter = new BooleanToVisibilityConverter
            {
                IsReversed = true,
                UseHidden = true
            };

            Assert.Equal(Visibility.Hidden, converter.Convert(true, null, null, null));
        }

        [Fact]
        public void convert_automatically_converts_relevant_values_to_booleans()
        {
            var converter = new BooleanToVisibilityConverter();

            Assert.Equal(Visibility.Collapsed, converter.Convert("false", null, null, null));
            Assert.Equal(Visibility.Collapsed, converter.Convert("False", null, null, null));
            Assert.Equal(Visibility.Collapsed, converter.Convert("FALSE", null, null, null));
            Assert.Equal(Visibility.Collapsed, converter.Convert(0, null, null, null));
            Assert.Equal(Visibility.Collapsed, converter.Convert(0d, null, null, null));

            Assert.Equal(Visibility.Visible, converter.Convert("true", null, null, null));
            Assert.Equal(Visibility.Visible, converter.Convert("True", null, null, null));
            Assert.Equal(Visibility.Visible, converter.Convert("TRUE", null, null, null));
            Assert.Equal(Visibility.Visible, converter.Convert(1, null, null, null));
            Assert.Equal(Visibility.Visible, converter.Convert(1d, null, null, null));
        }

        [Fact]
        public void convert_back_returns_unset_value_if_value_is_not_a_visibility_enumeration_member()
        {
            var converter = new BooleanToVisibilityConverter();

            Assert.Equal(DependencyProperty.UnsetValue, converter.ConvertBack(null, null, null, null));
            Assert.Equal(DependencyProperty.UnsetValue, converter.ConvertBack("Visible", null, null, null));
            Assert.Equal(DependencyProperty.UnsetValue, converter.ConvertBack(1, null, null, null));
        }

        [Fact]
        public void convert_back_returns_false_if_value_is_collapsed()
        {
            var converter = new BooleanToVisibilityConverter();

            Assert.Equal(false, converter.ConvertBack(Visibility.Collapsed, null, null, null));
        }

        [Fact]
        public void convert_back_returns_false_if_value_is_hidden()
        {
            var converter = new BooleanToVisibilityConverter();

            Assert.Equal(false, converter.ConvertBack(Visibility.Hidden, null, null, null));
        }

        [Fact]
        public void convert_back_returns_false_if_value_is_visible_and_is_reversed_is_true()
        {
            var converter = new BooleanToVisibilityConverter
            {
                IsReversed = true
            };

            Assert.Equal(false, converter.ConvertBack(Visibility.Visible, null, null, null));
        }

        [Fact]
        public void convert_back_returns_true_if_value_is_visible()
        {
            var converter = new BooleanToVisibilityConverter();

            Assert.Equal(true, converter.ConvertBack(Visibility.Visible, null, null, null));
        }

        [Fact]
        public void convert_back_returns_true_if_value_is_collapsed_and_is_reversed_is_true()
        {
            var converter = new BooleanToVisibilityConverter
            {
                IsReversed = true
            };

            Assert.Equal(true, converter.ConvertBack(Visibility.Collapsed, null, null, null));
        }

        [Fact]
        public void convert_back_returns_true_if_value_is_hidden_and_is_reversed_is_true()
        {
            var converter = new BooleanToVisibilityConverter
            {
                IsReversed = true
            };

            Assert.Equal(true, converter.ConvertBack(Visibility.Hidden, null, null, null));
        }
    }
}