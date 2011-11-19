using System;
using System.Runtime.Serialization;

namespace Kent.Boogaart.Converters.Expressions
{
    /// <summary>
    /// Exception thrown when the <see cref="Parser"/> encounters any errors.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public class ParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ParseException class.
        /// </summary>
        public ParseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ParseException class with the specified message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ParseException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ParseException class with the specified message and inner exception.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The exception that caused this exception to be thrown.
        /// </param>
        public ParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !SILVERLIGHT
        /// <summary>
        /// Initializes a new instance of the ParseException class with information read from the provided serialization information.
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> that holds all the data needed to serialize or deserialize an object.
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> structure that provides a context for the operation.
        /// </param>
        protected ParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
