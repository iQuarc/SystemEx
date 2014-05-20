using System;

namespace iQuarc.SystemEx.Priority
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class PriorityAttribute : Attribute
    {
        public PriorityAttribute(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }
}