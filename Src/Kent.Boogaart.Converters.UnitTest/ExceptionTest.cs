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
        private T exception;
        private readonly Type type = typeof(T);

        [Fact]
        public void Constructor_Default_ShouldSetUpDefaults()
        {
            this.exception = new T();
            Assert.Equal("Exception of type '" + this.type.FullName + "' was thrown.", this.exception.Message);
            Assert.Null(this.exception.InnerException);
            Assert.Null(this.exception.StackTrace);
        }

        [Fact]
        public void Constructor_Message_ShouldAssignDefaultMessageIfNoMessageSpecified()
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
            Assert.NotNull(constructor);
            this.exception = constructor.Invoke(new object[] { null }) as T;
            Assert.NotNull(this.exception);
            Assert.Equal("Exception of type '" + this.type.FullName + "' was thrown.", this.exception.Message);
            Assert.Null(this.exception.InnerException);
            Assert.Null(this.exception.StackTrace);
        }

        [Fact]
        public void Constructor_Message_ShouldAssignMessage()
        {
            string message = "my message";
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
            Assert.NotNull(constructor);
            this.exception = constructor.Invoke(new object[] { message }) as T;
            Assert.NotNull(this.exception);
            Assert.Equal(message, this.exception.Message);
            Assert.Null(this.exception.InnerException);
            Assert.Null(this.exception.StackTrace);
        }

        [Fact]
        public void Constructor_InnerException_ShouldAssignInnerException()
        {
            string message = "my message";
            Exception inner = new NullReferenceException();
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
            Assert.NotNull(constructor);
            this.exception = constructor.Invoke(new object[] { message, inner }) as T;
            Assert.NotNull(this.exception);
            Assert.Equal(message, this.exception.Message);
            Assert.Equal(inner, this.exception.InnerException);
            Assert.Null(this.exception.StackTrace);
        }

        [Fact]
        public void Constructor_Serialization_ShouldSerializeAndDeserializeException()
        {
            IFormatter formatter = new BinaryFormatter();
            string message = "my message";
            Exception inner = new NullReferenceException();
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
            Assert.NotNull(constructor);
            this.exception = constructor.Invoke(new object[] { message, inner }) as T;

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this.exception);
                stream.Flush();
                stream.Position = 0;
                this.exception = (T)formatter.Deserialize(stream);
            }

            Assert.NotNull(this.exception);
            Assert.Equal(message, this.exception.Message);
            Assert.NotNull(this.exception.InnerException);
            Assert.Null(this.exception.StackTrace);
        }
    }
}
