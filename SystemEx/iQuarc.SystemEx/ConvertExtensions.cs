using System;
using System.Globalization;

namespace iQuarc.SystemEx
{
    public static class ConvertExtensions
    {
        public static T ChangeType<T>(object value)
        {
            return (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture);
        }

        public static T As<T>(this object value)
            where T : class
        {
            return value as T;
        }
    }
}