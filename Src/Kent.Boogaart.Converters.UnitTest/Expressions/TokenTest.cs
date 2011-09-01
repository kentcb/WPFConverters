using Xunit;
using Kent.Boogaart.Converters.Expressions;

namespace Kent.Boogaart.Converters.UnitTest.Expressions
{
	public sealed class TokenTest : UnitTest
	{
		private Token _token;

		[Fact]
		public void Constructor_ShouldAssignGivenValues()
		{
			_token = new Token(TokenType.Symbol, "fu");
			Assert.Equal(TokenType.Symbol, _token.Type);
			Assert.Equal("fu", _token.Value);

			_token = new Token(TokenType.Word, "bar");
			Assert.Equal(TokenType.Word, _token.Type);
			Assert.Equal("bar", _token.Value);
		}

		[Fact]
		public void Equals_ShouldCompareTypeAndValue()
		{
			_token = new Token(TokenType.Number, "123");
			Assert.False(_token.Equals(TokenType.Number, "123 "));
			Assert.False(_token.Equals(TokenType.Word, "123"));
			Assert.True(_token.Equals(TokenType.Number, "123"));

			_token = new Token(TokenType.Symbol, "*");
			Assert.False(_token.Equals(TokenType.Symbol, "/"));
			Assert.False(_token.Equals(TokenType.Number, "*"));
			Assert.True(_token.Equals(TokenType.Symbol, "*"));
		}

		[Fact]
		public void ToString_ShouldYieldDebuggingString()
		{
			_token = new Token(TokenType.Number, "123");
			Assert.Equal("Number: '123'", _token.ToString());
			_token = new Token(TokenType.Symbol, "{");
			Assert.Equal("Symbol: '{'", _token.ToString());
		}
	}
}
