using CsvHelper.Configuration;

namespace GeoLocation.Models
{
    public class GeoCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

    public class GeoCoordinateMap : ClassMap<GeoCoordinate>
    {
        public GeoCoordinateMap()
        {
            Parameter("latitude").Name("Latitude");
            Parameter("longitude").Name("Longitude");
        }
    }
}
