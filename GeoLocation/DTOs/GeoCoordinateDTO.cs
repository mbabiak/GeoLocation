using GeoLocation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GeoLocation.DTOs
{
    public class GeoCoordinateDTO
    {
        [Required(ErrorMessage = "File is required.")]
        [FileExtension(".csv")]
        public IFormFile File { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Invalid longitude value.")]
        public double CenterLongitude { get; set; }

        [Required]
        [Range(-90, 90, ErrorMessage = "Invalid latitude value.")]
        public double CenterLatitude { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Invalid border longitude value.")]
        public double BorderLongitude { get; set; }

        [Required]
        [Range(-90, 90, ErrorMessage = "Invalid border latitude value.")]
        public double BorderLatitude { get; set; }

        public string? HubConnectionId { get; set; }
    }
}
