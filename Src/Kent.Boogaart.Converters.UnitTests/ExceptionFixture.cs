namespace Kent.Boogaart.Converters.UnitTests
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using Xunit;

    // a base class for exception unit tests
    public abstract class ExceptionTest<T>
        where T : Exception, new()
    {
        private static readonly Type type = typeof(T);

        [Fact]
        public void ctor_assigns_default_message()
        {
            var exception = new T();
            Assert.Equal("Exception of type '" + type.FullName + "' was thrown.", exception.Message);
        }

        [Fact]
        public void ctor_that_takes_message_assigns_default_message_if_given_message_is_null()
        {
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
            Assert.NotNull(constructor);
            var exception = constructor.Invoke(new object[] { null }) as T;
            Assert.NotNull(exception);
            Assert.Equal("Exception of type '" + type.FullName + "' was thrown.", exception.Message);
        }

        [Fact]
        public void ctor_that_takes_message_assigns_message()
        {
            var message = "my message";
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(string) });
            Assert.NotNull(constructor);
            var exception = constructor.Invoke(new object[] { message }) as T;
            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ctor_that_takes_inner_exception_assigns_inner_exception()
        {
            var message = "my message";
            var inner = new NullReferenceException();
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
            Assert.NotNull(constructor);
            var exception = constructor.Invoke(new object[] { message, inner }) as T;
            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.Equal(inner, exception.InnerException);
        }

        [Fact]
        public void ctor_for_serialization_allows_serialization_of_exception()
        {
            var formatter = new BinaryFormatter();
            var message = "my message";
            var inner = new NullReferenceException();
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(string), typeof(Exception) });
            Assert.NotNull(constructor);
            var exception = constructor.Invoke(new object[] { message, inner }) as T;

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, exception);
                stream.Flush();
                stream.Position = 0;
                exception = (T)formatter.Deserialize(stream);
            }

            Assert.NotNull(exception);
            Assert.Equal(message, exception.Message);
            Assert.NotNull(exception.InnerException);
        }
    }
}