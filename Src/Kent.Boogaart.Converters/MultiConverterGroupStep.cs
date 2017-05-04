#if !SILVERLIGHT

namespace Kent.Boogaart.Converters
{
    using System.Collections.ObjectModel;
    using System.Windows.Data;
    using System.Windows.Markup;

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
}

#endif