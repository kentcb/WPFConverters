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
    public class MultiConverterGroupStep
    {
        private readonly Collection<IMultiValueConverter> converters;

        /// <summary>
        /// Initializes a new instance of the MultiConverterGroupStep class.
        /// </summary>
        public MultiConverterGroupStep()
        {
            this.converters = new Collection<IMultiValueConverter>();
        }

        /// <summary>
        /// Gets the collection of <see cref="IMultiValueConverter"/>s in this <c>MultiConverterGroupStep</c>.
        /// </summary>
        public Collection<IMultiValueConverter> Converters
        {
            get { return this.converters; }
        }
    }
#endif
}