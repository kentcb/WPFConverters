using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kent.Boogaart.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that allows multiple <see cref="IValueConverter"/> implementations to be grouped
    /// together and executed in a pipeline.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>ConverterGroup</c> class allows multiple <see cref="IValueConverter"/> implementations to be executed in a pipeline in order to
    /// perform a conversion. The <see cref="Convert"/> method executes the <see cref="IValueConverter.Convert"/> method on each converter in the
    /// <see cref="Converters"/> collection. The <see cref="ConvertBack"/> method executes the <see cref="IValueConverter.ConvertBack"/> method
    /// on each converter in reverse order.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how a <see cref="DateTime"/> can be converted to local time prior to being formatted for display:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label>
    ///		<Label.Content>
    ///			<Binding Path="StartDateTime">
    ///				<Binding.Converter>
    ///					<ConverterGroup>
    /// 					<DateTimeConverter TargetKind="Local"/>
    /// 					<FormatConverter FormatString="{0:t}"/>
    ///					</ConverterGroup>
    ///				</Binding.Converter>
    ///			</Binding>
    ///		</Label.Content>
    ///	</Label>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows a <c>ConverterGroup</c> being used to convert user input to either <see langword="true"/> or
    /// <see langword="false"/>. If the user types "Yes" in the <c>TextBox</c> then the <c>CheckBox</c> will be checked. If the user
    /// types "No" then the <c>TextBox</c> will be unchecked. Any other value will result in the <c>CheckBox</c> being in an
    /// indeterminate state. Note that the comparisons are case-insensitive thanks to the <see cref="CaseConverter"/>.
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox x:Name="_textBox"/>
    /// <CheckBox IsThreeState="True">
    ///		<CheckBox.IsChecked>
    ///			<Binding Path="Text" ElementName="_textBox" FallbackValue="{x:Null}">
    ///				<Binding.Converter>
    ///					<ConverterGroup>
    ///						<CaseConverter Casing="Upper"/>
    ///						<MapConverter>
    ///							<Mapping From="YES">
    ///								<Mapping.To>
    ///									<sys:Boolean>True</sys:Boolean>
    ///								</Mapping.To>
    ///							</Mapping>
    ///							<Mapping From="NO">
    ///								<Mapping.To>
    ///									<sys:Boolean>False</sys:Boolean>
    ///								</Mapping.To>
    ///							</Mapping>
    ///						</MapConverter>
    ///					</ConverterGroup>
    ///				</Binding.Converter>
    ///			</Binding>
    ///		</CheckBox.IsChecked>
    ///	</CheckBox>
    /// ]]>
    /// </code>
    /// </example>
    [ContentProperty("Converters")]
#if !SILVERLIGHT
    [ValueConversion(typeof(object), typeof(object))]
#endif
    public class ConverterGroup : DependencyObject, IValueConverter
    {
#if !SILVERLIGHT
        private static readonly DependencyPropertyKey _convertersPropertyKey = DependencyProperty.RegisterReadOnly("Converters",
            typeof(Collection<IValueConverter>),
            typeof(ConverterGroup),
            new PropertyMetadata());
#endif

        /// <summary>
        /// Identifies the <see cref="Converters"/> dependency property.
        /// </summary>
#if !SILVERLIGHT
        public static readonly DependencyProperty ConvertersProperty = _convertersPropertyKey.DependencyProperty;
#else
        public static readonly DependencyProperty ConvertersProperty = DependencyProperty.Register("Converters",
            typeof(Collection<IValueConverter>),
            typeof(ConverterGroup),
            new PropertyMetadata(null));
#endif

        /// <summary>
        /// Gets the collection of <see cref="IValueConverter"/>s in this <c>ConverterGroup</c>.
        /// </summary>
        public Collection<IValueConverter> Converters
        {
            get
            {
                return GetValue(ConvertersProperty) as Collection<IValueConverter>;
            }
            private set
            {
#if !SILVERLIGHT
                SetValue(_convertersPropertyKey, value);
#else
                SetValue(ConvertersProperty, value);
#endif
            }
        }

        /// <summary>
        /// Constructs an instance of <c>ConverterGroup</c>.
        /// </summary>
        public ConverterGroup()
        {
            Converters = new Collection<IValueConverter>();
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
            if (Converters.Count == 0)
            {
                return DependencyProperty.UnsetValue;
            }

            object convertedValue = value;

            foreach (IValueConverter valueConverter in Converters)
            {
                convertedValue = valueConverter.Convert(convertedValue, targetType, parameter, culture);
            }

            return convertedValue;
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
            if (Converters.Count == 0)
            {
                return DependencyProperty.UnsetValue;
            }

            object convertedValue = value;

            for (int i = Converters.Count - 1; i >= 0; --i)
            {
                convertedValue = Converters[i].ConvertBack(convertedValue, targetType, parameter, culture);
            }

            return convertedValue;
        }
    }
}
