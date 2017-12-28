using System;

namespace GDAXClient.Services.Time.Models.Responses
{
    public class TimeResponse
    {
        public DateTime Iso { get; set; }

        public decimal Epoch { get; set; }
    }
}
