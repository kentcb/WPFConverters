using System;
using System.Collections.Generic;
using System.Text;
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
		public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From",
			typeof(object),
			typeof(Mapping));

		/// <summary>
		/// Identifies the <see cref="To"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To",
			typeof(object),
			typeof(Mapping));

		/// <summary>
		/// Gets or sets the source object for the mapping.
		/// </summary>
		[ConstructorArgument("from")]
		public object From
		{
			get
			{
				return GetValue(FromProperty);
			}
			set
			{
				SetValue(FromProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the destination object for the mapping.
		/// </summary>
		[ConstructorArgument("to")]
		public object To
		{
			get
			{
				return GetValue(ToProperty);
			}
			set
			{
				SetValue(ToProperty, value);
			}
		}

		/// <summary>
		/// Constructs a default instance of <c>Mapping</c>.
		/// </summary>
		public Mapping()
		{
		}

		/// <summary>
		/// Constructs an instance of <c>Mapping</c> with the specified <paramref name="from"/> and <paramref name="to"/> values.
		/// </summary>
		/// <param name="from">
		/// The value for the source in the mapping (see <see cref="From"/>).
		/// </param>
		/// <param name="to">
		/// The value for the destination in the mapping (see <see cref="To"/>).
		/// </param>
		public Mapping(object from, object to)
		{
			From = from;
			To = to;
		}
	}
}
