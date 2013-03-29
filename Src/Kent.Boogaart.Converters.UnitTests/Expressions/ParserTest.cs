using System;
using System.IO;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions
{
    public sealed class ParserTest : UnitTest
    {
        private Parser parser;
        private Node expression;

        [Fact]
        public void ParseExpression_NullCoalescingSimple()
        {
            this.CreateParser(@"null ?? ""foo""");
            this.AssertEvaluation("foo");
            this.CreateParser(@"""bar"" ?? ""foo""");
            this.AssertEvaluation("bar");
        }

        [Fact]
        public void ParseExpression_NullCoalescingComplex()
        {
            this.CreateParser(@"null ?? null ?? null ?? null ?? null ?? ""finally""");
            this.AssertEvaluation("finally");
            this.CreateParser(@"null ?? ""foo"" ?? null");
            this.AssertEvaluation("foo");
        }

        [Fact]
        public void ParseExpression_TernaryConditionalSimple()
        {
            this.CreateParser(@"true ? ""yes"" : ""no""");
            this.AssertEvaluation("yes");
            this.CreateParser(@"false ? ""yes"" : ""no""");
            this.AssertEvaluation("no");
        }

        [Fact]
        public void ParseExpression_TernaryConditionalComplex()
        {
            this.CreateParser(@"(1 == 1 + 1) ? ""madness"" : (1 == 2 - 1) ? ""sanity"" : ""madness""");
            this.AssertEvaluation("sanity");
        }

        [Fact]
        public void ParseExpression_ConditionalAnd_Simple()
        {
            this.CreateParser("true && true");
            this.AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_ConditionalOr_Simple()
        {
            this.CreateParser("false || true");
            this.AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_LogicalAnd_Simple()
        {
            this.CreateParser("3 & 1");
            this.AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_LogicalOr_Simple()
        {
            this.CreateParser("1 | 2");
            this.AssertEvaluation(3);
        }

        [Fact]
        public void ParseExpression_LogicalXor_Simple()
        {
            this.CreateParser("3 ^ 1");
            this.AssertEvaluation(2);
        }

        [Fact]
        public void ParseExpression_Equality_Simple()
        {
            this.CreateParser("3 == 2");
            this.AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_Inequality_Simple()
        {
            this.CreateParser("3 != 2");
            this.AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_LessThan_Simple()
        {
            this.CreateParser("10 < 10");
            this.AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_LessThanOrEqual_Simple()
        {
            this.CreateParser("10 <= 10");
            this.AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_GreaterThan_Simple()
        {
            this.CreateParser("10 > 10");
            this.AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_GreaterThanOrEqual_Simple()
        {
            this.CreateParser("10 >= 10");
            this.AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_ShiftLeft_Simple()
        {
            this.CreateParser("1 << 2");
            this.AssertEvaluation(4);
        }

        [Fact]
        public void ParseExpression_ShiftRight_Simple()
        {
            this.CreateParser("4 >> 2");
            this.AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_Add_Simple()
        {
            this.CreateParser("3 + 2");
            this.AssertEvaluation(5);
        }

        [Fact]
        public void ParseExpression_Subtract_Simple()
        {
            this.CreateParser("3 - 2");
            this.AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_Multiply_Simple()
        {
            this.CreateParser("3 * 2");
            this.AssertEvaluation(6);
        }

        [Fact]
        public void ParseExpression_Divide_Simple()
        {
            this.CreateParser("4 / 2");
            this.AssertEvaluation(2);
        }

        [Fact]
        public void ParseExpression_Modulus_Simple()
        {
            this.CreateParser("5 % 2");
            this.AssertEvaluation(1);
        }

        [Fact]
        public void ParseExpression_UnaryPlus_Simple()
        {
            this.CreateParser("+3");
            this.AssertEvaluation(3);
        }

        [Fact]
        public void ParseExpression_UnaryMinus_Simple()
        {
            this.CreateParser("-3");
            this.AssertEvaluation(-3);
        }

        [Fact]
        public void ParseExpression_Not_Simple()
        {
            this.CreateParser("!false");
            this.AssertEvaluation(true);
        }

        [Fact]
        public void ParseExpression_Complement_Simple()
        {
            this.CreateParser("~3");
            this.AssertEvaluation(-4);
        }

        [Fact]
        public void ParseExpression_Cast_Simple()
        {
            this.CreateParser("(bool) true");
            this.AssertEvaluation(true);
            this.CreateParser("(string) \"str\"");
            this.AssertEvaluation("str");
            this.CreateParser("(byte) 3");
            this.AssertEvaluation((byte)3);
            this.CreateParser("(short) 3");
            this.AssertEvaluation((short)3);
            this.CreateParser("(int) 3");
            this.AssertEvaluation(3);
            this.CreateParser("(long) 3");
            this.AssertEvaluation(3L);
            this.CreateParser("(float) 3");
            this.AssertEvaluation(3f);
            this.CreateParser("(double) 3");
            this.AssertEvaluation(3d);
            this.CreateParser("(decimal) 3");
            this.AssertEvaluation(3m);
        }

        [Fact]
        public void ParseExpression_Boolean_Simple()
        {
            this.CreateParser("true");
            this.AssertEvaluation(true);
            this.CreateParser("false");
            this.AssertEvaluation(false);
        }

        [Fact]
        public void ParseExpression_IntegralNumbers_Simple()
        {
            this.CreateParser("(byte) 43");
            this.AssertEvaluation((byte)43);
            this.CreateParser("(short) 43");
            this.AssertEvaluation((short)43);
            this.CreateParser("43");
            this.AssertEvaluation(43);
            this.CreateParser("43L");
            this.AssertEvaluation(43L);
            this.CreateParser("43l");
            this.AssertEvaluation(43L);
            this.CreateParser("0x2b");
            this.AssertEvaluation(43);
            this.CreateParser("0x2B");
            this.AssertEvaluation(43);
            this.CreateParser("0x2bL");
            this.AssertEvaluation(43L);
        }

        [Fact]
        public void ParseExpression_RealNumbers_Simple()
        {
            this.CreateParser("4.34f");
            this.AssertEvaluation(4.34f);
            this.CreateParser("4.34");
            this.AssertEvaluation(4.34d);
            this.CreateParser("4.34m");
            this.AssertEvaluation(4.34m);
            this.CreateParser("4d");
            this.AssertEvaluation(4d);
            this.CreateParser("4.34e3f");
            this.AssertEvaluation(4.34e3f);
            this.CreateParser("4.34e3");
            this.AssertEvaluation(4.34e3);
        }

        [Fact]
        public void ParseExpression_String_Simple()
        {
            this.CreateParser("\"a string\"");
            this.AssertEvaluation("a string");
        }

        [Fact]
        public void ParseExpression_Variable_Simple()
        {
            this.CreateParser("{0}");
            this.AssertEvaluation(3, new NodeEvaluationContext(new object[] { 3 }));
        }

        [Fact]
        public void ParseExpression_Null_Simple()
        {
            this.CreateParser("null");
            this.AssertEvaluation(null);
        }

        [Fact]
        public void ParseExpression_Grouping_Simple()
        {
            this.CreateParser("(3)");
            this.AssertEvaluation(3);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfIncorrectGroupingSymbol()
        {
            var ex = Assert.Throws<ParseException>(() => this.CreateParser("3 + [5*2]"));
            Assert.Equal("Expected an expression.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfCastingToUnknownType()
        {
            var ex = Assert.Throws<ParseException>(() => this.CreateParser("(fubar) 3"));
            Assert.Equal("Cannot cast to type 'fubar'.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfUnknownKeyword()
        {
            var ex = Assert.Throws<ParseException>(() => this.CreateParser("{0} == nulll"));
            Assert.Equal("Unexpected input 'nulll'.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ShouldThrowIfUnrecognizedSymbol()
        {
            var ex = Assert.Throws<ParseException>(() => this.CreateParser("1 <<> 3"));
            Assert.Equal("Unexpected input '<<>'.", ex.Message);
        }

        [Fact]
        public void ParseExpression_ComplexTests()
        {
            this.CreateParser("1 + (8/3d) * (50 >> (3 - 1)) * 1e4");
            this.AssertEvaluation(320001d);

            this.CreateParser("\"a\"+\"b\" + \"c\" +\"def\" + \"\\nghi\" == {0}");
            this.AssertEvaluation(true, new NodeEvaluationContext(new object[] { "abcdef\nghi" }));
            this.AssertEvaluation(false, new NodeEvaluationContext(new object[] { "abcdefghi" }));

            this.CreateParser("23543L & 3448 | (1024 * 56) ^ 8948 ^ (548395 % 34853)");
            this.AssertEvaluation(45044L);

            this.CreateParser("~{0} * -{1} / {2}");
            this.AssertEvaluation(177m, new NodeEvaluationContext(new object[] { 235, 3m, 4 }));
            this.AssertEvaluation(-279, new NodeEvaluationContext(new object[] { -32, 18, 2 }));

            this.CreateParser(@"{0} > 100 ? ({1} ?? {2} ?? ""default"") : ""small number""");
            this.AssertEvaluation("small number", new NodeEvaluationContext(new object[] { 99, "first", "second" }));
            this.AssertEvaluation("first", new NodeEvaluationContext(new object[] { 101, "first", "second" }));
            this.AssertEvaluation("second", new NodeEvaluationContext(new object[] { 101, null, "second" }));
            this.AssertEvaluation("default", new NodeEvaluationContext(new object[] { 101, null, null }));

            this.CreateParser(@"{0} == {1}");
            this.AssertEvaluation(true, new NodeEvaluationContext(new object[] { ConsoleKey.A, ConsoleKey.A }));
            this.AssertEvaluation(true, new NodeEvaluationContext(new object[] { ConsoleKey.A, (int)ConsoleKey.A }));
            this.AssertEvaluation(false, new NodeEvaluationContext(new object[] { ConsoleKey.A, ConsoleKey.B }));
            this.AssertEvaluation(true, new NodeEvaluationContext(new object[] { this, this }));
            this.AssertEvaluation(false, new NodeEvaluationContext(new object[] { this, this.expression }));

            this.CreateParser(@"{0} != null && {1} == {2}");
            this.AssertEvaluation(true, new NodeEvaluationContext(new object[] { this, ConsoleKey.A, ConsoleKey.A }));
            this.AssertEvaluation(false, new NodeEvaluationContext(new object[] { null, ConsoleKey.A, ConsoleKey.A }));
            this.AssertEvaluation(false, new NodeEvaluationContext(new object[] { null, ConsoleKey.A, ConsoleKey.B }));
        }

        #region Supporting Methods

        private void CreateParser(string input)
        {
            this.parser = new Parser(new Tokenizer(new StringReader(input)));
            this.expression = this.parser.ParseExpression();
        }

        private void AssertEvaluation(object expectedResult)
        {
            this.AssertEvaluation(expectedResult, NodeEvaluationContext.Empty);
        }

        private void AssertEvaluation(object expectedResult, NodeEvaluationContext nodeEvaluationContext)
        {
            Assert.Equal(expectedResult, this.expression.Evaluate(nodeEvaluationContext));
        }

        #endregion
    }
}
