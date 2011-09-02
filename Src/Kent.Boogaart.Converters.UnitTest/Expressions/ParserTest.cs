using System.IO;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Moq;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions
{
    public sealed class ParserTest : UnitTest
    {
        private Parser _parser;

        [Fact]
        public void ParseExpression_NullCoalescingSimple()
        {
            CreateParser(@"null ?? ""foo""");
            AssertEvaluation("foo");
            CreateParser(@"""bar"" ?? ""foo""");
            AssertEvaluation("bar");
        }

        [Fact]
        public void ParseExpression_NullCoalescingComplex()
        {
            CreateParser(@"null ?? null ?? null ?? null ?? null ?? ""finally""");
            AssertEvaluation("finally");
            CreateParser(@"null ?? ""foo"" ?? null");
            AssertEvaluation("foo");
        }

        [Fact]
        public void ParseExpression_TernaryConditionalSimple()
        {
            CreateParser(@"true ? ""yes"" : ""no""");
            AssertEvaluation("yes");
            CreateParser(@"false ? ""yes"" : ""no""");
            AssertEvaluation("no");
        }

        [Fact]
        public void ParseExpression_TernaryConditionalComplex()
        {
            CreateParser(@"(1 == 1 + 1) ? ""madness"" : (1 == 2 - 1) ? ""sanity"" : ""madness""");
            AssertEvaluation("sanity");
        }

        [Fact]
        public void ParseExpression_ConditionalAnd_Simple()
        {
            CreateParser("true && true");
            AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_ConditionalOr_Simple()
        {
            CreateParser("false || true");
            AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_LogicalAnd_Simple()
        {
            CreateParser("3 & 1");
            AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_LogicalOr_Simple()
        {
            CreateParser("1 | 2");
            AssertEvaluation(3);
        }

        [Fact]
        public void ParseExpression_LogicalXor_Simple()
        {
            CreateParser("3 ^ 1");
            AssertEvaluation(2);
        }

        [Fact]
        public void ParseExpression_Equality_Simple()
        {
            CreateParser("3 == 2");
            AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_Inequality_Simple()
        {
            CreateParser("3 != 2");
            AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_LessThan_Simple()
        {
            CreateParser("10 < 10");
            AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_LessThanOrEqual_Simple()
        {
            CreateParser("10 <= 10");
            AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_GreaterThan_Simple()
        {
            CreateParser("10 > 10");
            AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_GreaterThanOrEqual_Simple()
        {
            CreateParser("10 >= 10");
            AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_ShiftLeft_Simple()
        {
            CreateParser("1 << 2");
            AssertEvaluation(4);
        }

        [Fact]
        public void ParseExpression_ShiftRight_Simple()
        {
            CreateParser("4 >> 2");
            AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_Add_Simple()
        {
            CreateParser("3 + 2");
            AssertEvaluation(5);
        }

        [Fact]
        public void ParseExpression_Subtract_Simple()
        {
            CreateParser("3 - 2");
            AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_Multiply_Simple()
        {
            CreateParser("3 * 2");
            AssertEvaluation(6);
        }

        [Fact]
        public void ParseExpression_Divide_Simple()
        {
            CreateParser("4 / 2");
            AssertEvaluation(2);
        }

        [Fact]
        public void ParseExpression_Modulus_Simple()
        {
            CreateParser("5 % 2");
            AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_UnaryPlus_Simple()
        {
            CreateParser("+3");
            AssertEvaluation(3);
        }

        [Fact]
        public void ParseExpression_UnaryMinus_Simple()
        {
            CreateParser("-3");
            AssertEvaluation(-3);
        }

        [Fact]
        public void ParseExpression_Not_Simple()
        {
            CreateParser("!false");
            AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_Complement_Simple()
        {
            CreateParser("~3");
            AssertEvaluation(-4);
        }

        [Fact]
        public void ParseExpression_Cast_Simple()
        {
            CreateParser("(bool) true");
            AssertEvaluation(true);
            CreateParser("(string) \"str\"");
            AssertEvaluation("str");
            CreateParser("(byte) 3");
            AssertEvaluation((byte) 3);
            CreateParser("(short) 3");
            AssertEvaluation((short) 3);
            CreateParser("(int) 3");
            AssertEvaluation(3);
            CreateParser("(long) 3");
            AssertEvaluation(3L);
            CreateParser("(float) 3");
            AssertEvaluation(3f);
            CreateParser("(double) 3");
            AssertEvaluation(3d);
            CreateParser("(decimal) 3");
            AssertEvaluation(3m);
        }

        [Fact]
        public void ParseExpression_Boolean_Simple()
        {
            CreateParser("true");
            AssertEvaluation(true);
            CreateParser("false");
            AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_IntegralNumbers_Simple()
        {
            CreateParser("(byte) 43");
            AssertEvaluation((byte) 43);
            CreateParser("(short) 43");
            AssertEvaluation((short) 43);
            CreateParser("43");
            AssertEvaluation(43);
            CreateParser("43L");
            AssertEvaluation(43L);
            CreateParser("43l");
            AssertEvaluation(43L);
            CreateParser("0x2b");
            AssertEvaluation(43);
            CreateParser("0x2B");
            AssertEvaluation(43);
            CreateParser("0x2bL");
            AssertEvaluation(43L);
        }

        [Fact]
        public void ParseExpression_RealNumbers_Simple()
        {
            CreateParser("4.34f");
            AssertEvaluation(4.34f);
            CreateParser("4.34");
            AssertEvaluation(4.34d);
            CreateParser("4.34m");
            AssertEvaluation(4.34m);
            CreateParser("4d");
            AssertEvaluation(4d);
            CreateParser("4.34e3f");
            AssertEvaluation(4.34e3f);
            CreateParser("4.34e3");
            AssertEvaluation(4.34e3);
        }

        [Fact]
        public void ParseExpression_String_Simple()
        {
            CreateParser("\"a string\"");
            AssertEvaluation("a string");
        }

        [Fact]
        public void ParseExpression_Variable_Simple()
        {
            CreateParser("{0}");
            AssertEvaluation(3, new NodeEvaluationContext(new object[] { 3 }));
        }

        [Fact]
        public void ParseExpression_Null_Simple()
        {
            CreateParser("null");
            AssertEvaluation(null);
        }

        [Fact]
        public void ParseExpression_Grouping_Simple()
        {
            CreateParser("(3)");
            AssertEvaluation(3);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfIncorrectGroupingSymbol()
        {
            var ex = Assert.Throws<ParseException>(() => CreateParser("3 + [5*2]"));
            Assert.Equal("Expected an expression.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfCastingToUnknownType()
        {
            var ex = Assert.Throws<ParseException>(() => CreateParser("(fubar) 3"));
            Assert.Equal("Cannot cast to type 'fubar'.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfUnknownKeyword()
        {
            var ex = Assert.Throws<ParseException>(() => CreateParser("{0} == nulll"));
            Assert.Equal("Unexpected input 'nulll'.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfUnrecognizedSymbol()
        {
            var ex = Assert.Throws<ParseException>(() => CreateParser("1 <<> 3"));
            Assert.Equal("Unexpected input '<<>'.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ComplexTests()
        {
            CreateParser("1 + (8/3d) * (50 >> (3 - 1)) * 1e4");
            AssertEvaluation(320001d);

            CreateParser("\"a\"+\"b\" + \"c\" +\"def\" + \"\\nghi\" == {0}");
            AssertEvaluation(true, new NodeEvaluationContext(new object[] { "abcdef\nghi" }));
            AssertEvaluation(false, new NodeEvaluationContext(new object[] { "abcdefghi" }));

            CreateParser("23543L & 3448 | (1024 * 56) ^ 8948 ^ (548395 % 34853)");
            AssertEvaluation(45044L);

            CreateParser("~{0} * -{1} / {2}");
            AssertEvaluation(177m, new NodeEvaluationContext(new object[] { 235, 3m, 4 }));
            AssertEvaluation(-279, new NodeEvaluationContext(new object[] { -32, 18, 2 }));

            CreateParser(@"{0} > 100 ? ({1} ?? {2} ?? ""default"") : ""small number""");
            AssertEvaluation("small number", new NodeEvaluationContext(new object[] { 99, "first", "second" }));
            AssertEvaluation("first", new NodeEvaluationContext(new object[] { 101, "first", "second" }));
            AssertEvaluation("second", new NodeEvaluationContext(new object[] { 101, null, "second" }));
            AssertEvaluation("default", new NodeEvaluationContext(new object[] { 101, null, null }));
        }

        #region Supporting Methods

        Node _expression;

        private void CreateParser(string input)
        {
            _parser = new Parser(new Tokenizer(new StringReader(input)));
            _expression = _parser.ParseExpression();
        }

        private void AssertEvaluation(object expectedResult)
        {
            AssertEvaluation(expectedResult, NodeEvaluationContext.Empty);
        }

        private void AssertEvaluation(object expectedResult, NodeEvaluationContext nodeEvaluationContext)
        {
            Assert.Equal(expectedResult, _expression.Evaluate(nodeEvaluationContext));
        }

        #endregion
    }
}
