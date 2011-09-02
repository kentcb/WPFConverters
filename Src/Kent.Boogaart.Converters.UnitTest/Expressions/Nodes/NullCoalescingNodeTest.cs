using System;
using Xunit;
using Kent.Boogaart.Converters.Expressions;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public sealed class NullCoalescingNodeTest : UnitTest
    {
        [Fact]
        public void OperatorSymbols_ShouldYieldCorrectOperatorSymbols()
        {
            var node = new NullCoalescingNode(new ConstantNode<int>(0), new ConstantNode<int>(0));
            Assert.Equal("??", GetPrivateMemberValue<string>(node, "OperatorSymbols"));
        }

        [Fact]
        public void Evaluate_ShouldReturnLeftNodeValueIfNonNull()
        {
            var node = new NullCoalescingNode(new ConstantNode<string>("foo"), new ConstantNode<string>("bar"));
            var value = node.Evaluate(NodeEvaluationContext.Empty);
            Assert.Equal("foo", value);
        }

        [Fact]
        public void Evaluate_ShouldReturnRightNodeValueIfLeftIsNull()
        {
            var node = new NullCoalescingNode(new ConstantNode<string>(null), new ConstantNode<string>("bar"));
            var value = node.Evaluate(NodeEvaluationContext.Empty);
            Assert.Equal("bar", value);
        }
    }
}