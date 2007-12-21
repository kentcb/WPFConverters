using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Kent.Boogaart.Converters
{
	/// <summary>
	/// Defines possible fallback behaviors for the <see cref="MapConverter"/>.
	/// </summary>
	public enum FallbackBehavior
	{
		/// <summary>
		/// Specifies that <see cref="DependencyProperty.UnsetValue"/> should be returned when falling back.
		/// </summary>
		ReturnUnsetValue,
		/// <summary>
		/// Specifies that the value being converted should be returned when falling back.
		/// </summary>
		ReturnOriginalValue
	}
}
