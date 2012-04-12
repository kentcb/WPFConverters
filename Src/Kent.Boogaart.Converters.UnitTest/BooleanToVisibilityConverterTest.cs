using Xunit;
using System.Windows;
using System.Globalization;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class BooleanToVisibilityConverterTest : UnitTest
    {
        private BooleanToVisibilityConverter converter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.converter = new BooleanToVisibilityConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.False(this.converter.IsReversed);
            Assert.False(this.converter.UseHidden);
        }

        [Fact]
        public void Constructor_Args_ShouldSetProperties()
        {
            this.converter = new BooleanToVisibilityConverter(true, true);
            Assert.True(this.converter.IsReversed);
            Assert.True(this.converter.UseHidden);
        }

        [Fact]
        public void IsReversed_ShouldGetAndSetIsReversed()
        {
            Assert.False(this.converter.IsReversed);
            this.converter.IsReversed = true;
            Assert.True(this.converter.IsReversed);
        }

        [Fact]
        public void UseHidden_ShouldGetAndSetUseHidden()
        {
            Assert.False(this.converter.UseHidden);
            this.converter.UseHidden = true;
            Assert.True(this.converter.UseHidden);
        }

        [Fact]
        public void Convert_ShouldReturnCollapsedIfGivenFalse()
        {
            Assert.Equal(Visibility.Collapsed, this.converter.Convert(false, null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnVisibleIfGivenTrue()
        {
            Assert.Equal(Visibility.Visible, this.converter.Convert(true, null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnHiddenIfGivenFalseAndUseHiddenIsTrue()
        {
            this.converter.UseHidden = true;

            Assert.Equal(Visibility.Hidden, this.converter.Convert(false, null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnVisibleIfGivenFalseAndIsReversedIsTrue()
        {
            this.converter.IsReversed = true;

            Assert.Equal(Visibility.Visible, this.converter.Convert(false, null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnCollapsedIfGivenTrueAndIsReversedIsTrue()
        {
            this.converter.IsReversed = true;

            Assert.Equal(Visibility.Collapsed, this.converter.Convert(true, null, null, null));
        }

        [Fact]
        public void Convert_ShouldReturnHiddenIfGivenTrueAndIsReversedIsTrueAndUseHiddenIsTrue()
        {
            this.converter.IsReversed = true;
            this.converter.UseHidden = true;

            Assert.Equal(Visibility.Hidden, this.converter.Convert(true, null, null, null));
        }

        [Fact]
        public void Convert_CanConvertValuesToBooleanAutomatically()
        {
            Assert.Equal(Visibility.Collapsed, this.converter.Convert("false", null, null, null));
            Assert.Equal(Visibility.Collapsed, this.converter.Convert("False", null, null, null));
            Assert.Equal(Visibility.Collapsed, this.converter.Convert("FALSE", null, null, null));
            Assert.Equal(Visibility.Collapsed, this.converter.Convert(0, null, null, null));
            Assert.Equal(Visibility.Collapsed, this.converter.Convert(0d, null, null, null));

            Assert.Equal(Visibility.Visible, this.converter.Convert("true", null, null, null));
            Assert.Equal(Visibility.Visible, this.converter.Convert("True", null, null, null));
            Assert.Equal(Visibility.Visible, this.converter.Convert("TRUE", null, null, null));
            Assert.Equal(Visibility.Visible, this.converter.Convert(1, null, null, null));
            Assert.Equal(Visibility.Visible, this.converter.Convert(1d, null, null, null));
        }

        [Fact]
        public void ConvertBack_ReturnsUnsetValueIfValueIsNotVisibility()
        {
            Assert.Equal(DependencyProperty.UnsetValue, this.converter.ConvertBack(null, null, null, null));
            Assert.Equal(DependencyProperty.UnsetValue, this.converter.ConvertBack("Visible", null, null, null));
            Assert.Equal(DependencyProperty.UnsetValue, this.converter.ConvertBack(1, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnFalseIfGivenCollapsed()
        {
            Assert.Equal(false, this.converter.ConvertBack(Visibility.Collapsed, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnFalseIfGivenHidden()
        {
            Assert.Equal(false, this.converter.ConvertBack(Visibility.Hidden, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnTrueIfGivenVisible()
        {
            Assert.Equal(true, this.converter.ConvertBack(Visibility.Visible, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnFalseIfGivenVisibleAndIsReversedIsTrue()
        {
            this.converter.IsReversed = true;

            Assert.Equal(false, this.converter.ConvertBack(Visibility.Visible, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnTrueIfGivenCollapsedAndIsReversedIsTrue()
        {
            this.converter.IsReversed = true;

            Assert.Equal(true, this.converter.ConvertBack(Visibility.Collapsed, null, null, null));
        }

        [Fact]
        public void ConvertBack_ShouldReturnTrueIfGivenHiddenAndIsReversedIsTrue()
        {
            this.converter.IsReversed = true;

            Assert.Equal(true, this.converter.ConvertBack(Visibility.Hidden, null, null, null));
        }
    }
}
