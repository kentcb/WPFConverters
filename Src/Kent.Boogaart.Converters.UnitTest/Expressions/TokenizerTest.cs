using System.IO;
using NUnit.Framework;
using Rhino.Mocks;
using Kent.Boogaart.Converters.Expressions;

namespace Kent.Boogaart.Converters.UnitTest.Expressions
{
	[TestFixture]
	public sealed class TokenizerTest : UnitTest
	{
		private Tokenizer _tokenizer;

		[Test]
		public void Dispose_ShouldDisposeUnderlyingReader()
		{
			TextReader textReader = Mocks.PartialMock<TextReader>();
			textReader.Dispose();
			LastCall.Repeat.Once();
			Mocks.ReplayAll();

			_tokenizer = new Tokenizer(textReader);
			_tokenizer.Dispose();
		}

		[Test]
		public void ReadNextToken_ShouldReturnNullIfAtEndOfStream()
		{
			CreateTokenizer("");
			Assert.IsNull(_tokenizer.ReadNextToken());
		}

		[Test]
		public void ReadNextToken_ShouldReturnNullIfOnlyWhitespace()
		{
			CreateTokenizer("   \u0009  \u000d  ");
			Assert.IsNull(_tokenizer.ReadNextToken());
		}

		[Test]
		public void ReadNextToken_ShouldReturnSymbolTokensForSymbols()
		{
			CreateTokenizer("{} () + - * / ++ (( <<");
			Token token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("{", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("}", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("(", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual(")", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("+", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("-", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("*", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("/", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("++", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("(", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("(", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("<<", token.Value);

			Assert.IsNull(_tokenizer.ReadNextToken());
		}

		[Test]
		public void ReadNextToken_ShouldReturnNumberTokensForNumbers()
		{
			CreateTokenizer("1 2 2398 223.1 13d .8 123abc");
			Token token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("1", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("2", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("2398", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("223.1", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("13d", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual(".8", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("123abc", token.Value);

			Assert.IsNull(_tokenizer.ReadNextToken());
		}

		[Test]
		public void ReadNextToken_ShouldReturnStringForStrings()
		{
			CreateTokenizer(TokenizerTestResources.ReadNextToken_ShouldReturnStringForStrings);
			Token token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("one", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("two", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("three \" four", token.Value);
		}

		[Test]
		[ExpectedException(typeof(ParseException), ExpectedMessage="Unrecognized escape sequence.")]
		public void ReadNextToken_ShouldThrowIfUnrecognizedEscapeSequence()
		{
			CreateTokenizer(@"'\h'".Replace('\'', '"'));
			_tokenizer.ReadNextToken();
		}

		[Test]
		public void ReadNextToken_ShouldHandleAllEscapeSequences()
		{
			CreateTokenizer(TokenizerTestResources.ReadNextToken_ShouldHandleAllEscapeSequences);
			Token token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("\'\"\\\0\a\b\f\n\r\t\v", token.Value);	
		}

		[Test]
		public void ReadNextToken_ShouldReturnWordTokensForWords()
		{
			CreateTokenizer("a b abc abc123");
			Token token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Word, token.Type);
			Assert.AreEqual("a", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Word, token.Type);
			Assert.AreEqual("b", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Word, token.Type);
			Assert.AreEqual("abc", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Word, token.Type);
			Assert.AreEqual("abc123", token.Value);

			Assert.IsNull(_tokenizer.ReadNextToken());
		}

		[Test]
		public void ReadNextToken_ShouldHandleComplexExpressions()
		{
			CreateTokenizer(TokenizerTestResources.ReadNextToken_ShouldHandleComplexExpressions);
			Token token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("1", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("+", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("2", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("*", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("(", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("15", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("/", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("{", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("0", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("}", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual(")", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("+", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("(", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("(", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Word, token.Type);
			Assert.AreEqual("int", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual(")", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("{", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("1", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("}", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("*", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("{", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("2", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("}", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual(")", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("<<", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Number, token.Type);
			Assert.AreEqual("3", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("==", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("something", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.Symbol, token.Type);
			Assert.AreEqual("+", token.Value);

			token = _tokenizer.ReadNextToken();
			Assert.AreEqual(TokenType.String, token.Type);
			Assert.AreEqual("something else", token.Value);

			Assert.IsNull(_tokenizer.ReadNextToken());
		}

		#region Supporting Methods

		private void CreateTokenizer(string input)
		{
			_tokenizer = new Tokenizer(new StringReader(input));
		}

		#endregion
	}
}
