namespace Kent.Boogaart.Converters
{
    using System.Windows.Markup;

    /// <summary>
    /// Represents a mapping <see cref="From"/> one value <see cref="To"/> another.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="MapConverter"/> uses instances of this class to define mappings between one set of values and another.
    /// </para>
    /// </remarks>
    public class Mapping
    {
        private object from;
        private object to;

        /// <summary>
        /// Gets or sets the source object for the mapping.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("from")]
#endif
        public object From
        {
            get { return this.from; }
            set { this.from = value; }
        }

        /// <summary>
        /// Gets or sets the destination object for the mapping.
        /// </summary>
#if !SILVERLIGHT
        [ConstructorArgument("to")]
#endif
        public object To
        {
            get { return this.to; }
            set { this.to = value; }
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
