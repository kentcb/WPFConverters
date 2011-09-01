using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that attempts to convert values between specified <see cref="Type"/>s.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>TypeConverter</c> class can be used to attempt to convert values between a <see cref="SourceType"/> and <see cref="TargetType"/>.
    /// The <see cref="Convert"/> method will attempt to convert values to the <see cref="TargetType"/>, whilst the <see cref="ConvertBack"/>
    /// method will attempt to convert values to the <see cref="SourceType"/>.
    /// </para>
    /// <para>
    /// This class attempts to convert values by way of the <see cref="IConvertible"/> interface. However, if the value being converted does not
    /// implement <see cref="IConvertible"/>, an attempt is made to convert via a suitable <see cref="System.ComponentModel.TypeConverter"/>.
    /// Failing that, <see cref="DependencyProperty.UnsetValue"/> is returned by conversion attempts.
    /// </para>
    /// <para>
    /// Since the WPF binding infrastructure already performs automatic conversions, this converter is useful mainly in group
    /// scenarios when using the <see cref="ConverterGroup"/> or <see cref="MultiConverterGroup"/> classes.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how a <c>TypeConverter</c> can be used to convert integer values in the source to strings:
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox Text="{Binding Age, Converter={TypeConverter SourceType=sys:Int32, TargetType=sys:String}}"/>
    /// ]]>
    /// </code>
    /// </example>
#if !SILVERLIGHT
    [ValueConversion(typeof(object), typeof(object))]
#endif
    public class TypeConverter : DependencyObject, IValueConverter
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(TypeConverter));

        /// <summary>
        /// Identifies the <see cref="SourceType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceTypeProperty = DependencyProperty.Register("SourceType",
            typeof(Type),
            typeof(TypeConverter),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="TargetType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetTypeProperty = DependencyProperty.Register("TargetType",
            typeof(Type),
            typeof(TypeConverter),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the source type for the conversion.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("sourceType")]
#endif
        public Type SourceType
        {
            get
            {
                return GetValue(SourceTypeProperty) as Type;
            }
            set
            {
                SetValue(SourceTypeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the target type for the conversion.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("targetType")]
#endif
        public Type TargetType
        {
            get
            {
                return GetValue(TargetTypeProperty) as Type;
            }
            set
            {
                SetValue(TargetTypeProperty, value);
            }
        }

        /// <summary>
        /// Constructs an instance of <c>TypeConverter</c>.
        /// </summary>
        public TypeConverter()
        {
        }

        /// <summary>
        /// Constructs an instance of <c>TypeConverter</c> with the specified source and target types.
        /// </summary>
        /// <param name="sourceType">
        /// The source type (see <see cref="SourceType"/>).
        /// </param>
        /// <param name="targetType">
        /// The target type (see <see cref="TargetType"/>).
        /// </param>
        public TypeConverter(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
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
            exceptionHelper.ResolveAndThrowIf(TargetType == null, "NoTargetType");
            return DoConversion(value, TargetType, culture);
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
            exceptionHelper.ResolveAndThrowIf(SourceType == null, "NoSourceType");
            return DoConversion(value, SourceType, culture);
        }

        private static object DoConversion(object value, Type toType, CultureInfo culture)
        {
            if ((value is IConvertible) || (value == null))
            {
                try
                {
                    return System.Convert.ChangeType(value, toType, culture);
                }
                catch (Exception)
                {
                    return DependencyProperty.UnsetValue;
                }
            }
#if !SILVERLIGHT
            else
            {
                System.ComponentModel.TypeConverter typeConverter = TypeDescriptor.GetConverter(value);

                if (typeConverter.CanConvertTo(toType))
                {
                    return typeConverter.ConvertTo(null, culture, value, toType);
                }
            }
#endif

            return DependencyProperty.UnsetValue;
        }
    }
}