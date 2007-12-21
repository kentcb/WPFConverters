using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Markup
{
	/// <summary>
	/// Implements a markup extension that allows instances of <see cref="FormatConverter"/> to be easily created.
	/// </summary>
	/// <remarks>
	/// This markup extension allows instance of <see cref="FormatConverter"/> to be easily created inline in a XAML binding.
	/// See the example below.
	/// </remarks>
	/// <example>
	/// The following shows how to use the <c>FormatConverterExtension</c> inside a binding to format values:
	/// <code lang="xml">
	/// <![CDATA[
	/// <Label Content="{Binding Name, Converter={FormatConverter {}Your name is '{0}'}}"/>
	/// ]]>
	/// </code>
	/// </example>
	public sealed class FormatConverterExtension : MarkupExtension
	{
		private string _formatString;

		/// <summary>
		/// Gets or sets the format string for the <see cref="FormatConverter"/>.
		/// </summary>
		[ConstructorArgument("formatString")]
		public string FormatString
		{
			get
			{
				return _formatString;
			}
			set
			{
				_formatString = value;
			}
		}

		/// <summary>
		/// Constructs a default instance of <c>FormatConverterExtension</c>.
		/// </summary>
		public FormatConverterExtension()
		{
		}

		/// <summary>
		/// Constructs an instance of <c>FormatConverterExtension</c> with the specified format string.
		/// </summary>
		/// <param name="formatString">
		/// The format string for the <see cref="FormatConverter"/>.
		/// </param>
		public FormatConverterExtension(string formatString)
		{
			_formatString = formatString;
		}

		/// <summary>
		/// Provides an instance of <see cref="FormatConverter"/> based on <see cref="FormatString"/>.
		/// </summary>
		/// <param name="serviceProvider">
		/// An object that can provide services.
		/// </param>
		/// <returns>
		/// The instance of <see cref="FormatConverter"/>.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			ExceptionHelper.ThrowIf(FormatString == null, "NoFormatString");
			return new FormatConverter(FormatString);
		}
	}
}
