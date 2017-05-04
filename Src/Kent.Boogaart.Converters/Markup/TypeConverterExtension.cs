#if !SILVERLIGHT

namespace Kent.Boogaart.Converters.Markup
{
    using System;
    using System.Windows.Markup;

    /// <summary>
    /// Implements a markup extension that allows instances of <see cref="TypeConverter"/> to be easily created.
    /// </summary>
    /// <remarks>
    /// This markup extension allows instance of <see cref="TypeConverter"/> to be easily created inline in a XAML binding. See
    /// the example below.
    /// </remarks>
    /// <example>
    /// The following shows how to use the <c>TypeConverterExtension</c> inside a binding to convert integer values to strings
    /// and back:
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox Text="{Binding Age, Converter={TypeConverter sys:Int32, sys:String}}"/>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class TypeConverterExtension : MarkupExtension
    {
        private readonly TypeExtension sourceTypeExtension;
        private readonly TypeExtension targetTypeExtension;

        /// <summary>
        /// Initializes a new instance of the TypeConverterExtension class.
        /// </summary>
        public TypeConverterExtension()
        {
            this.sourceTypeExtension = new TypeExtension();
            this.targetTypeExtension = new TypeExtension();
        }

        /// <summary>
        /// Initializes a new instance of the TypeConverterExtension class with the specified source and target types.
        /// </summary>
        /// <param name="sourceType">
        /// The source type for the <see cref="TypeConverter"/>.
        /// </param>
        /// <param name="targetType">
        /// The target type for the <see cref="TypeConverter"/>.
        /// </param>
        public TypeConverterExtension(Type sourceType, Type targetType)
        {
            this.sourceTypeExtension = new TypeExtension(sourceType);
            this.targetTypeExtension = new TypeExtension(targetType);
        }

        /// <summary>
        /// Initializes a new instance of the TypeConverterExtension class with the specified source and target types.
        /// </summary>
        /// <param name="sourceTypeName">
        /// The source type name for the <see cref="TypeConverter"/>.
        /// </param>
        /// <param name="targetTypeName">
        /// The target type name for the <see cref="TypeConverter"/>.
        /// </param>
        public TypeConverterExtension(string sourceTypeName, string targetTypeName)
        {
            this.sourceTypeExtension = new TypeExtension(sourceTypeName);
            this.targetTypeExtension = new TypeExtension(targetTypeName);
        }

        /// <summary>
        /// Gets or sets the source type for the <see cref="TypeConverter"/>.
        /// </summary>
        [ConstructorArgument("sourceType")]
        public Type SourceType
        {
            get { return this.sourceTypeExtension.Type; }
            set { this.sourceTypeExtension.Type = value; }
        }

        /// <summary>
        /// Gets or sets the target type for the <see cref="TypeConverter"/>.
        /// </summary>
        [ConstructorArgument("targetType")]
        public Type TargetType
        {
            get { return this.targetTypeExtension.Type; }
            set { this.targetTypeExtension.Type = value; }
        }

        /// <summary>
        /// Gets or sets the name of the source type for the <see cref="TypeConverter"/>.
        /// </summary>
        [ConstructorArgument("sourceTypeName")]
        public string SourceTypeName
        {
            get { return this.sourceTypeExtension.TypeName; }
            set { this.sourceTypeExtension.TypeName = value; }
        }

        /// <summary>
        /// Gets or sets the name of the target type for the <see cref="TypeConverter"/>.
        /// </summary>
        [ConstructorArgument("targetTypeName")]
        public string TargetTypeName
        {
            get { return this.targetTypeExtension.TypeName; }
            set { this.targetTypeExtension.TypeName = value; }
        }

        /// <summary>
        /// Provides an instance of <see cref="TypeConverter"/> based on this <c>TypeConverterExtension</c>.
        /// </summary>
        /// <param name="serviceProvider">
        /// An object that can provide services.
        /// </param>
        /// <returns>
        /// The instance of <see cref="TypeConverter"/>.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Type sourceType = null;
            Type targetType = null;

            if ((this.sourceTypeExtension.Type != null) || (this.sourceTypeExtension.TypeName != null))
            {
                sourceType = this.sourceTypeExtension.ProvideValue(serviceProvider) as Type;
            }

            if ((this.targetTypeExtension.Type != null) || (this.targetTypeExtension.TypeName != null))
            {
                targetType = this.targetTypeExtension.ProvideValue(serviceProvider) as Type;
            }

            // just let the TypeExtensions do the type resolving via the service provider
            return new TypeConverter(sourceType, targetType);
        }
    }
}

#endif