using AirportsApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using AirportsApi.DataContracts;
using AirportsApi.Configuration;
using AirportsApi.Services.Interfaces;

namespace Airports.Controllers
{
    [Route("api/[controller]")]
    public class AirportsController : Controller
    {
        private readonly IAirportsService _airportsService;

        public AirportsController(IAirportsService airportsService)
        {
            _airportsService = airportsService;
        }

        [HttpGet("{from}/{to}")]
        public IActionResult MeasureDistanceInMiles(string from, string to)
        {
            if (!Validation.IsAirportCodeValid(from))
                return this.BadRequest(AppConfig.NOT_VALID_AIRPORT_FROM);

            if (!Validation.IsAirportCodeValid(to))
                return this.BadRequest(AppConfig.NOT_VALID_AIRPORT_TO);

            var airportFrom = _airportsService.GetAirportLocation(from.ToUpper());
            if (airportFrom == null)
                return this.BadRequest(AppConfig.NOT_VALID_AIRPORT_FROM);

            var airportTo = _airportsService.GetAirportLocation(to.ToUpper());
            if (airportTo ==null)
                return this.BadRequest(AppConfig.NOT_VALID_AIRPORT_TO);

            double miles = _airportsService.GetDistanceInMiles(airportFrom, airportTo);

            var response = new AirportResponse()
            {
                UnitValue = miles,
                UnitName = AppConfig.MILES_UNIT
            };

            return this.Ok(response);
        }
    }
}
