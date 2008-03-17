using NUnit.Framework;
using Kent.Boogaart.Converters.Expressions.Nodes;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
	[TestFixture]
	public sealed class NodeTest : UnitTest
	{
		[Test]
		public void GetNodeValueType_ShouldReturnTypeOfGivenObject()
		{
			Assert.AreEqual(NodeValueType.Null, Node.GetNodeValueType(null));
			Assert.AreEqual(NodeValueType.Unknown, Node.GetNodeValueType(this));
			Assert.AreEqual(NodeValueType.Boolean, Node.GetNodeValueType(true));
			Assert.AreEqual(NodeValueType.String, Node.GetNodeValueType("abc"));
			Assert.AreEqual(NodeValueType.Byte, Node.GetNodeValueType((byte) 1));
			Assert.AreEqual(NodeValueType.Int16, Node.GetNodeValueType((short) 1));
			Assert.AreEqual(NodeValueType.Int32, Node.GetNodeValueType(1));
			Assert.AreEqual(NodeValueType.Int64, Node.GetNodeValueType(1L));
			Assert.AreEqual(NodeValueType.Single, Node.GetNodeValueType(1f));
			Assert.AreEqual(NodeValueType.Double, Node.GetNodeValueType(1d));
			Assert.AreEqual(NodeValueType.Decimal, Node.GetNodeValueType(1m));
		}

		[Test]
		public void IsNumericalNodeValueType_ShouldReturnTrueOnlyForNumericalTypes()
		{
			Assert.IsFalse(Node.IsNumericalNodeValueType(NodeValueType.Unknown));
			Assert.IsFalse(Node.IsNumericalNodeValueType(NodeValueType.Null));
			Assert.IsFalse(Node.IsNumericalNodeValueType(NodeValueType.Boolean));
			Assert.IsFalse(Node.IsNumericalNodeValueType(NodeValueType.String));
			Assert.IsTrue(Node.IsNumericalNodeValueType(NodeValueType.Byte));
			Assert.IsTrue(Node.IsNumericalNodeValueType(NodeValueType.Int16));
			Assert.IsTrue(Node.IsNumericalNodeValueType(NodeValueType.Int32));
			Assert.IsTrue(Node.IsNumericalNodeValueType(NodeValueType.Int64));
			Assert.IsTrue(Node.IsNumericalNodeValueType(NodeValueType.Single));
			Assert.IsTrue(Node.IsNumericalNodeValueType(NodeValueType.Double));
			Assert.IsTrue(Node.IsNumericalNodeValueType(NodeValueType.Decimal));
		}

		[Test]
		public void IsIntegralNodeValueType_ShouldReturnTrueOnlyForIntegralTypes()
		{
			Assert.IsFalse(Node.IsIntegralNodeValueType(NodeValueType.Unknown));
			Assert.IsFalse(Node.IsNumericalNodeValueType(NodeValueType.Null));
			Assert.IsFalse(Node.IsIntegralNodeValueType(NodeValueType.Boolean));
			Assert.IsFalse(Node.IsIntegralNodeValueType(NodeValueType.String));
			Assert.IsTrue(Node.IsIntegralNodeValueType(NodeValueType.Byte));
			Assert.IsTrue(Node.IsIntegralNodeValueType(NodeValueType.Int16));
			Assert.IsTrue(Node.IsIntegralNodeValueType(NodeValueType.Int32));
			Assert.IsTrue(Node.IsIntegralNodeValueType(NodeValueType.Int64));
			Assert.IsFalse(Node.IsIntegralNodeValueType(NodeValueType.Single));
			Assert.IsFalse(Node.IsIntegralNodeValueType(NodeValueType.Double));
			Assert.IsFalse(Node.IsIntegralNodeValueType(NodeValueType.Decimal));
		}
	}
}
