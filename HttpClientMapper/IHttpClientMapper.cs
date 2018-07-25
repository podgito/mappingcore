using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientMapper
{
    public interface IHttpClientMapper
    {

        //public IHttpClientMapper(HttpClient httpClient, IMapper mapper)
        //{

        //}



        // Summary:
        //     Send a GET request to the specified Uri as an asynchronous operation.
        //
        // Parameters:
        //   requestUri:
        //     The Uri the request is sent to.
        //
        // Returns:
        //     The task object representing the asynchronous operation.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The requestUri was null.
        //
        //   T:System.Net.Http.HttpRequestException:
        //     The request failed due to an underlying issue such as network connectivity, DNS
        //     failure, server certificate validation or timeout.
        Task<HttpResponseMessage> GetAsync(string requestUri);





    }
}
