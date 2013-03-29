using System;
using System.Windows;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class WideningBinaryNodeTest : UnitTest
    {
        private MockWideningBinaryNode wideningBinaryNode;

        [Fact]
        public void Evaluate_ShouldThrowIfUnsupportedOperands()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<int>(0), new ConstantNode<short>(0));
            this.wideningBinaryNode.IsSupportedValue = false;
            var ex = Assert.Throws<ParseException>(() => this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Int32' and 'Int16'.", ex.Message);
        }

        [Fact]
        public void Evaluate_ShouldReturnUnsetValueIfAnyOperandIsUnsetValue()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<object>(DependencyProperty.UnsetValue), new ConstantNode<string>(string.Empty));
            Assert.Equal(DependencyProperty.UnsetValue, this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty));

            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<string>(string.Empty), new ConstantNode<object>(DependencyProperty.UnsetValue));
            Assert.Equal(DependencyProperty.UnsetValue, this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty));
        }

        [Fact]
        public void Evaluate_String_ShouldCallDoString()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<string>(string.Empty), new ConstantNode<string>(string.Empty));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoStringCalled);
        }

        [Fact]
        public void Evaluate_String_ShouldNotWidenIfOneArgumentIsNull()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<string>(string.Empty), NullNode.Instance);
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoStringCalled);
        }

        [Fact]
        public void Evaluate_Boolean_ShouldCallDoBoolean()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<bool>(false), new ConstantNode<bool>(false));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoBooleanCalled);
        }

        [Fact]
        public void Evaluate_Byte_ShouldCallDoByte()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<byte>(0), new ConstantNode<byte>(0));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoByteCalled);
        }

        [Fact]
        public void Evaluate_Int16_ShouldCallDoInt16()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<short>(0), new ConstantNode<short>(0));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoInt16Called);
        }

        [Fact]
        public void Evaluate_Int32_ShouldCallDoInt32()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoInt32Called);
        }

        [Fact]
        public void Evaluate_Int64_ShouldCallDoInt64()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<long>(0), new ConstantNode<long>(0));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoInt64Called);
        }

        [Fact]
        public void Evaluate_Single_ShouldCallDoSingle()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<float>(0), new ConstantNode<float>(0));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoSingleCalled);
        }

        [Fact]
        public void Evaluate_Double_ShouldCallDoDouble()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<double>(0), new ConstantNode<double>(0));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoDoubleCalled);
        }

        [Fact]
        public void Evaluate_Decimal_ShouldCallDoDecimal()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<decimal>(0), new ConstantNode<decimal>(0));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoDecimalCalled);
        }

        [Fact]
        public void Evaluate_ReferenceType_ShouldCallDoReferenceType()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<object>(null), new ConstantNode<object>(null));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoReferenceTypeCalled);
        }

        [Fact]
        public void Evaluate_ValueType_ShouldCallDoValueType()
        {
            this.wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<DateTime>(DateTime.UtcNow), new ConstantNode<DateTime>(DateTime.UtcNow));
            this.wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(this.wideningBinaryNode.DoValueTypeCalled);
        }

        #region Supporting Types

        // cannot mock because WideningBinaryNode is internal
        private sealed class MockWideningBinaryNode : WideningBinaryNode
        {
            public bool IsSupportedValue = true;
            public bool DoBooleanCalled;
            public bool DoStringCalled;
            public bool DoByteCalled;
            public bool DoInt16Called;
            public bool DoInt32Called;
            public bool DoInt64Called;
            public bool DoSingleCalled;
            public bool DoDoubleCalled;
            public bool DoDecimalCalled;
            public bool DoReferenceTypeCalled;
            public bool DoValueTypeCalled;

            protected override string OperatorSymbols
            {
                get
                {
                    return "op";
                }
            }

            public MockWideningBinaryNode(Node leftNode, Node rightNode)
                : base(leftNode, rightNode)
            {
            }

            protected override bool DoBoolean(bool value1, bool value2, out object result)
            {
                this.DoBooleanCalled = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoString(string value1, string value2, out object result)
            {
                this.DoStringCalled = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoByte(byte value1, byte value2, out object result)
            {
                this.DoByteCalled = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoInt16(short value1, short value2, out object result)
            {
                this.DoInt16Called = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoInt32(int value1, int value2, out object result)
            {
                this.DoInt32Called = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoInt64(long value1, long value2, out object result)
            {
                this.DoInt64Called = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoSingle(float value1, float value2, out object result)
            {
                this.DoSingleCalled = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoDouble(double value1, double value2, out object result)
            {
                this.DoDoubleCalled = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoDecimal(decimal value1, decimal value2, out object result)
            {
                this.DoDecimalCalled = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoReferenceType(object value1, object value2, out object result)
            {
                this.DoReferenceTypeCalled = true;
                result = null;
                return this.IsSupportedValue;
            }

            protected override bool DoValueType(object value1, object value2, out object result)
            {
                this.DoValueTypeCalled = true;
                result = null;
                return this.IsSupportedValue;
            }
        }

        #endregion
    }
}
