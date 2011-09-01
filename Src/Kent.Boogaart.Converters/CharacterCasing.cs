namespace Kent.Boogaart.Converters
{
#if SILVERLIGHT
    /// <summary>
    /// Defines different casing modes supported by the <see cref="CaseConverter"/>.
    /// </summary>
    public enum CharacterCasing
    {
        /// <summary>
        /// No conversion performed.
        /// </summary>
        Normal,

        /// <summary>
        /// Characters are converted to lowercase.
        /// </summary>
        Lower,

        /// <summary>
        /// Characters are converted to uppercase.
        /// </summary>
        Upper
    }
#endif
}