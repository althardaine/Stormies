using System;

namespace Stormies.Extensions
{
    public static class DateTimeExtensions
    {

        public static long ToMilliseconds(this DateTime dateTime)
        {
            var jan1St1970 = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)((DateTime.UtcNow - jan1St1970).TotalMilliseconds); 
        }

    }
}