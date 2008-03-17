using System;
using System.Reflection;
using NUnit.Framework;
using Rhino.Mocks;
using Kent.Boogaart.HelperTrinity;

namespace Kent.Boogaart.Converters.UnitTest
{
	/// <summary>
	/// A base class for all unit tests.
	/// </summary>
	public abstract class UnitTest : MarshalByRefObject
	{
		private MockRepository _mocks;

		/// <summary>
		/// Gets a repository of mock objects that subclasses can utilize in their implementation.
		/// </summary>
		protected MockRepository Mocks
		{
			get
			{
				return _mocks;
			}
		}

		[SetUp]
		public void SetUp()
		{
			_mocks = new MockRepository();
			SetUpCore();
		}

		[TearDown]
		public void TearDown()
		{
			TearDownCore();
			_mocks.ReplayAll();
			_mocks.VerifyAll();
		}

		/// <summary>
		/// Allows subclasses to hook into the set up process.
		/// </summary>
		protected virtual void SetUpCore()
		{
		}

		/// <summary>
		/// Allows subclasses to hook into the tear down process.
		/// </summary>
		protected virtual void TearDownCore()
		{
		}

		/// <summary>
		/// Reflectively invokes a method against <paramref name="instance"/>.
		/// </summary>
		/// <param name="instance">
		/// The object against which to invoke the method.
		/// </param>
		/// <param name="methodName">
		/// The name of the method to invoke.
		/// </param>
		/// <param name="args">
		/// Any arguments for the method invocation.
		/// </param>
		/// <returns>
		/// The result of invoking the method.
		/// </returns>
		protected T InvokePrivateMethod<T>(object instance, string methodName, params object[] args)
		{
			ArgumentHelper.AssertNotNull(instance, "instance");
			ArgumentHelper.AssertNotNull(methodName, "methodName");
			//may need to support method overloads at some stage
			MethodInfo methodInfo = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

			try
			{
				return (T) methodInfo.Invoke(instance, args);
			}
			catch (TargetInvocationException e)
			{
				throw e.InnerException;
			}
		}

		/// <summary>
		/// Reflectively obtains the value of a private field or property.
		/// </summary>
		/// <param name="instance">
		/// The object against which to retrieve the member value.
		/// </param>
		/// <param name="memberName">
		/// The name of the member to retrieve.
		/// </param>
		/// <returns>
		/// The value of the member.
		/// </returns>
		protected T GetPrivateMemberValue<T>(object instance, string memberName)
		{
			ArgumentHelper.AssertNotNull(instance, "instance");
			return GetPrivateMemberValue<T>(instance.GetType(), instance, memberName);
		}

		/// <summary>
		/// Reflectively obtains the value of a private field or property for a specified type.
		/// </summary>
		/// <param name="type">
		/// The <see cref="Type"/> that declares the field or property.
		/// </param>
		/// <param name="instance">
		/// The object against which to retrieve the member value.
		/// </param>
		/// <param name="memberName">
		/// The name of the member to retrieve.
		/// </param>
		/// <returns>
		/// The value of the member.
		/// </returns>
		protected T GetPrivateMemberValue<T>(Type type, object instance, string memberName)
		{
			ArgumentHelper.AssertNotNull(type, "type");
			ArgumentHelper.AssertNotNull(instance, "instance");
			ArgumentHelper.AssertNotNull(memberName, "memberName");
			MemberInfo[] memberInfos = type.GetMember(memberName, BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

			if (memberInfos.Length == 1)
			{
				if (memberInfos[0] is FieldInfo)
				{
					return (T) (memberInfos[0] as FieldInfo).GetValue(instance);
				}
				else if (memberInfos[0] is PropertyInfo)
				{
					return (T) (memberInfos[0] as PropertyInfo).GetValue(instance, null);
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
			else
			{
				throw new InvalidOperationException();
			}
		}
	}
}
