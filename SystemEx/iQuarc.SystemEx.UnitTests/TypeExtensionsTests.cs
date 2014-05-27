using System;
using iQuarc.xUnitEx;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void GetGenericInterface_ArgumentIsNull_ArgumentNullException()
        {
            Action act = () => TypeExtensions.GetGenericInterface(null, typeof (ISomeInterface));

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void GetGenericInterface_TypeDoesNotImplementTheInterface_NullReturned()
        {
            Type result = typeof (string).GetGenericInterface(typeof (ISomeInterface));

            Assert.Null(result);
        }

        [Fact]
        public void GetGenericInterface_InterfaceIsNotGeneric_NullReturned()
        {
            Type result = typeof (SomeType).GetGenericInterface(typeof (ISomeInterface));

            Assert.Null(result);
        }

        [Fact]
        public void GetGenericInterface_InterfaceIsGeneric_GenericInterfaceReturned()
        {
            Type result = typeof (GenericInterfaceImpl<int, string>).GetGenericInterface(typeof (IGenericInterface<,>));

            Assert.Equal(typeof (IGenericInterface<int, string>), result);
        }

        [Fact]
        public void GetGenericInterfaceArguments_DoesNotImplementInterface_InvalidOperationException()
        {
            Action act = () => typeof (SomeType).GetGenericInterfaceArguments(typeof (IGenericInterface<,>));

            act.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void GetGenericInterfaceArguments_InterfaceWithTwoGenericTypes_GenericArgumentsReturned()
        {
            Type[] result = typeof (GenericInterfaceImpl<int, string>).GetGenericInterfaceArguments(typeof (IGenericInterface<,>));

            AssertEx.AreEquivalent(result, typeof (int), typeof (string));
        }

        private interface ISomeInterface
        {
        }

        private class SomeType : ISomeInterface
        {
        }

        private interface IGenericInterface<T, V>
        {
        }

        private class GenericInterfaceImpl<T, V> : IGenericInterface<int, string>, ISomeInterface
        {
        }
    }
}