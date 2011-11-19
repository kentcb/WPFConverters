using System;
using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class CastNodeTest : UnitTest
    {
        private CastNode castNode;

        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfOperandIsUnsetValue()
        {
            this.castNode = new CastNode(new ConstantNode<object>(DependencyProperty.UnsetValue), NodeValueType.String);
            Assert.Equal(DependencyProperty.UnsetValue, this.castNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldThrowIfTargetTypeIsNonNumerical()
        {
            this.castNode = new CastNode(new ConstantNode<int>(1), NodeValueType.String);
            var ex = Assert.Throws<ParseException>(() => this.castNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Cannot convert type 'Int32' to type 'String'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfValueTypeIsNonNumerical()
        {
            this.castNode = new CastNode(new ConstantNode<string>("abc"), NodeValueType.Int32);
            var ex = Assert.Throws<ParseException>(() => this.castNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Cannot convert type 'String' to type 'Int32'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldCastBooleans()
        {
            this.castNode = new CastNode(new ConstantNode<bool>(true), NodeValueType.Boolean);
            Assert.Equal(true, this.castNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldCastStrings()
        {
            this.castNode = new CastNode(new ConstantNode<string>("str"), NodeValueType.String);
            Assert.Equal("str", this.castNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldCastBytes()
        {
            this.DoCastTests<byte>();
        }

        [Fact]
        public void Evaluate_ShouldCastInt16s()
        {
            this.DoCastTests<short>();
        }

        [Fact]
        public void Evaluate_ShouldCastInt32s()
        {
            this.DoCastTests<int>();
        }

        [Fact]
        public void Evaluate_ShouldCastInt64s()
        {
            this.DoCastTests<long>();
        }

        [Fact]
        public void Evaluate_ShouldCastSingles()
        {
            this.DoCastTests<float>();
        }

        [Fact]
        public void Evaluate_ShouldCastDoubles()
        {
            this.DoCastTests<double>();
        }

        [Fact]
        public void Evaluate_ShouldCastDecimals()
        {
            this.DoCastTests<decimal>();
        }

        private void DoCastTests<T>()
        {
            T value = (T)(3 as IConvertible).ToType(typeof(T), null);

            this.castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Byte);
            Assert.Equal((byte)3, this.castNode.Evaluate(NodeEvaluationContext.Empty));

            this.castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int16);
            Assert.Equal((short)3, this.castNode.Evaluate(NodeEvaluationContext.Empty));

            this.castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int32);
            Assert.Equal(3, this.castNode.Evaluate(NodeEvaluationContext.Empty));

            this.castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int64);
            Assert.Equal(3L, this.castNode.Evaluate(NodeEvaluationContext.Empty));

            this.castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Single);
            Assert.Equal(3f, this.castNode.Evaluate(NodeEvaluationContext.Empty));

            this.castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Double);
            Assert.Equal(3d, this.castNode.Evaluate(NodeEvaluationContext.Empty));

            this.castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Decimal);
            Assert.Equal(3m, this.castNode.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
