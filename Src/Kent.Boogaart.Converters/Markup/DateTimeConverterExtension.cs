#if !SILVERLIGHT40

namespace Kent.Boogaart.Converters.Markup
{
    using Kent.Boogaart.HelperTrinity.Extensions;
    using System;
    using System.Windows.Markup;

    /// <summary>
    /// Implements a markup extension that allows instances of <see cref="DateTimeConverter"/> to be easily created.
    /// </summary>
    /// <remarks>
    /// This markup extension allows instance of <see cref="DateTimeConverter"/> to be easily created inline in a XAML binding.
    /// See the example below.
    /// </remarks>
    /// <example>
    /// The following shows how to use the <c>DateTimeConverterExtension</c> inside a binding to convert values to local time:
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding StartTime, Converter={DateTimeConverter TargetKind=Local}}"/>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class DateTimeConverterExtension : MarkupExtension
    {
        private DateTimeKind sourceKind;
        private DateTimeKind targetKind;
        private DateTimeConversionMode conversionMode;
        private TimeSpan sourceAdjustment;
        private TimeSpan targetAdjustment;

        /// <summary>
        /// Gets or sets the source kind for the <see cref="DateTimeConverter"/>.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("sourceKind")]
#endif
        public DateTimeKind SourceKind
        {
            get
            {
                return this.sourceKind;
            }

            set
            {
                value.AssertEnumMember("value", DateTimeKind.Local, DateTimeKind.Unspecified, DateTimeKind.Utc);
                this.sourceKind = value;
            }
        }

        /// <summary>
        /// Gets or sets the target kind for the <see cref="DateTimeConverter"/>.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("targetKind")]
#endif
        public DateTimeKind TargetKind
        {
            get
            {
                return this.targetKind;
            }

            set
            {
                value.AssertEnumMember("value", DateTimeKind.Local, DateTimeKind.Unspecified, DateTimeKind.Utc);
                this.targetKind = value;
            }
        }

        /// <summary>
        /// Gets or sets the conversion mode for the <see cref="DateTimeConverter"/>.
        /// </summary>
        public DateTimeConversionMode ConversionMode
        {
            get
            {
                return this.conversionMode;
            }

            set
            {
                value.AssertEnumMember("value", DateTimeConversionMode.DoConversion, DateTimeConversionMode.SpecifyKindOnly);
                this.conversionMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the source adjustment for the <see cref="DateTimeConverter"/>.
        /// </summary>
        public TimeSpan SourceAdjustment
        {
            get { return this.sourceAdjustment; }
            set { this.sourceAdjustment = value; }
        }

        /// <summary>
        /// Gets or sets the target adjustment for the <see cref="DateTimeConverter"/>.
        /// </summary>
        public TimeSpan TargetAdjustment
        {
            get { return this.targetAdjustment; }
            set { this.targetAdjustment = value; }
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeConverterExtension class.
        /// </summary>
        public DateTimeConverterExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DateTimeConverterExtension class with the specified source and target kinds.
        /// </summary>
        /// <param name="sourceKind">
        /// The source kind for the <see cref="DateTimeConverter"/>.
        /// </param>
        /// <param name="targetKind">
        /// The target kind for the <see cref="DateTimeConverter"/>.
        /// </param>
        public DateTimeConverterExtension(DateTimeKind sourceKind, DateTimeKind targetKind)
        {
            this.SourceKind = sourceKind;
            this.TargetKind = targetKind;
        }

        /// <summary>
        /// Provides an instance of <see cref="DateTimeConverter"/> based on this <c>DateTimeConverterExtension</c>.
        /// </summary>
        /// <param name="serviceProvider">
        /// An object that can provide services.
        /// </param>
        /// <returns>
        /// The instance of <see cref="DateTimeConverter"/>.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var dateTimeConverter = new DateTimeConverter(this.SourceKind, this.TargetKind);
            dateTimeConverter.ConversionMode = this.ConversionMode;
            dateTimeConverter.SourceAdjustment = this.SourceAdjustment;
            dateTimeConverter.TargetAdjustment = this.TargetAdjustment;

            return dateTimeConverter;
        }
    }
}

#endif