using System;

namespace iQuarc.SystemEx
{
    public static class ExceptionExtensions
    {
        public static T FirstInner<T>(this Exception exception) where T : Exception
        {
            if (exception == null)
                return null;

            if (exception is T)
                return (T)exception;

            return exception.InnerException.FirstInner<T>();
        }

        public static Exception InnermostException(this Exception exception)
        {
            if (exception == null)
                return null;

            if (exception.InnerException == null)
                return exception;

            return exception.InnerException.InnermostException();
        }
    }
}