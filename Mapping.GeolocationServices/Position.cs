using System;
using System.Collections.Generic;
using System.Text;

namespace Mapping.GeolocationServices
{
    public class Position
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
 
        public string CountryCode { get; set; } 
        public string City { get; set; }

        /// <summary>
        /// County | State | Province
        /// </summary>
        public string Region { get; set; }


    }
}
