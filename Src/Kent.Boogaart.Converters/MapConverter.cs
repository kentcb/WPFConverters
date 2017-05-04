namespace Kent.Boogaart.Converters
{
    using Kent.Boogaart.HelperTrinity.Extensions;
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that converts from one set of values to another based on the contents of the
    /// <see cref="Mappings"/> collection.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>MapConverter</c> converts from one set of values to another. The source and destination values are stored in instances of
    /// <see cref="Mapping"/> inside the <see cref="Mappings"/> collection. 
    /// </para>
    /// <para>
    /// If this converter is asked to convert a value for which it has no knowledge, it will use the <see cref="FallbackBehavior"/> to determine
    /// how to deal with the situation.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows a <c>MapConverter</c> being used to control the visibility of a <c>Label</c> based on a
    /// <c>CheckBox</c>:
    /// <code lang="xml">
    /// <![CDATA[
    /// <CheckBox x:Name="_checkBox"/>
    /// <Label Content="Here is the label.">
    ///     <Label.Visibility>
    ///         <Binding Path="IsChecked" ElementName="_checkBox" FallbackValue="Collapsed">
    ///             <Binding.Converter>
    ///                 <MapConverter>
    ///                     <Mapping To="{x:Static Visibility.Visible}">
    ///                         <Mapping.From>
    ///                             <sys:Boolean>True</sys:Boolean>
    ///                         </Mapping.From>
    ///                     </Mapping>
    ///                 </MapConverter>
    ///             </Binding.Converter>
    ///         </Binding>
    ///     </Label.Visibility>
    /// </Label>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how a <c>MapConverter</c> can be used to convert between values of the <see cref="UriFormat"/>
    /// enumeration and human-readable strings. Notice how not all possible values are present in the mappings. The fallback behavior
    /// is set to <c>ReturnOriginalValue</c> to ensure that any conversion failures result in the original value being returned:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label>
    ///        <Label.Content>
    ///            <Binding Path="UriFormat">
    ///                <Binding.Converter>
    ///                    <MapConverter FallbackBehavior="ReturnOriginalValue">
    ///                        <Mapping From="{x:Static sys:UriFormat.SafeUnescaped}" To="Safe unescaped"/>
    ///                        <Mapping From="{x:Static sys:UriFormat.UriEscaped}" To="URI escaped"/>
    ///                    </MapConverter>
    ///                </Binding.Converter>
    ///            </Binding>
    ///        </Label.Content>
    ///    </Label>
    /// ]]>
    /// </code>
    /// </example>
    [ContentProperty("Mappings")]
#if !SILVERLIGHT
    [ValueConversion(typeof(object), typeof(object))]
#endif
    public class MapConverter : IValueConverter
    {
        private readonly Collection<Mapping> mappings;
        private FallbackBehavior fallbackBehavior;
        private object fallbackValue;

        /// <summary>
        /// Initializes a new instance of the MapConverter class.
        /// </summary>
        public MapConverter()
        {
            this.mappings = new Collection<Mapping>();
        }

        /// <summary>
        /// Gets or sets the fallback behavior for this <c>MapConverter</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The fallback behavior determines how this <c>MapConverter</c> treats failed conversions. <c>ReturnUnsetValue</c> (the default)
        /// specifies that any failed conversions should return <see cref="DependencyProperty.UnsetValue"/>, which can be used in combination with
        /// <c>Binding.FallbackValue</c> to default bindings to a specific value.
        /// </para>
        /// <para>
        /// Alternatively, <c>FallbackBehavior.ReturnOriginalValue</c> can be specified so that failed conversions result in the original value
        /// being returned. This is useful where mappings are only necessary for a subset of the total possible values. Mappings can be specified
        /// where necessary and other values can be returned as is by the <c>MapConverter</c> by setting the fallback behavior to
        /// <c>ReturnOriginalValue</c>.
        /// </para>
        /// </remarks>
        public FallbackBehavior FallbackBehavior
        {
            get
            {
                return this.fallbackBehavior;
            }

            set
            {
                value.AssertEnumMember("value", FallbackBehavior.ReturnFallbackValue, FallbackBehavior.ReturnOriginalValue, FallbackBehavior.ReturnUnsetValue);
                this.fallbackBehavior = value;
            }
        }

        /// <summary>
        /// Gets or sets the value that will be returned if <see cref="FallbackBehavior"/> is set to <see cref="FallbackBehavior.ReturnFallbackValue"/>
        /// and no mapping is present for the converted value.
        /// </summary>
        public object FallbackValue
        {
            get { return this.fallbackValue; }
            set { this.fallbackValue = value; }
        }

        /// <summary>
        /// Gets the collection of <see cref="Mapping"/>s configured for this <c>MapConverter</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each <see cref="Mapping"/> defines a relationship between a source object (see <see cref="Mapping.From"/>) and a destination (see
        /// <see cref="Mapping.To"/>). The <c>MapConverter</c> uses these mappings whilst attempting to convert values.
        /// </para>
        /// </remarks>
        public Collection<Mapping> Mappings
        {
            get { return this.mappings; }
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
            foreach (var mapping in this.Mappings)
            {
                if (object.Equals(value, mapping.From))
                {
                    return mapping.To;
                }
            }

            if (FallbackBehavior == FallbackBehavior.ReturnUnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            else if (FallbackBehavior == Converters.FallbackBehavior.ReturnOriginalValue)
            {
                return value;
            }
            else
            {
                return this.FallbackValue;
            }
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
            foreach (var mapping in this.Mappings)
            {
                if (object.Equals(value, mapping.To))
                {
                    return mapping.From;
                }
            }

            if (FallbackBehavior == FallbackBehavior.ReturnUnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            else if (FallbackBehavior == Converters.FallbackBehavior.ReturnOriginalValue)
            {
                return value;
            }
            else
            {
                return this.FallbackValue;
            }
        }
    }
}