namespace Kent.Boogaart.Converters.UnitTests.Expressions
{
    using Kent.Boogaart.Converters.Expressions;
    using Kent.Boogaart.Converters.Expressions.Nodes;
    using System;
    using System.IO;
    using Xunit;
    using Xunit.Extensions;

    [CLSCompliant(false)]
    public sealed class ParserFixture
    {
        [Fact]
        public void parse_expression_parses_simple_null_coalescing_expression()
        {
            using (var stringReader = new StringReader(@"null ?? ""foo"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("foo", expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader(@"""bar"" ?? ""foo"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("bar", expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_complex_null_coalescing_expression()
        {
            using (var stringReader = new StringReader(@"null ?? null ?? null ?? null ?? null ?? ""finally"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("finally", expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader(@"null ?? ""foo"" ?? null"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("foo", expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_simple_ternary_conditional_expression()
        {
            using (var stringReader = new StringReader(@"true ? ""yes"" : ""no"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("yes", expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader(@"false ? ""yes"" : ""no"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("no", expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_complex_ternary_conditional_expression()
        {
            using (var stringReader = new StringReader(@"(1 == 1 + 1) ? ""madness"" : (1 == 2 - 1) ? ""sanity"" : ""madness"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("sanity", expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_conditional_and_expression()
        {
            using (var stringReader = new StringReader("true && true"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_conditional_or_expression()
        {
            using (var stringReader = new StringReader("false || true"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_logical_and_expression()
        {
            using (var stringReader = new StringReader("3 & 1"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(1, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_logical_or_expression()
        {
            using (var stringReader = new StringReader("1 | 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_logical_xor_expression()
        {
            using (var stringReader = new StringReader("3 ^ 1"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(2, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_equality_expression()
        {
            using (var stringReader = new StringReader("3 == 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_inequality_expression()
        {
            using (var stringReader = new StringReader("3 != 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_less_than_expression()
        {
            using (var stringReader = new StringReader("10 < 10"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_less_than_or_equal_expression()
        {
            using (var stringReader = new StringReader("10 <= 10"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_greater_than_expression()
        {
            using (var stringReader = new StringReader("10 > 10"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_greater_than_or_equal_expression()
        {
            using (var stringReader = new StringReader("10 >= 10"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_shift_left_expression()
        {
            using (var stringReader = new StringReader("1 << 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(4, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_shift_right_expression()
        {
            using (var stringReader = new StringReader("4 >> 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(1, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_add_expression()
        {
            using (var stringReader = new StringReader("3 + 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(5, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_subtract_expression()
        {
            using (var stringReader = new StringReader("3 - 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(1, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_multiply_expression()
        {
            using (var stringReader = new StringReader("3 * 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(6, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_divide_expression()
        {
            using (var stringReader = new StringReader("4 / 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(2, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_modulus_expression()
        {
            using (var stringReader = new StringReader("5 % 2"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(1, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_unary_plus_expression()
        {
            using (var stringReader = new StringReader("+3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_unary_minus_expression()
        {
            using (var stringReader = new StringReader("-3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(-3, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_logical_not_expression()
        {
            using (var stringReader = new StringReader("!false"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_complement_expression()
        {
            using (var stringReader = new StringReader("~3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(-4, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_cast_expression()
        {
            using (var stringReader = new StringReader("(bool) true"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(string) \"str\""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("str", expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(byte) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal((byte)3, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(short) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal((short)3, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(int) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(long) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3L, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(float) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3f, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(double) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3d, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("(decimal) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3m, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_boolean_expression()
        {
            using (var stringReader = new StringReader("true"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("false"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Theory]
        [InlineData("(byte)43", (byte)43)]
        [InlineData("(short) 43", (short)43)]
        [InlineData("43", 43)]
        [InlineData("43L", 43L)]
        [InlineData("43l", 43L)]
        [InlineData("0x2b", 43)]
        [InlineData("0x2B", 43)]
        [InlineData("0x2bL", 43L)]
        public void parse_expression_parses_integral_number_expression(string input, object expectedEvaluationValue)
        {
            using (var stringReader = new StringReader(input))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(expectedEvaluationValue, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Theory]
        [InlineData("4.34f", 4.34f)]
        [InlineData("4.34", 4.34d)]
        [InlineData("4d", 4d)]
        [InlineData("4.34e3f", 4.34e3f)]
        [InlineData("4.34e3", 4.34e3)]
        public void parse_expression_parses_real_number_expression(string input, object expectedEvaluationValue)
        {
            using (var stringReader = new StringReader(input))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(expectedEvaluationValue, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_string_expression()
        {
            using (var stringReader = new StringReader(@"""a string"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("a string", expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_variable_expression()
        {
            using (var stringReader = new StringReader("{0}"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3, expression.Evaluate(NodeEvaluationContext.Create(3)));
            }
        }

        [Fact]
        public void parse_expression_parses_null_expression()
        {
            using (var stringReader = new StringReader("null"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(null, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_parses_grouping_expression()
        {
            using (var stringReader = new StringReader("(3)"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(3, expression.Evaluate(NodeEvaluationContext.Empty));
            }
        }

        [Fact]
        public void parse_expression_throws_if_incorrect_grouping_symbol_is_used()
        {
            using (var stringReader = new StringReader("3 + [5*2]"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var ex = Assert.Throws<ParseException>(() => parser.ParseExpression());
                Assert.Equal("Expected an expression.", ex.Message);
            }
        }

        [Fact]
        public void parse_expression_throws_if_cast_to_unknown_type_is_specified()
        {
            using (var stringReader = new StringReader("(fubar) 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var ex = Assert.Throws<ParseException>(() => parser.ParseExpression());
                Assert.Equal("Cannot cast to type 'fubar'.", ex.Message);
            }
        }

        [Fact]
        public void parse_expression_throws_if_unknown_keyword_is_specified()
        {
            using (var stringReader = new StringReader("{0} == nulll"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var ex = Assert.Throws<ParseException>(() => parser.ParseExpression());
                Assert.Equal("Unexpected input 'nulll'.", ex.Message);
            }
        }

        [Fact]
        public void parse_expression_throws_if_unknown_symbol_is_specified()
        {
            using (var stringReader = new StringReader("1 <<> 3"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var ex = Assert.Throws<ParseException>(() => parser.ParseExpression());
                Assert.Equal("Unexpected input '<<>'.", ex.Message);
            }
        }

        [Fact]
        public void parse_expression_parses_complex_expressions()
        {
            using (var stringReader = new StringReader("1 + (8/3d) * (50 >> (3 - 1)) * 1e4"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(320001d, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("\"a\"+\"b\" + \"c\" +\"def\" + \"\\nghi\" == {0}"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Create("abcdef\nghi")));
                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Create("abcdefghi")));
            }

            using (var stringReader = new StringReader("23543L & 3448 | (1024 * 56) ^ 8948 ^ (548395 % 34853)"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(45044L, expression.Evaluate(NodeEvaluationContext.Empty));
            }

            using (var stringReader = new StringReader("~{0} * -{1} / {2}"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(177m, expression.Evaluate(NodeEvaluationContext.Create(235, 3m, 4)));
                Assert.Equal(-279, expression.Evaluate(NodeEvaluationContext.Create(-32, 18, 2)));
            }

            using (var stringReader = new StringReader(@"{0} > 100 ? ({1} ?? {2} ?? ""default"") : ""small number"""))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal("small number", expression.Evaluate(NodeEvaluationContext.Create(99, "first", "second")));
                Assert.Equal("first", expression.Evaluate(NodeEvaluationContext.Create(101, "first", "second")));
                Assert.Equal("second", expression.Evaluate(NodeEvaluationContext.Create(101, null, "second")));
                Assert.Equal("default", expression.Evaluate(NodeEvaluationContext.Create(101, null, null)));
            }

            using (var stringReader = new StringReader(@"{0} == {1}"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Create(ConsoleKey.A, ConsoleKey.A)));
                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Create(ConsoleKey.A, (int)ConsoleKey.A)));
                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Create(ConsoleKey.A, ConsoleKey.B)));
                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Create(this, this)));
                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Create(this, expression)));
            }

            using (var stringReader = new StringReader(@"{0} != null && {1} == {2}"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Create(this, ConsoleKey.A, ConsoleKey.A)));
                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Create(null, ConsoleKey.A, ConsoleKey.A)));
                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Create(this, ConsoleKey.A, ConsoleKey.B)));
            }

            using (var stringReader = new StringReader(@"{0} != null && {1} == {2}"))
            using (var tokenizer = new Tokenizer(stringReader))
            using (var parser = new Parser(tokenizer))
            {
                var expression = parser.ParseExpression();

                Assert.Equal(true, expression.Evaluate(NodeEvaluationContext.Create(this, ConsoleKey.A, ConsoleKey.A)));
                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Create(null, ConsoleKey.A, ConsoleKey.A)));
                Assert.Equal(false, expression.Evaluate(NodeEvaluationContext.Create(this, ConsoleKey.A, ConsoleKey.B)));
            }
        }
    }
}