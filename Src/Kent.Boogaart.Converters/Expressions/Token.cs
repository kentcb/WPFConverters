using System;
using System.Diagnostics;

namespace Kent.Boogaart.Converters.Expressions
{
	internal sealed class Token
	{
		private readonly TokenType _type;
		private readonly string _value;

		public TokenType Type
		{
			get
			{
				return _type;
			}
		}

		public string Value
		{
			get
			{
				return _value;
			}
		}

		public Token(TokenType type, string value)
		{
			Debug.Assert(Enum.IsDefined(typeof(TokenType), type));
			Debug.Assert(value != null);
			_type = type;
			_value = value;
		}

		public bool Equals(TokenType tokenType, string value)
		{
			Debug.Assert(Enum.IsDefined(typeof(TokenType), tokenType));
			Debug.Assert(value != null);
			return (_type == tokenType) && string.Equals(_value, value, StringComparison.InvariantCulture);
		}

		public override string ToString()
		{
			return string.Concat(_type, ": '", _value, "'");
		}
	}
}