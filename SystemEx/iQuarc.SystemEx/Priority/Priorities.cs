namespace iQuarc.SystemEx.Priority
{
    public static class Priorities
    {
        public const int VeryHigh = 0;
        public const int High = int.MaxValue / 4;
        public const int Medium = int.MaxValue / 2;
        public const int Low = (int.MaxValue / 4) * 3;
        public const int VeryLow = int.MaxValue;
    }
}