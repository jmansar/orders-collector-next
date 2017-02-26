using System;

namespace OrdersCollector.Utils.Time
{
    public static class Clock
    {
        static Clock()
        {
            ResetToDefault();
        }

        internal static Func<DateTimeOffset> UtcDateTimeProvider { get; set; }

        internal static void ResetToDefault()
        {
            UtcDateTimeProvider = () => DateTimeOffset.UtcNow;
        }
        
        public static DateTimeOffset UtcNow => UtcDateTimeProvider(); 
    }
}