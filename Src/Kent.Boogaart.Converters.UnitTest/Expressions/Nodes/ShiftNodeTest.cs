using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ShiftNodeTest : UnitTest
    {
        private MockShiftNode _shiftNode;

        [Fact]
        public void Evaluate_ShouldThrowIfRightNodeIsntInt32()
        {
            _shiftNode = new MockShiftNode(new ConstantNode<int>(1), new ConstantNode<short>(1));
            var ex = Assert.Throws<ParseException>(() => _shiftNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Int32' and 'Int16' because the left node is non-numerical or because the right node isn't an Int32.", ex.Message);
        }

        [Fact]
        public void Evaluate_Byte_ShouldCallDoByte()
        {
            _shiftNode = new MockShiftNode(new ConstantNode<byte>(0), new ConstantNode<int>(1));
            _shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_shiftNode.DoByteCalled);
        }

        [Fact]
        public void Evaluate_Int16_ShouldCallDoInt16()
        {
            _shiftNode = new MockShiftNode(new ConstantNode<short>(0), new ConstantNode<int>(1));
            _shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_shiftNode.DoInt16Called);
        }

        [Fact]
        public void Evaluate_Int32_ShouldCallDoInt32()
        {
            _shiftNode = new MockShiftNode(new ConstantNode<int>(0), new ConstantNode<int>(1));
            _shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_shiftNode.DoInt32Called);
        }

        [Fact]
        public void Evaluate_Int64_ShouldCallDoInt64()
        {
            _shiftNode = new MockShiftNode(new ConstantNode<long>(0), new ConstantNode<int>(1));
            _shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_shiftNode.DoInt64Called);
        }

        #region Supporting Types

        //cannot mock because ShiftNode is internal
        private sealed class MockShiftNode : ShiftNode
        {
            public bool DoByteCalled;
            public bool DoInt16Called;
            public bool DoInt32Called;
            public bool DoInt64Called;

            protected override string OperatorSymbols
            {
                get
                {
                    return "op";
                }
            }

            public MockShiftNode(Node leftNode, Node rightNode)
                : base(leftNode, rightNode)
            {
            }

            protected override int DoByte(byte value1, int value2)
            {
                DoByteCalled = true;
                return 0;
            }

            protected override int DoInt16(short value1, int value2)
            {
                DoInt16Called = true;
                return 0;
            }

            protected override int DoInt32(int value1, int value2)
            {
                DoInt32Called = true;
                return 0;
            }

            protected override long DoInt64(long value1, int value2)
            {
                DoInt64Called = true;
                return 0;
            }
        }

        #endregion
    }
}
