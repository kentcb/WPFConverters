using System;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class WideningBinaryNodeTest : UnitTest
    {
        private MockWideningBinaryNode _wideningBinaryNode;

        [Fact]
        public void Evaluate_ShouldThrowIfUnsupportedOperands()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<int>(0), new ConstantNode<short>(0));
            _wideningBinaryNode.IsSupportedValue = false;
            var ex = Assert.Throws<ParseException>(() => _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty));
            Assert.Equal("Operator 'op' cannot be applied to operands of type 'Int32' and 'Int16'.", ex.Message);
        }

        [Fact]
        public void Evaluate_String_ShouldCallDoString()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<string>(""), new ConstantNode<string>(""));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoStringCalled);
        }

        [Fact]
        public void Evaluate_String_ShouldNotWidenIfOneArgumentIsNull()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<string>(""), NullNode.Instance);
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoStringCalled);
        }

        [Fact]
        public void Evaluate_Boolean_ShouldCallDoBoolean()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<bool>(false), new ConstantNode<bool>(false));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoBooleanCalled);
        }

        [Fact]
        public void Evaluate_Byte_ShouldCallDoByte()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<byte>(0), new ConstantNode<byte>(0));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoByteCalled);
        }

        [Fact]
        public void Evaluate_Int16_ShouldCallDoInt16()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<short>(0), new ConstantNode<short>(0));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoInt16Called);
        }

        [Fact]
        public void Evaluate_Int32_ShouldCallDoInt32()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoInt32Called);
        }

        [Fact]
        public void Evaluate_Int64_ShouldCallDoInt64()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<long>(0), new ConstantNode<long>(0));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoInt64Called);
        }

        [Fact]
        public void Evaluate_Single_ShouldCallDoSingle()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<float>(0), new ConstantNode<float>(0));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoSingleCalled);
        }

        [Fact]
        public void Evaluate_Double_ShouldCallDoDouble()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<double>(0), new ConstantNode<double>(0));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoDoubleCalled);
        }

        [Fact]
        public void Evaluate_Decimal_ShouldCallDoDecimal()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<decimal>(0), new ConstantNode<decimal>(0));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoDecimalCalled);
        }

        [Fact]
        public void Evaluate_ReferenceType_ShouldCallDoReferenceType()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<object>(null), new ConstantNode<object>(null));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoReferenceTypeCalled);
        }

        [Fact]
        public void Evaluate_ValueType_ShouldCallDoValueType()
        {
            _wideningBinaryNode = new MockWideningBinaryNode(new ConstantNode<DateTime>(DateTime.UtcNow), new ConstantNode<DateTime>(DateTime.UtcNow));
            _wideningBinaryNode.Evaluate(NodeEvaluationContext.Empty);
            Assert.True(_wideningBinaryNode.DoValueTypeCalled);
        }

        #region Supporting Types

        //cannot mock because WideningBinaryNode is internal
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
                DoBooleanCalled = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoString(string value1, string value2, out object result)
            {
                DoStringCalled = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoByte(byte value1, byte value2, out object result)
            {
                DoByteCalled = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoInt16(short value1, short value2, out object result)
            {
                DoInt16Called = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoInt32(int value1, int value2, out object result)
            {
                DoInt32Called = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoInt64(long value1, long value2, out object result)
            {
                DoInt64Called = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoSingle(float value1, float value2, out object result)
            {
                DoSingleCalled = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoDouble(double value1, double value2, out object result)
            {
                DoDoubleCalled = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoDecimal(decimal value1, decimal value2, out object result)
            {
                DoDecimalCalled = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoReferenceType(object value1, object value2, out object result)
            {
                DoReferenceTypeCalled = true;
                result = null;
                return IsSupportedValue;
            }

            protected override bool DoValueType(object value1, object value2, out object result)
            {
                DoValueTypeCalled = true;
                result = null;
                return IsSupportedValue;
            }
        }

        #endregion
    }
}
