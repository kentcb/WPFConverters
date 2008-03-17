using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters
{
	/// <summary>
	/// An implementation of <see cref="IValueConverter"/> and <see cref="IMultiValueConverter"/> that formats any bound data with a specified
	/// <see cref="FormatString"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The <c>FormatConverter</c> class can be used to format bound data according to standard .NET formatting rules. It implements both the
	/// <see cref="IValueConverter"/> and <see cref="IMultiValueConverter"/> interfaces, thus enabling usage in both <c>Binding</c>s and
	/// <c>MultiBinding</c>s.
	/// </para>
	/// <para>
	/// Note that this converter does not support conversions back for its <see cref="IMultiValueConverter"/> implementation. Any attempt to
	/// convert multiple values back will return <see langword="null"/>.
	/// </para>
	/// </remarks>
	/// <example>
	/// The following example shows how a <c>FormatConverter</c> can be used to format a single value:
	/// <code lang="xml">
	/// <![CDATA[
	/// <Label Content="{Binding Name, Converter={FormatConverter {}Your name is '{0}'.}}"/>
	/// ]]>
	/// </code>
	/// </example>
	/// <example>
	/// The following example shows how a <c>FormatConverter</c> can be used to format multiple values:
	/// <code lang="xml">
	/// <![CDATA[
	/// <Label>
	///		<Label.Content>
	///			<MultiBinding Converter="{FormatConverter {}Your name is '{0}' and you were born on {1:dd/MM/yyyy}.}">
	///				<Binding Path="Name"/>
	///				<Binding Path="Dob"/>
	///			</MultiBinding>
	///		</Label.Content>
	/// </Label>
	/// ]]>
	/// </code>
	/// </example>
	[ContentProperty("FormatString")]
	[ValueConversion(typeof(object), typeof(string))]
	public class FormatConverter : DependencyObject, IValueConverter, IMultiValueConverter
	{
		/// <summary>
		/// Identifies the <see cref="FormatString"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty FormatStringProperty = DependencyProperty.Register("FormatString",
			typeof(string),
			typeof(FormatConverter));

		/// <summary>
		/// Gets or sets the format string to use when converting bound data.
		/// </summary>
		[ConstructorArgument("formatString")]
		public string FormatString
		{
			get
			{
				return GetValue(FormatStringProperty) as string;
			}
			set
			{
				SetValue(FormatStringProperty, value);
			}
		}

		/// <summary>
		/// Constructs a default instance of <c>FormatConverter</c>.
		/// </summary>
		public FormatConverter()
		{
		}

		/// <summary>
		/// Constructs an instance of <c>FormatConverter</c> with the specified format string.
		/// </summary>
		/// <param name="formatString">
		/// The format string.
		/// </param>
		public FormatConverter(string formatString)
		{
			FormatString = formatString;
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
			ExceptionHelper.ThrowIf(FormatString == null, "NoFormatString");
			return string.Format(culture, FormatString, value);
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
			ArgumentHelper.AssertNotNull(targetType, "targetType");

			try
			{
				return System.Convert.ChangeType(value, targetType, culture);
			}
			catch (Exception)
			{
				return DependencyProperty.UnsetValue;
			}
		}

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
			ExceptionHelper.ThrowIf(FormatString == null, "NoFormatString");
			return string.Format(culture, FormatString, values);
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
	}
}