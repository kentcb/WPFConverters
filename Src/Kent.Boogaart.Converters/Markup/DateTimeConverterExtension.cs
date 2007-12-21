using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Markup
{
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
		private DateTimeKind _sourceKind;
		private DateTimeKind _targetKind;
		private DateTimeConversionMode _conversionMode;
		private TimeSpan _sourceAdjustment;
		private TimeSpan _targetAdjustment;

		/// <summary>
		/// Gets or sets the source kind for the <see cref="DateTimeConverter"/>.
		/// </summary>
		[ConstructorArgument("sourceKind")]
		public DateTimeKind SourceKind
		{
			get
			{
				return _sourceKind;
			}
			set
			{
				ArgumentHelper.AssertEnumMember(value, "value");
				_sourceKind = value;
			}
		}

		/// <summary>
		/// Gets or sets the target kind for the <see cref="DateTimeConverter"/>.
		/// </summary>
		[ConstructorArgument("targetKind")]
		public DateTimeKind TargetKind
		{
			get
			{
				return _targetKind;
			}
			set
			{
				ArgumentHelper.AssertEnumMember(value, "value");
				_targetKind = value;
			}
		}

		/// <summary>
		/// Gets or sets the conversion mode for the <see cref="DateTimeConverter"/>.
		/// </summary>
		public DateTimeConversionMode ConversionMode
		{
			get
			{
				return _conversionMode;
			}
			set
			{
				ArgumentHelper.AssertEnumMember(value, "value");
				_conversionMode = value;
			}
		}

		/// <summary>
		/// Gets or sets the source adjustment for the <see cref="DateTimeConverter"/>.
		/// </summary>
		public TimeSpan SourceAdjustment
		{
			get
			{
				return _sourceAdjustment;
			}
			set
			{
				_sourceAdjustment = value;
			}
		}

		/// <summary>
		/// Gets or sets the target adjustment for the <see cref="DateTimeConverter"/>.
		/// </summary>
		public TimeSpan TargetAdjustment
		{
			get
			{
				return _targetAdjustment;
			}
			set
			{
				_targetAdjustment = value;
			}
		}

		/// <summary>
		/// Constructs a default instance of <c>DateTimeConverterExtension</c>.
		/// </summary>
		public DateTimeConverterExtension()
		{
		}

		/// <summary>
		/// Constructs an instance of <c>DateTimeConverterExtension</c> with the specified source and target kinds.
		/// </summary>
		/// <param name="sourceKind">
		/// The source kind for the <see cref="DateTimeConverter"/>.
		/// </param>
		/// <param name="targetKind">
		/// The target kind for the <see cref="DateTimeConverter"/>.
		/// </param>
		public DateTimeConverterExtension(DateTimeKind sourceKind, DateTimeKind targetKind)
		{
			SourceKind = sourceKind;
			TargetKind = targetKind;
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
			DateTimeConverter dateTimeConverter = new DateTimeConverter(SourceKind, TargetKind);
			dateTimeConverter.ConversionMode = ConversionMode;
			dateTimeConverter.SourceAdjustment = SourceAdjustment;
			dateTimeConverter.TargetAdjustment = TargetAdjustment;

			return dateTimeConverter;
		}
	}
}
