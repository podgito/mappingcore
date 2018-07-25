using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientMapper
{
    public class HttpClientMapper : IHttpClientMapper
    {


        public HttpClientMapper(HttpClient httpClient, IMapper mapper)
        {
            this.HttpClient = httpClient;
            this.Mapper = mapper;
        }

        public HttpClient HttpClient { get; }
        public IMapper Mapper { get; }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            throw new NotImplementedException();
        }
    }
}
