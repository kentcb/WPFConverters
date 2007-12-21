using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions;

namespace Kent.Boogaart.Converters.UnitTest.Expressions
{
	[TestFixture]
	public sealed class TokenTest : UnitTest
	{
		private Token _token;

		[Test]
		public void Constructor_ShouldAssignGivenValues()
		{
			_token = new Token(TokenType.Symbol, "fu");
			Assert.AreEqual(TokenType.Symbol, _token.Type);
			Assert.AreEqual("fu", _token.Value);

			_token = new Token(TokenType.Word, "bar");
			Assert.AreEqual(TokenType.Word, _token.Type);
			Assert.AreEqual("bar", _token.Value);
		}

		[Test]
		public void Equals_ShouldCompareTypeAndValue()
		{
			_token = new Token(TokenType.Number, "123");
			Assert.IsFalse(_token.Equals(TokenType.Number, "123 "));
			Assert.IsFalse(_token.Equals(TokenType.Word, "123"));
			Assert.IsTrue(_token.Equals(TokenType.Number, "123"));

			_token = new Token(TokenType.Symbol, "*");
			Assert.IsFalse(_token.Equals(TokenType.Symbol, "/"));
			Assert.IsFalse(_token.Equals(TokenType.Number, "*"));
			Assert.IsTrue(_token.Equals(TokenType.Symbol, "*"));
		}

		[Test]
		public void ToString_ShouldYieldDebuggingString()
		{
			_token = new Token(TokenType.Number, "123");
			Assert.AreEqual("Number: '123'", _token.ToString());
			_token = new Token(TokenType.Symbol, "{");
			Assert.AreEqual("Symbol: '{'", _token.ToString());
		}
	}
}
