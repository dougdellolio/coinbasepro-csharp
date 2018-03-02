namespace GDAXClient.Services.Products.Models
{
    /// <summary>
    /// The granularity field must be one of the following values: {60, 300, 900, 3600, 21600, 86400}. 
    /// These values correspond to timeslices representing:
    /// one minute, five minutes, fifteen minutes, one hour, six hours, and one day, respectively.
    /// </summary>
    public enum CandleGranularity : int
    {
        Minutes1 = 60,
        Minutes5 = 300,
        Minutes15 = 900,
        Hour1 = 3600,
        Hour6 = 21600,
        Hour24 = 86400
    }
}
