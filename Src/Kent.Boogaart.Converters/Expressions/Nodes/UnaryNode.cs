using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node with a single child node
	internal abstract class UnaryNode : Node
	{
		private readonly Node _node;

		public Node Node
		{
			get
			{
				return _node;
			}
		}

		protected UnaryNode(Node node)
		{
			Debug.Assert(node != null);
			_node = node;
		}
	}
}
