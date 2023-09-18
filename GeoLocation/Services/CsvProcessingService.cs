using CsvHelper;
using GeoLocation.DTOs;
using GeoLocation.Models;
using GeoLocation.Services.Interfaces;
using System.Globalization;

namespace GeoLocation.Services
{
    public class CsvProcessingService : ICsvProcessingService
    {
        private readonly IGeoCoordinatesService _geoCoordinatesService;
        private readonly int BATCH_SIZE = 1234;

        public CsvProcessingService(IGeoCoordinatesService geoCoordinatesService)
        {
            _geoCoordinatesService = geoCoordinatesService;
        }

        public async IAsyncEnumerable<int> ProcessCsvFile(GeoCoordinateDTO dto)
        {
            int processedLines = 0;
            var center = new GeoCoordinate(dto.CenterLatitude, dto.CenterLongitude);
            var radius = _geoCoordinatesService.CalculateDistance(center, new GeoCoordinate(dto.BorderLatitude, dto.BorderLongitude));
            using (var streamReader = new StreamReader(dto.File.OpenReadStream()))
            using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GeoCoordinateMap>();
                await foreach (var x in csv.GetRecordsAsync<GeoCoordinate>().Where(x => _geoCoordinatesService.CalculateDistance(x, center) <= radius))
                {
                    processedLines++;
                    if (processedLines % BATCH_SIZE == 0)
                    {
                        yield return processedLines;
                    }
                }
            }
            yield return processedLines;
        }
    }
}
