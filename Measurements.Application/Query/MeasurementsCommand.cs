using System;
using MediatR;
using Measurements.Domain;
using System.Collections.Generic;

namespace Measurements.Application.Query
{
    public class MeasurementsCommand : IRequest<List<MeasuredData>>
    {
        public string DeviceId { get; set; }
        public string SensorType { get; set; }
        public DateTime MeasureDate { get; set; }

        public MeasurementsCommand(string _deviceId, string _sensorType, DateTime _measureDate)
        {
            DeviceId = _deviceId;
            SensorType = _sensorType;
            MeasureDate = _measureDate;
        }
    }
}
