using System;

namespace GDAXClient.Utilities
{
    public class Clock : IClock
    {
        public DateTime GetTime()
        {
            return DateTime.UtcNow;
        }
    }
}
