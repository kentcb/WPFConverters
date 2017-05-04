namespace Kent.Boogaart.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that converts boolean values to <see cref="Visibility"/> values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>BooleanToVisibilityConverter</c> class can be used to convert boolean values (or values that can be converted to boolean values) to
    /// <see cref="Visibility"/> values. By default, <see langword="true"/> is converted to <see cref="Visibility.Visible"/> and <see langword="false"/>
    /// is converted to <see cref="Visibility.Collapsed"/>. However, the <see cref="UseHidden"/> property can be set to <see langword="true"/> in order
    /// to return <see cref="Visibility.Hidden"/> instead of <see cref="Visibility.Collapsed"/>. In addition, the <see cref="IsReversed"/> property
    /// can be set to <see langword="true"/> to reverse the returned values.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how a <c>BooleanToVisibilityConverter</c> can be used to display a <c>TextBox</c> only when a property is <c>true</c>:
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox Visibility="{Binding ShowTheTextBox, Converter={BooleanToVisibilityConverter}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how a <c>BooleanToVisibilityConverter</c> can be used to display a <c>TextBox</c> only when a property is <c>true</c>.
    /// Rather than collapsing the <c>TextBox</c>, it is hidden:
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox Visibility="{Binding ShowTheTextBox, Converter={BooleanToVisibilityConverter UseHidden=true}}"/>
    /// ]]>
    /// </code>
    /// </example>
#if !SILVERLIGHT
    [ValueConversion(typeof(bool), typeof(Visibility))]
#endif
    public class BooleanToVisibilityConverter : IValueConverter
#if !SILVERLIGHT
, IMultiValueConverter
#endif
    {
        private bool isReversed;
#if !SILVERLIGHT
        private bool useHidden;
#endif

        /// <summary>
        /// Initializes a new instance of the BooleanToVisibilityConverter class.
        /// </summary>
        public BooleanToVisibilityConverter()
        {
        }

#if !SILVERLIGHT
        /// <summary>
        /// Initializes a new instance of the BooleanToVisibilityConverter class.
        /// </summary>
        /// <param name="isReversed">
        /// Whether the return values should be reversed.
        /// </param>
        /// <param name="useHidden">
        /// Whether <see cref="Visibility.Hidden"/> should be used instead of <see cref="Visibility.Collapsed"/>.
        /// </param>
        public BooleanToVisibilityConverter(bool isReversed, bool useHidden)
        {
            this.isReversed = isReversed;
            this.useHidden = useHidden;
        }
#endif

        /// <summary>
        /// Gets or sets a value indicating whether the return values should be reversed.
        /// </summary>
        public bool IsReversed
        {
            get { return this.isReversed; }
            set { this.isReversed = value; }
        }

#if !SILVERLIGHT
        /// <summary>
        /// Gets or sets a value indicating whether <see cref="Visibility.Hidden"/> should be returned instead of <see cref="Visibility.Collapsed"/>.
        /// </summary>
        public bool UseHidden
        {
            get { return this.useHidden; }
            set { this.useHidden = value; }
        }
#endif

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
            var val = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);

            if (this.IsReversed)
            {
                val = !val;
            }

            if (val)
            {
                return Visibility.Visible;
            }

#if !SILVERLIGHT
            return this.UseHidden ? Visibility.Hidden : Visibility.Collapsed;
#else
            return Visibility.Collapsed;
#endif
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
            if (!(value is Visibility))
            {
                return DependencyProperty.UnsetValue;
            }

            var visibility = (Visibility)value;
            var result = visibility == Visibility.Visible;

            if (this.IsReversed)
            {
                result = !result;
            }

            return result;
        }


#if !SILVERLIGHT
        /// <summary>
        /// Attempts to convert the specified values.
        /// </summary>
        /// <param name="values">
        /// The values to convert.
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
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var val = true;
            foreach (var value in values)
            {
                bool temp;
                if(bool.TryParse(value.ToString(),out temp))
                {
                    val &= temp;
                }
            }

            if (this.IsReversed)
            {
                val = !val;
            }

            if (val)
            {
                return Visibility.Visible;
            }

#if !SILVERLIGHT
            return this.UseHidden ? Visibility.Hidden : Visibility.Collapsed;
#else
            return Visibility.Collapsed;
#endif
        }

        /// <summary>
        /// Attempts to convert back the specified values.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetTypes">
        /// The types of the binding target properties.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// Converted values.
        /// </returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
#endif
    }
}