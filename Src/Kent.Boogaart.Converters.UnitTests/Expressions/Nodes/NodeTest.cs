using System;
using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTests.Expressions.Nodes
{
    public sealed class NodeTest : UnitTest
    {
        [Fact]
        public void GetNodeValueType_ShouldReturnTypeOfGivenObject()
        {
            Assert.Equal(NodeValueType.Boolean, Node.GetNodeValueType(true));
            Assert.Equal(NodeValueType.String, Node.GetNodeValueType("abc"));
            Assert.Equal(NodeValueType.Byte, Node.GetNodeValueType((byte)1));
            Assert.Equal(NodeValueType.Int16, Node.GetNodeValueType((short)1));
            Assert.Equal(NodeValueType.Int32, Node.GetNodeValueType(1));
            Assert.Equal(NodeValueType.Int64, Node.GetNodeValueType(1L));
            Assert.Equal(NodeValueType.Single, Node.GetNodeValueType(1f));
            Assert.Equal(NodeValueType.Double, Node.GetNodeValueType(1d));
            Assert.Equal(NodeValueType.Decimal, Node.GetNodeValueType(1m));
            Assert.Equal(NodeValueType.ValueType, Node.GetNodeValueType(DateTime.UtcNow));
            Assert.Equal(NodeValueType.ReferenceType, Node.GetNodeValueType(null));
            Assert.Equal(NodeValueType.ReferenceType, Node.GetNodeValueType(this));
        }

        [Fact]
        public void IsNumericalNodeValueType_ShouldReturnTrueOnlyForNumericalTypes()
        {
            Assert.False(Node.IsNumericalNodeValueType(NodeValueType.Unknown));
            Assert.False(Node.IsNumericalNodeValueType(NodeValueType.ReferenceType));
            Assert.False(Node.IsNumericalNodeValueType(NodeValueType.ValueType));
            Assert.False(Node.IsNumericalNodeValueType(NodeValueType.Boolean));
            Assert.False(Node.IsNumericalNodeValueType(NodeValueType.String));
            Assert.True(Node.IsNumericalNodeValueType(NodeValueType.Byte));
            Assert.True(Node.IsNumericalNodeValueType(NodeValueType.Int16));
            Assert.True(Node.IsNumericalNodeValueType(NodeValueType.Int32));
            Assert.True(Node.IsNumericalNodeValueType(NodeValueType.Int64));
            Assert.True(Node.IsNumericalNodeValueType(NodeValueType.Single));
            Assert.True(Node.IsNumericalNodeValueType(NodeValueType.Double));
            Assert.True(Node.IsNumericalNodeValueType(NodeValueType.Decimal));
        }

        [Fact]
        public void IsIntegralNodeValueType_ShouldReturnTrueOnlyForIntegralTypes()
        {
            Assert.False(Node.IsIntegralNodeValueType(NodeValueType.Unknown));
            Assert.False(Node.IsNumericalNodeValueType(NodeValueType.ReferenceType));
            Assert.False(Node.IsNumericalNodeValueType(NodeValueType.ValueType));
            Assert.False(Node.IsIntegralNodeValueType(NodeValueType.Boolean));
            Assert.False(Node.IsIntegralNodeValueType(NodeValueType.String));
            Assert.True(Node.IsIntegralNodeValueType(NodeValueType.Byte));
            Assert.True(Node.IsIntegralNodeValueType(NodeValueType.Int16));
            Assert.True(Node.IsIntegralNodeValueType(NodeValueType.Int32));
            Assert.True(Node.IsIntegralNodeValueType(NodeValueType.Int64));
            Assert.False(Node.IsIntegralNodeValueType(NodeValueType.Single));
            Assert.False(Node.IsIntegralNodeValueType(NodeValueType.Double));
            Assert.False(Node.IsIntegralNodeValueType(NodeValueType.Decimal));
        }
    }
}
