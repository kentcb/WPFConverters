namespace Kent.Boogaart.Converters.UnitTests.Expressions
{
    using Kent.Boogaart.Converters.Expressions;
    using Xunit;

    public sealed class TokenFixture
    {
        private Token token;

        [Fact]
        public void ctor_that_takes_type_and_value_sets_type_and_value()
        {
            this.token = new Token(TokenType.Symbol, "fu");
            Assert.Equal(TokenType.Symbol, this.token.Type);
            Assert.Equal("fu", this.token.Value);

            this.token = new Token(TokenType.Word, "bar");
            Assert.Equal(TokenType.Word, this.token.Type);
            Assert.Equal("bar", this.token.Value);
        }

        [Fact]
        public void equals_compares_type_and_value()
        {
            this.token = new Token(TokenType.Number, "123");
            Assert.False(this.token.Equals(TokenType.Number, "123 "));
            Assert.False(this.token.Equals(TokenType.Word, "123"));
            Assert.True(this.token.Equals(TokenType.Number, "123"));

            this.token = new Token(TokenType.Symbol, "*");
            Assert.False(this.token.Equals(TokenType.Symbol, "/"));
            Assert.False(this.token.Equals(TokenType.Number, "*"));
            Assert.True(this.token.Equals(TokenType.Symbol, "*"));
        }

        [Fact]
        public void to_string_returns_a_debugging_string()
        {
            this.token = new Token(TokenType.Number, "123");
            Assert.Equal("Number: '123'", this.token.ToString());
            this.token = new Token(TokenType.Symbol, "{");
            Assert.Equal("Symbol: '{'", this.token.ToString());
        }
    }
}
