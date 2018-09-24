using Ninject;
using Mapping.GeolocationServices.IPLocationServices;
using Mapping.GeolocationServices.IPLocationServices.IPStack;
using System.Net.Http;
using Mapping.GeolocationServices.IPLocationServices.IPStack;
using System.Collections;
using System;

namespace Mapping.Function
{
    public class DIContainer
    {
        static IKernel kernel;
        static DIContainer()
        {
            //Register services
            kernel = new StandardKernel();

            //General
            kernel.Bind<HttpClient>().ToMethod(ctx => HttpClientFactory.Create());
            kernel.Bind<IApiConfiguration>().To<IPStackApiConfiguration>();
            kernel.Bind<IPStackApiConfiguration>().ToSelf();
            kernel.Bind<IDictionary>().ToConstant(Environment.GetEnvironmentVariables());


            //IP Geo-location Service
            kernel.Bind<IPLocationService>().To<IPStackLocationService>();

            
        }

        public static IKernel GetKernel()
        {
            return kernel;
        }

    }
}
