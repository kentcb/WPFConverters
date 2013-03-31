#if !SILVERLIGHT40

namespace Kent.Boogaart.Converters.Markup
{
    using Kent.Boogaart.HelperTrinity;
    using System;
    using System.Windows.Markup;

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
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(FormatConverterExtension));
        private string formatString;

        /// <summary>
        /// Initializes a new instance of the FormatConverterExtension class.
        /// </summary>
        public FormatConverterExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the FormatConverterExtension class with the specified format string.
        /// </summary>
        /// <param name="formatString">
        /// The format string for the <see cref="FormatConverter"/>.
        /// </param>
        public FormatConverterExtension(string formatString)
        {
            this.formatString = formatString;
        }

        /// <summary>
        /// Gets or sets the format string for the <see cref="FormatConverter"/>.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("formatString")]
#endif
        public string FormatString
        {
            get { return this.formatString; }
            set { this.formatString = value; }
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
            exceptionHelper.ResolveAndThrowIf(this.FormatString == null, "NoFormatString");
            return new FormatConverter(this.FormatString);
        }
    }
}

#endif