using System;

namespace Measurements.Domain
{
    public class MeasuredData
    {
        public string DeviceId { get; set; }
        public string SensorType { get; set; }
        public DateTime MeasureDate { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal Rainfall { get; set; }
    }
}
