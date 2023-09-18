using GeoLocation.Models;

namespace GeoLocation.Services.Interfaces
{
    public interface IGeoCoordinatesService
    {
        double CalculateDistance(GeoCoordinate coordinate1, GeoCoordinate coordinate2);
    }
}
