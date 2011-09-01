using System;

namespace Kent.Boogaart.Converters
{
	/// <summary>
	/// Used to specify how the <see cref="DateTimeConverter"/> should convert <see cref="DateTime"/>s between different
	/// <see cref="DateTimeKind"/>s.
	/// </summary>
	public enum DateTimeConversionMode
	{
		/// <summary>
		/// The <see cref="DateTime.ToLocalTime"/> or <see cref="DateTime.ToUniversalTime"/> methods are called as necessary. The
		/// <see cref="DateTime"/>'s value is adjusted accordingly.
		/// </summary>
		DoConversion,

		/// <summary>
		/// The <see cref="DateTime.SpecifyKind"/> method is used to change the kind of the <see cref="DateTime"/> without affecting
		/// its value.
		/// </summary>
		SpecifyKindOnly
	}
}
