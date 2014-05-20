using System;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class ExceptionExtensionsTests
    {
        [Fact]
        public void FirstInner_DoesNotHaveInnerException_ReturnsNull()
        {
            Exception e = new Exception();

            Exception actual = e.FirstInner<ArgumentException>();

            Assert.Null(actual);
        }

        [Fact]
        public void FirstInner_IsOfSameType_SameExceptionReturned()
        {
            Exception e = new Exception();

            var actual = e.FirstInner<Exception>();

            Assert.Same(e, actual);
        }

        [Fact]
        public void FirstInner_InheritedExceptionFirstInner_InheritedExceptionReturned()
        {
            ArgumentException ae = new ArgumentException();
            ArgumentNullException ane = new ArgumentNullException("", ae);
            Exception e = new Exception("", ane);

            var actual = e.FirstInner<ArgumentException>();

            Assert.Same(ane, actual);
        }

        [Fact]
        public void InnermostException_DoesNotHaveInnerException_ReturnsSameException()
        {
            Exception e = new Exception();

            Exception actual = e.InnermostException();

            Assert.Same(e, actual);
        }

        [Fact]
        public void InnermostException_IsNull_NullReturned()
        {
            Exception actual = ExceptionExtensions.InnermostException(null);

            Assert.Null(actual);
        }

        [Fact]
        public void InnermostException_InheritedExceptionFirstInner_DeepestInnerReturned()
        {
            ArgumentException ae = new ArgumentException();
            ArgumentNullException ane = new ArgumentNullException("", ae);
            Exception e = new Exception("", ane);

            var actual = e.InnermostException();

            Assert.Same(ae, actual);
        }
    }
}