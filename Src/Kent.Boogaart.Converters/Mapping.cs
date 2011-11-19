using System.Windows;
using System.Windows.Markup;

namespace Kent.Boogaart.Converters
{
    /// <summary>
    /// Represents a mapping <see cref="From"/> one value <see cref="To"/> another.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="MapConverter"/> uses instances of this class to define mappings between one set of values and another.
    /// </para>
    /// </remarks>
    public class Mapping : DependencyObject
    {
        /// <summary>
        /// Identifies the <see cref="From"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            "From",
            typeof(object),
            typeof(Mapping),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="To"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            "To",
            typeof(object),
            typeof(Mapping),
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the source object for the mapping.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("from")]
#endif
        public object From
        {
            get { return GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        /// <summary>
        /// Gets or sets the destination object for the mapping.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("to")]
#endif
        public object To
        {
            get { return GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the Mapping class.
        /// </summary>
        public Mapping()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Mapping class with the specified <paramref name="from"/> and <paramref name="to"/> values.
        /// </summary>
        /// <param name="from">
        /// The value for the source in the mapping (see <see cref="From"/>).
        /// </param>
        /// <param name="to">
        /// The value for the destination in the mapping (see <see cref="To"/>).
        /// </param>
        public Mapping(object from, object to)
        {
            this.From = from;
            this.To = to;
        }
    }
}
