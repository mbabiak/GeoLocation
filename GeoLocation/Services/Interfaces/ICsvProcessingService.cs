using GeoLocation.DTOs;

namespace GeoLocation.Services.Interfaces
{
    public interface ICsvProcessingService
    {
        IAsyncEnumerable<int> ProcessCsvFile(GeoCoordinateDTO geoCoordinateDTO);
    }
}
