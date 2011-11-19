using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kent.Boogaart.Converters
{
#if !SILVERLIGHT
    /// <summary>
    /// Represents a single step in a <see cref="MultiConverterGroup"/>.
    /// </summary>
    [ContentProperty("Converters")]
    public class MultiConverterGroupStep : DependencyObject
    {
        private static readonly DependencyPropertyKey convertersPropertyKey = DependencyProperty.RegisterReadOnly(
            "Converters",
            typeof(Collection<IMultiValueConverter>),
            typeof(MultiConverterGroupStep),
            new FrameworkPropertyMetadata());

        /// <summary>
        /// Identifies the <see cref="Converters"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ConvertersProperty = convertersPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets the collection of <see cref="IMultiValueConverter"/>s in this <c>MultiConverterGroupStep</c>.
        /// </summary>
        public Collection<IMultiValueConverter> Converters
        {
            get { return GetValue(ConvertersProperty) as Collection<IMultiValueConverter>; }
            private set { SetValue(convertersPropertyKey, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MultiConverterGroupStep class.
        /// </summary>
        public MultiConverterGroupStep()
        {
            this.Converters = new Collection<IMultiValueConverter>();
        }
    }
#endif
}