using System;

namespace GDAXSharp.Utilities
{
    public class Clock : IClock
    {
        public DateTime GetTime()
        {
            return DateTime.UtcNow;
        }
    }
}
