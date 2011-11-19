using Kent.Boogaart.Converters.Expressions;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions
{
    public sealed class TokenTest : UnitTest
    {
        private Token token;

        [Fact]
        public void Constructor_ShouldAssignGivenValues()
        {
            this.token = new Token(TokenType.Symbol, "fu");
            Assert.Equal(TokenType.Symbol, this.token.Type);
            Assert.Equal("fu", this.token.Value);

            this.token = new Token(TokenType.Word, "bar");
            Assert.Equal(TokenType.Word, this.token.Type);
            Assert.Equal("bar", this.token.Value);
        }

        [Fact]
        public void Equals_ShouldCompareTypeAndValue()
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
        public void ToString_ShouldYieldDebuggingString()
        {
            this.token = new Token(TokenType.Number, "123");
            Assert.Equal("Number: '123'", this.token.ToString());
            this.token = new Token(TokenType.Symbol, "{");
            Assert.Equal("Symbol: '{'", this.token.ToString());
        }
    }
}
