using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters
{
	/// <summary>
	/// An implementation of <see cref="IMultiValueConverter"/> that allows multiple <see cref="IMultiValueConverter"/>s to be chained together in
	/// a sequence of steps.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The <c>MultiConverterGroup</c> class allows <see cref="IMultiValueConverter"/> implementations to be chained together in various steps to
	/// produce an execution pipeline. Each step in the execution chain is represented by an instance of <see cref="MultiConverterGroupStep"/>,
	/// which must be added to the <see cref="Steps"/> collection.
	/// </para>
	/// <para>
	/// During a call to <see cref="Convert"/>, each step is handed a set of values. Each converter in that step is required to produce one value
	/// from the set of values. All values produced by the converters in a step are combined and form the input to the next step.
	/// </para>
	/// <para>
	/// During a call to <see cref="ConvertBack"/>, the steps are traversed in reverse order. Only the first converter in each step is used, since
	/// each converter in the step should map a single input value back to the same output values. The output values from the single converter in
	/// each step are fed as input into the previous step all the way up the stack. Of course, if any of the first converters in any of the steps
	/// do not support backward conversions, calling <see cref="ConvertBack"/> will not yield the desired results.
	/// </para>
	/// <para>
	/// Note that the final step in a <c>MultiConverterGroup</c> must have a single <see cref="IMultiValueConverter"/> instance added to it. In
	/// addition, all steps in a <c>MultiConverterGroup</c> must have at least one converter added to them. Violating either of these constraints
	/// will result in an exception at runtime.
	/// </para>
	/// </remarks>
	/// <example>
	/// The following example shows how multiple converters might be joined together to produce some useful output:
	/// <code lang="xml">
	/// <![CDATA[
	///	<MultiConverterGroup>
	///		<MultiConverterGroupStep>
	/// 		<NumberOfFilesConverter/>
	///			<TotalFileSizeConverter/>
	///		</MultiConverterGroupStep>
	///		<MultiConverterGroupStep>
	///			<FormatConverter FormatString="{0} files with a total size of {1}KB."/>
	///		</MultiConverterGroupStep>
	///	</MultiConverterGroup>
	/// ]]>
	/// </code>
	/// Such a converter might be used in a <c>MultiBinding</c> that is bound to a collection of files.
	/// </example>
	[ContentProperty("Steps")]
	[ValueConversion(typeof(object), typeof(object))]
	public class MultiConverterGroup : DependencyObject, IMultiValueConverter
	{
		private static readonly DependencyPropertyKey _stepsPropertyKey = DependencyProperty.RegisterReadOnly("Steps",
			typeof(Collection<MultiConverterGroupStep>),
			typeof(MultiConverterGroup),
			new FrameworkPropertyMetadata());

		/// <summary>
		/// Identifies the <see cref="Steps"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty StepsProperty = _stepsPropertyKey.DependencyProperty;

		/// <summary>
		/// Gets the collection of <see cref="MultiConverterGroupStep"/>s in this <c>MultiConverterGroup</c>.
		/// </summary>
		public Collection<MultiConverterGroupStep> Steps
		{
			get
			{
				return GetValue(StepsProperty) as Collection<MultiConverterGroupStep>;
			}
			private set
			{
				SetValue(_stepsPropertyKey, value);
			}
		}

		/// <summary>
		/// Constructs an instance of <c>MultiConverterGroup</c>.
		/// </summary>
		public MultiConverterGroup()
		{
			Steps = new Collection<MultiConverterGroupStep>();
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
			if (Steps.Count == 0)
			{
				return DependencyProperty.UnsetValue;
			}

			ExceptionHelper.ThrowIf(Steps[Steps.Count - 1].Converters.Count != 1, "FinalStepMustHaveOneConverter");

			foreach (MultiConverterGroupStep step in Steps)
			{
				ExceptionHelper.ThrowIf(step.Converters.Count == 0, "EachStepMustHaveAtLeastOneConverter");
				object[] convertedValues = new object[step.Converters.Count];

				for (int i = 0; i < step.Converters.Count; ++i)
				{
					convertedValues[i] = step.Converters[i].Convert(values, targetType, parameter, culture);
				}

				values = convertedValues;
			}

			Debug.Assert(values.Length == 1);
			return values[0];
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
			if (Steps.Count == 0)
			{
				return null;
			}

			ExceptionHelper.ThrowIf(Steps[Steps.Count - 1].Converters.Count != 1, "FinalStepMustHaveOneConverter");
			object[] stepValues = new object[] { value };

			for (int i = Steps.Count - 1; i >= 0; --i)
			{
				MultiConverterGroupStep step = Steps[i];
				ExceptionHelper.ThrowIf(step.Converters.Count == 0, "EachStepMustHaveAtLeastOneConverter");
				ExceptionHelper.ThrowIf(step.Converters.Count != stepValues.Length, "NumberOfConvertersInStepMustEqualNumberOfValuesFromPreviousStep", i + 1, stepValues.Length, i, step.Converters.Count);
				stepValues = step.Converters[0].ConvertBack(stepValues[0], targetTypes, parameter, culture);
			}

			return stepValues;
		}
	}
}