using GeoLocation.Models;
using GeoLocation.Services.Interfaces;

namespace GeoLocation.Services
{
    public class GeoCoordinatesService : IGeoCoordinatesService
    {
        private const double EarthRadiusKm = 6371.0;

        public double CalculateDistance(GeoCoordinate coordinate1, GeoCoordinate coordinate2)
        {
            double dLat = ToRadians(coordinate2.Latitude - coordinate1.Latitude);
            double dLon = ToRadians(coordinate2.Longitude - coordinate1.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(coordinate1.Latitude)) * Math.Cos(ToRadians(coordinate2.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
