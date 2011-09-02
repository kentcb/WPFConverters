namespace Kent.Boogaart.Converters
{
    /// <summary>
    /// Defines possible fallback behaviors for the <see cref="MapConverter"/>.
    /// </summary>
    public enum FallbackBehavior
    {
        /// <summary>
        /// Specifies that <see cref="System.Windows.DependencyProperty.UnsetValue"/> should be returned when falling back.
        /// </summary>
        ReturnUnsetValue,

        /// <summary>
        /// Specifies that the value being converted should be returned when falling back.
        /// </summary>
        ReturnOriginalValue,

        /// <summary>
        /// Specifies that the fallback value specified by <see cref="MapConverter.FallbackValue"/> should be returned when falling back.
        /// </summary>
        ReturnFallbackValue
    }
}
