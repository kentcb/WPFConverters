using System.Diagnostics;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
	internal sealed class NodeEvaluationContext
	{
		private readonly object[] _arguments;

		public static readonly NodeEvaluationContext Empty = new NodeEvaluationContext(new object[] { });

		public NodeEvaluationContext(object[] arguments)
		{
			Debug.Assert(arguments != null);
			_arguments = arguments;
		}

		public bool HasArgument(int index)
		{
			Debug.Assert(index >= 0);
			return index < _arguments.Length;
		}

		public object GetArgument(int index)
		{
			Debug.Assert(index >= 0);
			return _arguments[index];
		}
	}
}
