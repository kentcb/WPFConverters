using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace Kent.Boogaart.Converters.Markup
{
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
		private readonly TypeExtension _sourceTypeExtension;
		private readonly TypeExtension _targetTypeExtension;

		/// <summary>
		/// Gets or sets the source type for the <see cref="TypeConverter"/>.
		/// </summary>
		[ConstructorArgument("sourceType")]
		public Type SourceType
		{
			get
			{
				return _sourceTypeExtension.Type;
			}
			set
			{
				_sourceTypeExtension.Type = value;
			}
		}

		/// <summary>
		/// Gets or sets the target type for the <see cref="TypeConverter"/>.
		/// </summary>
		[ConstructorArgument("targetType")]
		public Type TargetType
		{
			get
			{
				return _targetTypeExtension.Type;
			}
			set
			{
				_targetTypeExtension.Type = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the source type for the <see cref="TypeConverter"/>.
		/// </summary>
		[ConstructorArgument("sourceTypeName")]
		public string SourceTypeName
		{
			get
			{
				return _sourceTypeExtension.TypeName;
			}
			set
			{
				_sourceTypeExtension.TypeName = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the target type for the <see cref="TypeConverter"/>.
		/// </summary>
		[ConstructorArgument("targetTypeName")]
		public string TargetTypeName
		{
			get
			{
				return _targetTypeExtension.TypeName;
			}
			set
			{
				_targetTypeExtension.TypeName = value;
			}
		}

		/// <summary>
		/// Constructs a default instance of the <c>TypeConverterExtension</c> class.
		/// </summary>
		public TypeConverterExtension()
		{
			_sourceTypeExtension = new TypeExtension();
			_targetTypeExtension = new TypeExtension();
		}

		/// <summary>
		/// Constructs an instance of <c>TypeConverterExtension</c> with the specified source and target types.
		/// </summary>
		/// <param name="sourceType">
		/// The source type for the <see cref="TypeConverter"/>.
		/// </param>
		/// <param name="targetType">
		/// The target type for the <see cref="TypeConverter"/>.
		/// </param>
		public TypeConverterExtension(Type sourceType, Type targetType)
		{
			_sourceTypeExtension = new TypeExtension(sourceType);
			_targetTypeExtension = new TypeExtension(targetType);
		}

		/// <summary>
		/// Constructs an instance of <c>TypeConverterExtension</c> with the specified source and target types.
		/// </summary>
		/// <param name="sourceTypeName">
		/// The source type name for the <see cref="TypeConverter"/>.
		/// </param>
		/// <param name="targetTypeName">
		/// The target type name for the <see cref="TypeConverter"/>.
		/// </param>
		public TypeConverterExtension(string sourceTypeName, string targetTypeName)
		{
			_sourceTypeExtension = new TypeExtension(sourceTypeName);
			_targetTypeExtension = new TypeExtension(targetTypeName);
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

			if ((_sourceTypeExtension.Type != null) || (_sourceTypeExtension.TypeName != null))
			{
				sourceType = _sourceTypeExtension.ProvideValue(serviceProvider) as Type;
			}

			if ((_targetTypeExtension.Type != null) || (_targetTypeExtension.TypeName != null))
			{
				targetType = _targetTypeExtension.ProvideValue(serviceProvider) as Type;
			}

			//just let the TypeExtensions do the type resolving via the service provider
			return new TypeConverter(sourceType, targetType);
		}
	}
}
