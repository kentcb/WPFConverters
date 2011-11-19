using System;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class EqualityNodeTest : WideningBinaryNodeTestBase
    {
        private EqualityNode equalityNode;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            this.equalityNode = new EqualityNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
        }

        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            Assert.Equal("==", GetPrivateMemberValue<string>(this.equalityNode, "OperatorSymbols"));
        }

        [Fact]
        public void DoString_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoString", "abc", "abc"));
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoString", null, null));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoString", "abc", null));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoString", "abc", "abcd"));
        }

        [Fact]
        public void DoBoolean_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoBoolean", true, true));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoBoolean", true, false));
        }

        [Fact]
        public void DoByte_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoByte", (byte)1, (byte)1));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoByte", (byte)1, (byte)2));
        }

        [Fact]
        public void DoInt16_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoInt16", (short)1, (short)1));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoInt16", (short)1, (short)2));
        }

        [Fact]
        public void DoInt32_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoInt32", 1, 1));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoInt32", 1, 2));
        }

        [Fact]
        public void DoInt64_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoInt64", 1L, 1L));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoInt64", 1L, 2L));
        }

        [Fact]
        public void DoSingle_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoSingle", 1f, 1f));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoSingle", 1f, 2f));
        }

        [Fact]
        public void DoDouble_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoDouble", 1d, 1d));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoDouble", 1d, 2d));
        }

        [Fact]
        public void DoDecimal_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoDecimal", 1m, 1m));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoDecimal", 1m, 2m));
        }

        [Fact]
        public void DoValueType_ShouldDoComparison()
        {
            var value1 = DateTime.UtcNow;
            var value2 = value1.Add(TimeSpan.FromDays(1));
            var value3 = value1;

            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoValueType", value1, value3));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoValueType", value1, value2));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoValueType", value3, value2));
        }

        [Fact]
        public void DoReferenceType_ShouldDoComparison()
        {
            Assert.True(InvokeDoMethod<bool>(this.equalityNode, "DoReferenceType", this, this));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoReferenceType", this, this.equalityNode));
            Assert.False(InvokeDoMethod<bool>(this.equalityNode, "DoReferenceType", this.equalityNode, new EqualityNode(new ConstantNode<int>(0), new ConstantNode<int>(0))));
        }
    }
}
