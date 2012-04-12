using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that converts the casing of the input string.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>CaseConverter</c> class can be used to convert input strings into upper or lower case according to the <see cref="Casing"/> property.
    /// Setting <see cref="Casing"/> is a shortcut for setting both <see cref="SourceCasing"/> and <see cref="TargetCasing"/>. It is therefore possible
    /// to specify that the source and target properties be converted to different casings.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how a <c>CaseConverter</c> can be used to convert a bound value to upper-case:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter Upper}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how a <c>CaseConverter</c> can be used to convert a bound value to upper-case, but display it in lower-case:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter SourceCasing=Upper, TargetCasing=Lower}}"/>
    /// ]]>
    /// </code>
    /// </example>
#if !SILVERLIGHT
    [ValueConversion(typeof(string), typeof(string))]
#endif
    public class CaseConverter : IValueConverter
    {
        private CharacterCasing sourceCasing;
        private CharacterCasing targetCasing;

        /// <summary>
        /// Initializes a new instance of the CaseConverter class.
        /// </summary>
        public CaseConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverter class with the specified source and target casings.
        /// </summary>
        /// <param name="casing">
        /// The source and target casings for the converter (see <see cref="Casing"/>).
        /// </param>
        public CaseConverter(CharacterCasing casing)
        {
            this.Casing = casing;
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverter class with the specified source and target casings.
        /// </summary>
        /// <param name="sourceCasing">
        /// The source casing for the converter (see <see cref="SourceCasing"/>).
        /// </param>
        /// <param name="targetCasing">
        /// The target casing for the converter (see <see cref="TargetCasing"/>).
        /// </param>
        public CaseConverter(CharacterCasing sourceCasing, CharacterCasing targetCasing)
        {
            this.SourceCasing = sourceCasing;
            this.TargetCasing = targetCasing;
        }

        /// <summary>
        /// Gets or sets the source casing for the converter.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("sourceCasing")]
#endif
        public CharacterCasing SourceCasing
        {
            get
            {
                return this.sourceCasing;
            }

            set
            {
                ArgumentHelper.AssertEnumMember(value, "value", CharacterCasing.Lower, CharacterCasing.Upper, CharacterCasing.Normal);
                this.sourceCasing = value;
            }
        }

        /// <summary>
        /// Gets or sets the target casing for the converter.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("targetCasing")]
#endif
        public CharacterCasing TargetCasing
        {
            get
            {
                return this.targetCasing;
            }

            set
            {
                ArgumentHelper.AssertEnumMember(value, "value", CharacterCasing.Lower, CharacterCasing.Upper, CharacterCasing.Normal);
                this.targetCasing = value;
            }
        }

        /// <summary>
        /// Sets both the source and target casings for the converter.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("casing")]
#endif
        public CharacterCasing Casing
        {
            set
            {
                ArgumentHelper.AssertEnumMember(value, "value", CharacterCasing.Lower, CharacterCasing.Upper, CharacterCasing.Normal);
                this.sourceCasing = value;
                this.targetCasing = value;
            }
        }

        /// <summary>
        /// Attempts to convert the specified value.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            if (str != null)
            {
                culture = culture ?? CultureInfo.CurrentCulture;

                switch (this.TargetCasing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower(culture);
                    case CharacterCasing.Upper:
                        return str.ToUpper(culture);
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Attempts to convert the specified value back.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            if (str != null)
            {
                culture = culture ?? CultureInfo.CurrentCulture;

                switch (this.SourceCasing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower(culture);
                    case CharacterCasing.Upper:
                        return str.ToUpper(culture);
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
