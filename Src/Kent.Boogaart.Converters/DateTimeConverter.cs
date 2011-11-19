using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that converts <see cref="DateTime"/>s between specified <see cref="DateTimeKind"/>s.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <c>DateTimeConverter</c> can be used to convert <see cref="DateTime"/>s between different <see cref="DateTimeKind"/>s as required by
    /// the source and target of a data binding. The <see cref="SourceKind"/> specifies the <see cref="DateTimeKind"/> that the source stores the
    /// <see cref="DateTime"/> in, whilst the <see cref="TargetKind"/> specifies the <see cref="DateTimeKind"/> that the target stores the
    /// <see cref="DateTime"/> in.
    /// </para>
    /// <para>
    /// If <see cref="DateTimeKind.Unspecified"/> is assigned to either <see cref="SourceKind"/> or <see cref="TargetKind"/> then no conversion
    /// will take place in that direction.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how a <c>DateTimeConverter</c> can be used to convert a UTC date to local time:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding StartDateTime, Converter={DateTimeConverter TargetKind=Local}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how a <c>DateTimeConverter</c> can be used to adjust a <c>DateTime</c> by 3 minutes:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding StartDateTime, Converter={DateTimeConverter TargetAdjustment=0:3}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how a <c>DateTimeConverter</c> can be used to convert <c>DateTime</c>s to local time for the
    /// user, but convert them back to UTC time before storing them in the bound object:
    /// <code lang="xml">
    /// <![CDATA[
    /// <DatePicker Value="{Binding StartDateTime, Converter={DateTimeConverter SourceKind=Utc, TargetKind=Local}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// The following example shows how a <c>DateTimeConverter</c> can be used to convert <c>DateTime</c>s to local time without
    /// adjusting the underlying value of the <c>DateTime</c>:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding StartDateTime, Converter={DateTimeConverter TargetKind=Local, ConversionMode=SpecifyKindOnly}}"/>
    /// ]]>
    /// </code>
    /// </example>
#if !SILVERLIGHT
    [ValueConversion(typeof(DateTime), typeof(DateTime))]
#endif
    public class DateTimeConverter : DependencyObject, IValueConverter
    {
        /// <summary>
        /// Identifies the <see cref="SourceKind"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceKindProperty = DependencyProperty.Register(
            "SourceKind",
            typeof(DateTimeKind),
            typeof(DateTimeConverter),
            new PropertyMetadata(DateTimeKind.Unspecified)
#if !SILVERLIGHT
            , ValidateDateTimeKind
#endif
            );

        /// <summary>
        /// Identifies the <see cref="TargetKind"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetKindProperty = DependencyProperty.Register(
            "TargetKind",
            typeof(DateTimeKind),
            typeof(DateTimeConverter),
            new PropertyMetadata(DateTimeKind.Unspecified)
#if !SILVERLIGHT
            , ValidateDateTimeKind
#endif
            );

        /// <summary>
        /// Identifies the <see cref="ConversionMode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ConversionModeProperty = DependencyProperty.Register(
            "ConversionMode",
            typeof(DateTimeConversionMode),
            typeof(DateTimeConverter),
            new PropertyMetadata(DateTimeConversionMode.DoConversion)
#if !SILVERLIGHT
            , ValidateDateTimeConversionMode
#endif
            );

        /// <summary>
        /// Identifies the <see cref="SourceAdjustment"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceAdjustmentProperty = DependencyProperty.Register(
            "SourceAdjustment",
            typeof(TimeSpan),
            typeof(DateTimeConverter),
            new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Identifies the <see cref="TargetAdjustment"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetAdjustmentProperty = DependencyProperty.Register(
            "TargetAdjustment",
            typeof(TimeSpan),
            typeof(DateTimeConverter),
            new PropertyMetadata(TimeSpan.Zero));

        /// <summary>
        /// Gets or sets the source kind for converted <see cref="DateTime"/>s.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property specifies what type of <see cref="DateTime"/> is required for the binding source. Any <see cref="DateTime"/>s converted
        /// before being applied to the source will be converted to the kind specified by this property.
        /// </para>
        /// </remarks>
#if !SILVERLIGHT
        [ConstructorArgument("sourceKind")]
