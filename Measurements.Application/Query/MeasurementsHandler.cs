using System;
using MediatR;
using System.Linq;
using System.Threading;
using Measurements.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Measurements.Application.Query
{
    public class MeasurementsHandler : IRequestHandler<MeasurementsCommand, List<MeasuredData>>
    {
        private readonly DbContext context;

        public MeasurementsHandler(DbContext _context)
        {
            this.context = _context;
        }

        public async Task<List<MeasuredData>> Handle(MeasurementsCommand request, CancellationToken cancellationToken)
        {
            if (!this.context.Database.CanConnect()) this.context.Database.EnsureCreated();

            List<MeasuredData> ListMeasuredData = await this.context.Set<MeasuredData>()
                                                                    .Where(p => p.DeviceId == request.DeviceId && p.MeasureDate.Date == request.MeasureDate.Date)
                                                                    .ToListAsync(cancellationToken);

            if (ListMeasuredData == null || ListMeasuredData.Count == 0) return null;

            if (!string.IsNullOrWhiteSpace(request.SensorType)) ListMeasuredData = ListMeasuredData.FindAll(p => p.SensorType == request.SensorType).ToList();

            if (ListMeasuredData == null || ListMeasuredData.Count == 0) return null;

            return ListMeasuredData;
        }
    }
}
