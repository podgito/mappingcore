using System;
using System.Collections.Generic;
using System.Text;

namespace MapPing.Geolocation.Exceptions
{
    public class IPGeolationServiceUnavailableException: Exception
    {
        
    }

    public class IPGeolocationServiceTemporarilyUnavailableExcepion : IPGeolationServiceUnavailableException { }
    public class IPGeolocationServiceRateExceededExcepion : IPGeolationServiceUnavailableException { }
    public class IPGeolocationServiceClientErrorExcepion : IPGeolationServiceUnavailableException { }
    public class IPGeolocationServicePermanentlyUnavailableExcepion : IPGeolationServiceUnavailableException { }

}
