using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class ShiftNodeTest : UnitTest
    {
        private MockShiftNode shiftNode;

        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfAnyOperandIsUnsetValue()
        {
            this.shiftNode = new MockShiftNode(new ConstantNode<object>(DependencyProperty.UnsetValue), new ConstantNode<int>(1));
            Assert.Equal(DependencyProperty.UnsetValue, this.shiftNode.Evaluate(NodeEvaluationContext.Empty));

            this.shiftNode = new MockShiftNode(new ConstantNode<int>(1), new ConstantNode<object>(DependencyProperty.UnsetValue));
            Assert.Equal(DependencyProperty.UnsetValue, this.shiftNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_ShouldThrowIfRightNodeIsntInt32()
        {
            this.shiftNode = new MockShiftNode(new ConstantNode<int>(1), new ConstantNode<short>(1));
            var ex = Assert.Throws<ParseException>(() => this.shiftNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Int32' and 'Int16' because the left node is non-numerical or because the right node isn't an Int32.", ex.Message);
        }

        [Fact]
        public void Evaluate_Byte_ShouldCallDoByte()
        {
            this.shiftNode = new MockShiftNode(new ConstantNode<byte>(0), new ConstantNode<int>(1));
            this.shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.shiftNode.DoByteCalled);
        }

        [Fact]
        public void Evaluate_Int16_ShouldCallDoInt16()
        {
            this.shiftNode = new MockShiftNode(new ConstantNode<short>(0), new ConstantNode<int>(1));
            this.shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.shiftNode.DoInt16Called);
        }

        [Fact]
        public void Evaluate_Int32_ShouldCallDoInt32()
        {
            this.shiftNode = new MockShiftNode(new ConstantNode<int>(0), new ConstantNode<int>(1));
            this.shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.shiftNode.DoInt32Called);
        }

        [Fact]
        public void Evaluate_Int64_ShouldCallDoInt64()
        {
            this.shiftNode = new MockShiftNode(new ConstantNode<long>(0), new ConstantNode<int>(1));
            this.shiftNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.shiftNode.DoInt64Called);
        }

        #region Supporting Types

        // cannot mock because ShiftNode is internal
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
                this.DoByteCalled = true;
                return 0;
            }

            protected override int DoInt16(short value1, int value2)
            {
                this.DoInt16Called = true;
                return 0;
            }

            protected override int DoInt32(int value1, int value2)
            {
                this.DoInt32Called = true;
                return 0;
            }

            protected override long DoInt64(long value1, int value2)
            {
                this.DoInt64Called = true;
                return 0;
            }
        }

        #endregion
    }
}
