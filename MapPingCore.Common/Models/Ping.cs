using System.ComponentModel.DataAnnotations;

namespace MapPingCore.Common.Models
{
    public class Ping
    {
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-90, 90)]
        public double Longitude { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Value { get; set; }
    }
}