using System;
using MediatR;
using Measurements.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Measurements.Application.Query;

namespace Measurements.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IMediator mediator;

        public DevicesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets weather information for the device and the sensor on a particular date
        /// </summary>
        /// <param name="deviceId">DeviceId</param>
        /// <param name="date">Date</param>
        /// <param name="sensorType">SensorType</param>
        /// <returns>The job information.</returns>
        /// <response code="200">The weather information.</response>
        /// <response code="400">Date supplied is not valid.</response>
        /// <response code="404">Weather information not available for the date.</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/Devices/testdevice/data/temperature/getdata
        ///     
        /// </remarks>
        [HttpGet("testdevice/data/temperature/getdata")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MeasuredData>>> GetData([FromQuery] string deviceId, [FromQuery] string date, [FromQuery] string sensorType)
        {
            if (!DateTime.TryParse(date, out DateTime measureDate)) return BadRequest("Date supplied is not valid");

            List<MeasuredData> response = await this.mediator.Send(new MeasurementsCommand(deviceId, sensorType, measureDate));
            if (response != null) return Ok(response);
            return NotFound("Data not found for the search criteria");
        }

        /// <summary>
        /// Gets weather information for the device on a particular date
        /// </summary>
        /// <param name="deviceId">DeviceId.</param>
        /// <param name="date">date.</param>
        /// <returns>The job information.</returns>
        /// <response code="200">The weather information.</response>
        /// <response code="400">Date supplied is not valid.</response>
        /// <response code="404">Weather information not available for the date.</response>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/v1/Devices/testdevice/data/getdatafordevice
        ///     
        /// </remarks>
        [HttpGet("testdevice/data/getdatafordevice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MeasuredData>>> GetData([FromQuery] string deviceId, [FromQuery] string date)
        {
            if (!DateTime.TryParse(date, out DateTime measureDate)) return BadRequest("Date supplied is not valid");

            List<MeasuredData> response = await this.mediator.Send(new MeasurementsCommand(deviceId, null, measureDate));
            if (response != null) return Ok(response);
            return NotFound("Data not found for the search criteria");
        }
    }
}
