using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kent.Boogaart.Converters
{
	/// <summary>
	/// Represents a single step in a <see cref="MultiConverterGroup"/>.
	/// </summary>
	[ContentProperty("Converters")]
	public class MultiConverterGroupStep : DependencyObject
	{
		private static readonly DependencyPropertyKey _convertersPropertyKey = DependencyProperty.RegisterReadOnly("Converters",
			typeof(Collection<IMultiValueConverter>),
			typeof(MultiConverterGroupStep),
			new FrameworkPropertyMetadata());

		/// <summary>
		/// Identifies the <see cref="Converters"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty ConvertersProperty = _convertersPropertyKey.DependencyProperty;

		/// <summary>
		/// Gets the collection of <see cref="IMultiValueConverter"/>s in this <c>MultiConverterGroupStep</c>.
		/// </summary>
		public Collection<IMultiValueConverter> Converters
		{
			get
			{
				return GetValue(ConvertersProperty) as Collection<IMultiValueConverter>;
			}
			private set
			{
				SetValue(_convertersPropertyKey, value);
			}
		}

		/// <summary>
		/// Constructs an instance of <c>MultiConverterGroupStep</c>.
		/// </summary>
		public MultiConverterGroupStep()
		{
			Converters = new Collection<IMultiValueConverter>();
		}
	}
}
