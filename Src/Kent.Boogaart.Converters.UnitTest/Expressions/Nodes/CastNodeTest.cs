using System;
using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class CastNodeTest : UnitTest
    {
        private CastNode _castNode;

        [Fact]
        public void Evaluate_ShouldThrowIfTargetTypeIsNonNumerical()
        {
            _castNode = new CastNode(new ConstantNode<int>(1), NodeValueType.String);
            var ex = Assert.Throws<ParseException>(() => _castNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Cannot convert type 'Int32' to type 'String'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldThrowIfValueTypeIsNonNumerical()
        {
            _castNode = new CastNode(new ConstantNode<string>("abc"), NodeValueType.Int32);
            var ex = Assert.Throws<ParseException>(() => _castNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Cannot convert type 'String' to type 'Int32'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldCastBooleans()
        {
            _castNode = new CastNode(new ConstantNode<bool>(true), NodeValueType.Boolean);
            Assert.Equal(true, _castNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldCastStrings()
        {
            _castNode = new CastNode(new ConstantNode<string>("str"), NodeValueType.String);
            Assert.Equal("str", _castNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldCastBytes()
        {
            DoCastTests<byte>();
        }

        [Fact]
        public void Evaluate_ShouldCastInt16s()
        {
            DoCastTests<short>();
        }

        [Fact]
        public void Evaluate_ShouldCastInt32s()
        {
            DoCastTests<int>();
        }

        [Fact]
        public void Evaluate_ShouldCastInt64s()
        {
            DoCastTests<long>();
        }

        [Fact]
        public void Evaluate_ShouldCastSingles()
        {
            DoCastTests<float>();
        }

        [Fact]
        public void Evaluate_ShouldCastDoubles()
        {
            DoCastTests<double>();
        }

        [Fact]
        public void Evaluate_ShouldCastDecimals()
        {
            DoCastTests<decimal>();
        }

        private void DoCastTests<T>()
        {
            T value = (T) (3 as IConvertible).ToType(typeof(T), null);

            _castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Byte);
            Assert.Equal((byte) 3, _castNode.Evaluate(NodeEvaluationContext.Empty));

            _castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int16);
            Assert.Equal((short) 3, _castNode.Evaluate(NodeEvaluationContext.Empty));

            _castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int32);
            Assert.Equal(3, _castNode.Evaluate(NodeEvaluationContext.Empty));

            _castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Int64);
            Assert.Equal(3L, _castNode.Evaluate(NodeEvaluationContext.Empty));

            _castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Single);
            Assert.Equal(3f, _castNode.Evaluate(NodeEvaluationContext.Empty));

            _castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Double);
            Assert.Equal(3d, _castNode.Evaluate(NodeEvaluationContext.Empty));

            _castNode = new CastNode(new ConstantNode<T>(value), NodeValueType.Decimal);
            Assert.Equal(3m, _castNode.Evaluate(NodeEvaluationContext.Empty));
        }
    }
}
