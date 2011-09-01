using System;
using System.IO;
using Kent.Boogaart.Converters.Expressions;
using Moq;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions
{
    public sealed class TokenizerTest : UnitTest
    {
        private Tokenizer _tokenizer;

        [Fact]
        public void ReadNextToken_ShouldReturnNullIfAtEndOfStream()
        {
            CreateTokenizer("");
            Assert.Null(_tokenizer.ReadNextToken());
        }

        [Fact]
        public void ReadNextToken_ShouldReturnNullIfOnlyWhitespace()
        {
            CreateTokenizer("   \u0009  \u000d  ");
            Assert.Null(_tokenizer.ReadNextToken());
        }

        [Fact]
        public void ReadNextToken_ShouldReturnSymbolTokensForSymbols()
        {
            CreateTokenizer("{} () + - * / ++ (( <<");
            Token token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("{", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("}", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("(", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal(")", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("+", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("-", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("*", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("/", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("++", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("(", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("(", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("<<", token.Value);

            Assert.Null(_tokenizer.ReadNextToken());
        }

        [Fact]
        public void ReadNextToken_ShouldReturnNumberTokensForNumbers()
        {
            CreateTokenizer("1 2 2398 223.1 13d .8 123abc");
            Token token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("1", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("2", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("2398", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("223.1", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("13d", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal(".8", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("123abc", token.Value);

            Assert.Null(_tokenizer.ReadNextToken());
        }

        [Fact]
        public void ReadNextToken_ShouldReturnStringForStrings()
        {
            CreateTokenizer(TokenizerTestResources.ReadNextToken_ShouldReturnStringForStrings);
            Token token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.String, token.Type);
            Assert.Equal("one", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.String, token.Type);
            Assert.Equal("two", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.String, token.Type);
            Assert.Equal("three \" four", token.Value);
        }

        [Fact]
        public void ReadNextToken_ShouldThrowIfUnrecognizedEscapeSequence()
        {
            CreateTokenizer(@"'\h'".Replace('\'', '"'));
            var ex = Assert.Throws<ParseException>(() => _tokenizer.ReadNextToken());
            Assert.Equal("Unrecognized escape sequence.", ex.Message);
        }

        [Fact]
        public void ReadNextToken_ShouldHandleAllEscapeSequences()
        {
            CreateTokenizer(TokenizerTestResources.ReadNextToken_ShouldHandleAllEscapeSequences);
            Token token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.String, token.Type);
            Assert.Equal("\'\"\\\0\a\b\f\n\r\t\v", token.Value);	
        }

        [Fact]
        public void ReadNextToken_ShouldReturnWordTokensForWords()
        {
            CreateTokenizer("a b abc abc123");
            Token token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Word, token.Type);
            Assert.Equal("a", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Word, token.Type);
            Assert.Equal("b", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Word, token.Type);
            Assert.Equal("abc", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Word, token.Type);
            Assert.Equal("abc123", token.Value);

            Assert.Null(_tokenizer.ReadNextToken());
        }

        [Fact]
        public void ReadNextToken_ShouldHandleComplexExpressions()
        {
            CreateTokenizer(TokenizerTestResources.ReadNextToken_ShouldHandleComplexExpressions);
            Token token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("1", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("+", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("2", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("*", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("(", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("15", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("/", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("{", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("0", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("}", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal(")", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("+", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("(", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("(", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Word, token.Type);
            Assert.Equal("int", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal(")", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("{", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("1", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("}", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("*", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("{", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("2", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("}", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal(")", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("<<", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Number, token.Type);
            Assert.Equal("3", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("==", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.String, token.Type);
            Assert.Equal("something", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.Symbol, token.Type);
            Assert.Equal("+", token.Value);

            token = _tokenizer.ReadNextToken();
            Assert.Equal(TokenType.String, token.Type);
            Assert.Equal("something else", token.Value);

            Assert.Null(_tokenizer.ReadNextToken());
        }

        #region Supporting Methods

        private void CreateTokenizer(string input)
        {
            _tokenizer = new Tokenizer(new StringReader(input));
        }

        #endregion
    }
}
