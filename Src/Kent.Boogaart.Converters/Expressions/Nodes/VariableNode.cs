using System.Diagnostics;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.Expressions.Nodes
{
    // node to hold a reference to a variable
    internal sealed class VariableNode : Node
    {
        private static readonly ExceptionHelper exceptionHelper = new ExceptionHelper(typeof(VariableNode));

        private readonly int index;

        public VariableNode(int index)
        {
            Debug.Assert(index >= 0);
            this.index = index;
        }

        public override object Evaluate(NodeEvaluationContext evaluationContext)
        {
            Debug.Assert(evaluationContext != null);
            exceptionHelper.ResolveAndThrowIf(!evaluationContext.HasArgument(this.index), "ArgumentNotFound", this.index);

            // variable values are passed inside the context for each evaluation
            return evaluationContext.GetArgument(this.index);
        }
    }
}
