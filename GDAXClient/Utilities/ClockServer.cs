using GDAXClient.Services.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAXClient.Utilities
{
    public class ClockServer : IClock
    {
        private TimeService timeService;
        public DateTime GetTime()
        {
            var time = Task.Run(()=>timeService.GetTimeAsync()).Result;
            return DateTime.Parse(time.Iso).ToUniversalTime();
        }

        public void SetTimeService(TimeService timeService)
        {
            this.timeService = timeService;
        }
    }
}
