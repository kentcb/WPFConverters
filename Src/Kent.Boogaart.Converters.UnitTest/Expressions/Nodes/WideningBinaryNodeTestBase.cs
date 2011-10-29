using Kent.Boogaart.Converters.Expressions.Nodes;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest.Expressions.Nodes
{
    public abstract class WideningBinaryNodeTestBase : UnitTest
    {
        /// <summary>
        /// This is a special-case of <see cref="InvokePrivateMethod"/> that works specifically against the various Do methods defined by <see cref="WideningBinaryNode"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The result type.
        /// </typeparam>
        /// <param name="instance">
        /// The node against which to invoke the method.
        /// </param>
        /// <param name="methodName">
        /// The method name - must be one of the DoXxx methods defined by <see cref="WideningBinaryNode"/>.
        /// </param>
        /// <param name="arg1">
        /// The first argument to the Do method.
        /// </param>
        /// <param name="arg2">
        /// The second argument to the Do method.
        /// </param>
        /// <returns>
        /// The result of the call.
        /// </returns>
        protected T InvokeDoMethod<T>(object instance, string methodName, object arg1, object arg2)
        {
            object result = null;
            var args = new object[] { arg1, arg2, result };
            Assert.True(this.InvokePrivateMethod<bool>(instance, methodName, args));
            return (T)args[2];
        }
    }
}