#endif
        public DateTimeKind SourceKind
        {
            get { return (DateTimeKind)GetValue(SourceKindProperty); }
            set { SetValue(SourceKindProperty, value); }
        }

        /// <summary>
        /// Gets or sets the target kind for converted <see cref="DateTime"/>s.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property specifies what type of <see cref="DateTime"/> is required for the binding target. Any <see cref="DateTime"/>s converted
        /// before being applied to the target will be converted to the kind specified by this property.
        /// </para>
        /// </remarks>
#if !SILVERLIGHT
        [ConstructorArgument("targetKind")]
#endif
        public DateTimeKind TargetKind
        {
            get { return (DateTimeKind)GetValue(TargetKindProperty); }
            set { SetValue(TargetKindProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how <see cref="DateTime"/>s are converted between the <see cref="SourceKind"/> and
        /// <see cref="TargetKind"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property can be used to indicate how <see cref="DateTime"/>s should be converted between different
        /// <see cref="DateTimeKind"/>s. If set to <see cref="DateTimeConversionMode.DoConversion"/> (the default), the
        /// <see cref="DateTime"/>'s value will be adjusted during conversion. If set to
        /// <see cref="DateTimeConversionMode.SpecifyKindOnly"/>, the value of the <see cref="DateTime"/> is not adjusted during
        /// the conversion.
        /// </para>
        /// </remarks>
        public DateTimeConversionMode ConversionMode
        {
            get { return (DateTimeConversionMode)GetValue(ConversionModeProperty); }
            set { SetValue(ConversionModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value by which the source <see cref="DateTime"/> will be adjusted during conversions.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property can be used to ensure that any <see cref="DateTime"/> heading to the source of the binding will be
        /// adjusted according to the specified <see cref="TimeSpan"/>.
        /// </para>
        /// </remarks>
        public TimeSpan SourceAdjustment
        {
            get { return (TimeSpan)GetValue(SourceAdjustmentProperty); }
            set { SetValue(SourceAdjustmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value by which the target <see cref="DateTime"/> will be adjusted during conversions.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property can be used to ensure that any <see cref="DateTime"/> heading to the target of the binding will be
        /// adjusted according to the specified <see cref="TimeSpan"/>.
        /// </para>
        /// </remarks>
        public TimeSpan TargetAdjustment
        {
            get { return (TimeSpan)GetValue(TargetAdjustmentProperty); }
            set { SetValue(TargetAdjustmentProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeConverter class
        /// </summary>
        public DateTimeConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeConverter class with the specified source and target kinds.
        /// </summary>
        /// <param name="sourceKind">
        /// The source kind for converted <see cref="DateTime"/>s.
        /// </param>
        /// <param name="targetKind">
        /// The target kind for converted <see cref="DateTime"/>s.
        /// </param>
        public DateTimeConverter(DateTimeKind sourceKind, DateTimeKind targetKind)
        {
            this.SourceKind = sourceKind;
            this.TargetKind = targetKind;
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
            if (value is DateTime)
            {
                return DoConversion(this.ConversionMode, (DateTime)value, this.TargetKind, this.TargetAdjustment);
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
            if (value is DateTime)
            {
                return DoConversion(this.ConversionMode, (DateTime)value, this.SourceKind, this.SourceAdjustment);
            }

            return DependencyProperty.UnsetValue;
        }

        private static DateTime DoConversion(DateTimeConversionMode conversionMode, DateTime dateTime, DateTimeKind convertTo, TimeSpan adjustment)
        {
            if (adjustment != TimeSpan.Zero)
            {
                dateTime = dateTime.Add(adjustment);
            }

            if (conversionMode == DateTimeConversionMode.DoConversion)
            {
                switch (convertTo)
                {
                    case DateTimeKind.Local:
                        return dateTime.ToLocalTime();
                    case DateTimeKind.Utc:
                        return dateTime.ToUniversalTime();
                    default:
                        return dateTime;
                }
            }
            else
            {
                return DateTime.SpecifyKind(dateTime, convertTo);
            }
        }

        private static bool ValidateDateTimeKind(object value)
        {
            Debug.Assert(value is DateTimeKind);

            try
            {
                ArgumentHelper.AssertEnumMember((DateTimeKind)value, "value");
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateDateTimeConversionMode(object value)
        {
            Debug.Assert(value is DateTimeConversionMode);

            try
            {
                ArgumentHelper.AssertEnumMember((DateTimeConversionMode)value, "value");
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }
    }
}