using System;
using System.Windows.Controls;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Markup
{
#if !SILVERLIGHT
    /// <summary>
    /// Implements a markup extension that allows instances of <see cref="CaseConverter"/> to be easily created.
    /// </summary>
    /// <remarks>
    /// This markup extension allows instance of <see cref="CaseConverter"/> to be easily created inline in a XAML binding.
    /// See the example below.
    /// </remarks>
    /// <example>
    /// The following shows how to use the <c>CaseConverterExtension</c> inside a binding to convert values to upper-case:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter Upper}}"/>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class CaseConverterExtension : MarkupExtension
    {
        private CharacterCasing casing;

        /// <summary>
        /// Gets or sets the <see cref="CharacterCasing"/> for the <see cref="CaseConverter"/>.
        /// </summary>
        [ConstructorArgument("casing")]
        public CharacterCasing Casing
        {
            get
            {
                return this.casing;
            }

            set
            {
                ArgumentHelper.AssertEnumMember(value, "value");
                this.casing = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverterExtension class.
        /// </summary>
        public CaseConverterExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverterExtension class with the specified <see cref="Casing"/>.
        /// </summary>
        /// <param name="casing">
        /// The casing for the <see cref="CaseConverter"/>.
        /// </param>
        public CaseConverterExtension(CharacterCasing casing)
        {
            this.Casing = casing;
        }

        /// <summary>
        /// Provides an instance of <see cref="CaseConverter"/> based on <see cref="Casing"/>.
        /// </summary>
        /// <param name="serviceProvider">
        /// An object that can provide services.
        /// </param>
        /// <returns>
        /// The instance of <see cref="CaseConverter"/>.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new CaseConverter(this.Casing);
        }
    }
#endif
}