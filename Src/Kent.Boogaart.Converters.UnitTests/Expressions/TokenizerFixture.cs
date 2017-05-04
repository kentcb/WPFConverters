namespace Kent.Boogaart.Converters.UnitTests.Expressions
{
    using Kent.Boogaart.Converters.Expressions;
    using System.IO;
    using Xunit;

    public sealed class TokenizerFixture
    {
        [Fact]
        public void read_next_token_returns_null_if_stream_has_ended()
        {
            using (var stringReader = new StringReader(string.Empty))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                Assert.Null(tokenizer.ReadNextToken());
            }
        }

        [Fact]
        public void read_next_token_returns_null_if_input_contains_only_whitespace()
        {
            using (var stringReader = new StringReader("   \u0009  \u000d  "))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                Assert.Null(tokenizer.ReadNextToken());
            }
        }

        [Fact]
        public void read_next_token_returns_symbol_token_for_symbols()
        {
            using (var stringReader = new StringReader("{} () + - * / ++ (( <<"))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                var token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("{", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("}", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("(", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal(")", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("+", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("-", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("*", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("/", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("++", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("(", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("(", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("<<", token.Value);

                Assert.Null(tokenizer.ReadNextToken());
            }
        }

        [Fact]
        public void read_next_token_returns_number_token_for_numbers()
        {
            using (var stringReader = new StringReader("1 2 2398 223.1 13d .8 123abc"))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                var token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("1", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("2", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("2398", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("223.1", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("13d", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal(".8", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("123abc", token.Value);

                Assert.Null(tokenizer.ReadNextToken());
            }
        }

        [Fact]
        public void read_next_token_returns_string_token_for_string()
        {
            using (var stringReader = new StringReader(@"""one"" ""two"" ""three \"" four"""))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                var token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.String, token.Type);
                Assert.Equal("one", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.String, token.Type);
                Assert.Equal("two", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.String, token.Type);
                Assert.Equal("three \" four", token.Value);
            }
        }

        [Fact]
        public void read_next_token_returns_word_token_for_words()
        {
            using (var stringReader = new StringReader("a b abc abc123"))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                var token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Word, token.Type);
                Assert.Equal("a", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Word, token.Type);
                Assert.Equal("b", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Word, token.Type);
                Assert.Equal("abc", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Word, token.Type);
                Assert.Equal("abc123", token.Value);

                Assert.Null(tokenizer.ReadNextToken());
            }
        }

        [Fact]
        public void read_next_token_throws_if_escape_sequence_is_unrecognized()
        {
            using (var stringReader = new StringReader(@"""\h"""))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                var ex = Assert.Throws<ParseException>(() => tokenizer.ReadNextToken());
                Assert.Equal("Unrecognized escape sequence.", ex.Message);
            }
        }

        [Fact]
        public void read_next_token_handles_recognized_escape_sequences()
        {
            using (var stringReader = new StringReader(@"""\'\""\\\0\a\b\f\n\r\t\v"""))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                var token = tokenizer.ReadNextToken();

                Assert.Equal(TokenType.String, token.Type);
                Assert.Equal("\'\"\\\0\a\b\f\n\r\t\v", token.Value);
            }
        }

        [Fact]
        public void read_next_token_handles_complex_expression()
        {
            using (var stringReader = new StringReader(@"1+2 * (15/ {0}) + ((int) {1} *{2}) <<3 == ""something"" +""something else"""))
            using (var tokenizer = new Tokenizer(stringReader))
            {
                var token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("1", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("+", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("2", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("*", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("(", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("15", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("/", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("{", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("0", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("}", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal(")", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("+", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("(", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("(", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Word, token.Type);
                Assert.Equal("int", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal(")", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("{", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("1", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("}", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("*", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("{", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("2", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("}", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal(")", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("<<", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Number, token.Type);
                Assert.Equal("3", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("==", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.String, token.Type);
                Assert.Equal("something", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.Symbol, token.Type);
                Assert.Equal("+", token.Value);

                token = tokenizer.ReadNextToken();
                Assert.Equal(TokenType.String, token.Type);
                Assert.Equal("something else", token.Value);

                Assert.Null(tokenizer.ReadNextToken());
            }
        }
    }
}