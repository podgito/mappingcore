using System;
using System.Collections.Generic;
using System.Text;

namespace MapPing.Geolocation.IPLocationServices
{
    public interface IPLocationService
    {
        Position GetPosition(string ipAddress);
    }
}
