namespace Kent.Boogaart.Converters.Expressions
{
    using System;
    using System.Diagnostics;

    internal sealed class Token
    {
        private readonly TokenType type;
        private readonly string value;

        public Token(TokenType type, string value)
        {
            Debug.Assert(Enum.IsDefined(typeof(TokenType), type));
            Debug.Assert(value != null);

            this.type = type;
            this.value = value;
        }

        public TokenType Type
        {
            get { return this.type; }
        }

        public string Value
        {
            get { return this.value; }
        }

        public bool Equals(TokenType tokenType, string value)
        {
            Debug.Assert(Enum.IsDefined(typeof(TokenType), tokenType));
            Debug.Assert(value != null);

            return (this.type == tokenType) && string.Equals(this.value, value, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return string.Concat(this.type, ": '", this.value, "'");
        }
    }
}