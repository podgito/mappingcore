using System;
using System.Collections;
using System.Linq;

namespace Mapping.Function.Configuration
{
    public class FuncConfig
    {
        private static IDictionary config;

        static FuncConfig()
        {
            config = Environment.GetEnvironmentVariables();
        }

        public T Get<T>(string key)
        {
            var keys = config.Keys.OfType<string>().Where(s => s.Contains(key));
            return default(T);
        }
    }
}