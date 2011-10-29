using System;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class EqualityNodeTest : WideningBinaryNodeTestBase
    {
        private EqualityNode _equalityNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _equalityNode = new EqualityNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("==", GetPrivateMemberValue<string>(_equalityNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoString_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoString", "abc", "abc"));
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoString", null, null));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoString", "abc", null));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoString", "abc", "abcd"));
        }

        [Fact]
        public void DoBoolean_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoBoolean", true, true));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoBoolean", true, false));
        }

        [Fact]
        public void DoByte_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoByte", (byte)1, (byte)1));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoByte", (byte)1, (byte)2));
        }

        [Fact]
        public void DoInt16_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoInt16", (short)1, (short)1));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoInt16", (short)1, (short)2));
        }

        [Fact]
        public void DoInt32_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoInt32", 1, 1));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoInt32", 1, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoInt64", 1L, 1L));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoInt64", 1L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoSingle", 1f, 1f));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoSingle", 1f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoDouble", 1d, 1d));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoDouble", 1d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoDecimal", 1m, 1m));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoDecimal", 1m, 2m));
        }

        [Fact]
        public void DoValueType_ShouldDoComparison()
        {
            var value1 = DateTime.UtcNow;
            var value2 = value1.Add(TimeSpan.FromDays(1));
            var value3 = value1;

            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoValueType", value1, value3));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoValueType", value1, value2));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoValueType", value3, value2));
        }

        [Fact]
        public void DoReferenceType_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(_equalityNode, "DoReferenceType", this, this));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoReferenceType", this, this._equalityNode));
            Assert.False(InvokeDoMethod<bool>(_equalityNode, "DoReferenceType", this._equalityNode, new EqualityNode(new ConstantNode<int>(0), new ConstantNode<int>(0))));
        }
    }
}
