using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientMapper.Extensions
{
    public static class ReadAsExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public static Task<TDest> ReadAsAsync<TSource, TDest>(this HttpClientMapper client)
        {
            throw new NotImplementedException();
        }
    }
}
