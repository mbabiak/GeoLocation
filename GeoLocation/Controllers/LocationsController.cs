using GeoLocation.DTOs;
using GeoLocation.Hubs;
using GeoLocation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GeoLocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly IHubContext<ProgressHub, IProgressHub> _progressHub;
        private readonly ICsvProcessingService _csvProcessingService;

        public LocationsController(ILogger<LocationsController> logger, IHubContext<ProgressHub, IProgressHub> progressHub, ICsvProcessingService csvProcessingService)
        {
            _logger = logger;
            _progressHub = progressHub;
            _csvProcessingService = csvProcessingService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromForm] GeoCoordinateDTO geoCoordinate)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(m => m.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var errorMessage = string.Join("; ", errors);
                _logger.LogError($"ModelState errors: {errorMessage}");

                return BadRequest(ModelState);
            }
            int result = 0;

            await foreach(var coordinatesInRange in _csvProcessingService.ProcessCsvFile(geoCoordinate))
            {
                await _progressHub.Clients.Client(geoCoordinate.HubConnectionId).CountUpdate(coordinatesInRange);
                _logger.LogInformation($"Number of coordinates in range: {coordinatesInRange}");
                result = coordinatesInRange;
            }

            return Ok(result);
        }
    }
}
