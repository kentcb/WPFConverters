#if !SILVERLIGHT40

namespace Kent.Boogaart.Converters.Markup
{
    using Kent.Boogaart.HelperTrinity.Extensions;
    using System;
    using System.Windows.Controls;
    using System.Windows.Markup;

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
        private CharacterCasing sourceCasing;
        private CharacterCasing targetCasing;

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
        /// Initializes a new instance of the CaseConverterExtension class with the specified source and target <see cref="Casing"/>.
        /// </summary>
        /// <param name="sourceCasing">
        /// The source casing for the <see cref="CaseConverter"/>.
        /// </param>
        /// <param name="targetCasing">
        /// The target casing for the <see cref="CaseConverter"/>.
        /// </param>
        public CaseConverterExtension(CharacterCasing sourceCasing, CharacterCasing targetCasing)
        {
            this.SourceCasing = sourceCasing;
            this.TargetCasing = targetCasing;
        }

        /// <summary>
        /// Gets or sets the source <see cref="CharacterCasing"/> for the <see cref="CaseConverter"/>.
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
                value.AssertEnumMember("value", CharacterCasing.Lower, CharacterCasing.Normal, CharacterCasing.Upper);
                this.sourceCasing = value;
            }
        }

        /// <summary>
        /// Gets or sets the target <see cref="CharacterCasing"/> for the <see cref="CaseConverter"/>.
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
                value.AssertEnumMember("value", CharacterCasing.Lower, CharacterCasing.Normal, CharacterCasing.Upper);
                this.targetCasing = value;
            }
        }

        /// <summary>
        /// Sets both the source and target <see cref="CharacterCasing"/> for the <see cref="CaseConverter"/>.
        /// </summary>
        public CharacterCasing Casing
        {
            set
            {
                value.AssertEnumMember("value", CharacterCasing.Lower, CharacterCasing.Normal, CharacterCasing.Upper);
                this.sourceCasing = value;
                this.targetCasing = value;
            }
        }

        /// <summary>
        /// Provides an instance of <see cref="CaseConverter"/> based on <see cref="SourceCasing"/> and <see cref="TargetCasing"/>.
        /// </summary>
        /// <param name="serviceProvider">
        /// An object that can provide services.
        /// </param>
        /// <returns>
        /// The instance of <see cref="CaseConverter"/>.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new CaseConverter(this.SourceCasing, this.TargetCasing);
        }
    }
}

#endif