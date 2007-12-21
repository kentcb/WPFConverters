using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	//a node containing two children
	internal abstract class BinaryNode : Node
	{
		private readonly Node _leftNode;
		private readonly Node _rightNode;

		public Node LeftNode
		{
			get
			{
				return _leftNode;
			}
		}

		public Node RightNode
		{
			get
			{
				return _rightNode;
			}
		}

		protected abstract string OperatorSymbols
		{
			get;
		}

		protected BinaryNode(Node leftNode, Node rightNode)
		{
			Debug.Assert(leftNode != null);
			Debug.Assert(rightNode != null);
			_leftNode = leftNode;
			_rightNode = rightNode;
		}
	}
}
