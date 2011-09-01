using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace Kent.Boogaart.Converters.UnitTest
{
	/// <summary>
	/// A base class for exception unit tests.
	/// </summary>
	/// <typeparam name="T">
	/// The exception type under test.
	/// </typeparam>
	public abstract class ExceptionTest<T> : UnitTest
		where T : Exception, new()
	{
		private T _exception;
		private readonly Type _type = typeof(T);

		[Fact]
		public void Constructor_Default_ShouldSetUpDefaults()
		{
			_exception = new T();
			Assert.Equal("Exception of type '" + _type.FullName + "' was thrown.", _exception.Message);
			Assert.Null(_exception.InnerException);
			Assert.Null(_exception.StackTrace);
		}

		[Fact]
		public void Constructor_Message_ShouldAssignDefaultMessageIfNoMessageSpecified()
		{
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
			Assert.NotNull(constructor);
			_exception = constructor.Invoke(new object[] { null }) as T;
			Assert.NotNull(_exception);
			Assert.Equal("Exception of type '" + _type.FullName + "' was thrown.", _exception.Message);
			Assert.Null(_exception.InnerException);
			Assert.Null(_exception.StackTrace);
		}

		[Fact]
		public void Constructor_Message_ShouldAssignMessage()
		{
			string message = "my message";
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
			Assert.NotNull(constructor);
			_exception = constructor.Invoke(new object[] { message }) as T;
			Assert.NotNull(_exception);
			Assert.Equal(message, _exception.Message);
			Assert.Null(_exception.InnerException);
			Assert.Null(_exception.StackTrace);
		}

		[Fact]
		public void Constructor_InnerException_ShouldAssignInnerException()
		{
			string message = "my message";
			Exception inner = new NullReferenceException();
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
			Assert.NotNull(constructor);
			_exception = constructor.Invoke(new object[] { message, inner }) as T;
			Assert.NotNull(_exception);
			Assert.Equal(message, _exception.Message);
			Assert.Equal(inner, _exception.InnerException);
			Assert.Null(_exception.StackTrace);
		}

		[Fact]
		public void Constructor_Serialization_ShouldSerializeAndDeserializeException()
		{
			IFormatter formatter = new BinaryFormatter();
			string message = "my message";
			Exception inner = new NullReferenceException();
			ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
			Assert.NotNull(constructor);
			_exception = constructor.Invoke(new object[] { message, inner }) as T;

			using (MemoryStream stream = new MemoryStream())
			{
				formatter.Serialize(stream, _exception);
				stream.Flush();
				stream.Position = 0;
				_exception = (T) formatter.Deserialize(stream);
			}

			Assert.NotNull(_exception);
			Assert.Equal(message, _exception.Message);
			Assert.NotNull(_exception.InnerException);
			Assert.Null(_exception.StackTrace);
		}
	}
}
