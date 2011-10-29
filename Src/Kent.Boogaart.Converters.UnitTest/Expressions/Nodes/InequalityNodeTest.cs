using System;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class InequalityNodeTest : UnitTest
    {
        private InequalityNode _inequalityNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _inequalityNode = new InequalityNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("!=", GetPrivateMemberValue<string>(_inequalityNode, "OperatorSymbols"));
        }

        [Fact]
        public void IsSupported_ShouldYieldTrue()
        {
            foreach (NodeValueType type1 in Enum.GetValues(typeof(NodeValueType)))
            {
                foreach (NodeValueType type2 in Enum.GetValues(typeof(NodeValueType)))
                {
                    Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "IsSupported", type1, type2));
                }
            }
        }

        [Fact]
        public void DoString_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", "abc"));
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoString", null, null));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", null));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoString", "abc", "abcd"));
        }

        [Fact]
        public void DoBoolean_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoBoolean", true, true));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoBoolean", true, false));
        }

        [Fact]
        public void DoByte_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoByte", (byte) 1, (byte) 1));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoByte", (byte) 1, (byte) 2));
        }

        [Fact]
        public void DoInt16_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoInt16", (short) 1, (short) 1));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoInt16", (short) 1, (short) 2));
        }

        [Fact]
        public void DoInt32_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoInt32", 1, 1));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoInt32", 1, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoInt64", 1L, 1L));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoInt64", 1L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoSingle", 1f, 1f));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoSingle", 1f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoDouble", 1d, 1d));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoDouble", 1d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoDecimal", 1m, 1m));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoDecimal", 1m, 2m));
        }

        [Fact]
        public void DoValueType_ShouldDoComparison()
        {
            var value1 = DateTime.UtcNow;
            var value2 = value1.Add(TimeSpan.FromDays(1));
            var value3 = value1;

            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoValueType", value1, value3));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoValueType", value1, value2));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoValueType", value3, value2));
        }

        [Fact]
        public void DoReferenceType_ShouldDoComparison()
        {
            Assert.False(InvokePrivateMethod<bool>(_inequalityNode, "DoReferenceType", this, this));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoReferenceType", this, this._inequalityNode));
            Assert.True(InvokePrivateMethod<bool>(_inequalityNode, "DoReferenceType", this._inequalityNode, new InequalityNode(new ConstantNode<int>(0), new ConstantNode<int>(0))));
        }
    }
}
