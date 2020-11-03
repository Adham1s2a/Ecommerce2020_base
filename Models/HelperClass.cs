using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public static class HelperClass
    {
        public static void Add<T>(this ISession iSession, string key, T data)
        {
            string serializedData = JsonConvert.SerializeObject(data);
            iSession.SetString(key, serializedData);
        }
        public static T Get<T>(this ISession iSession, string key)
        {
            var data = iSession.GetString(key);
            if (null != data)
                return JsonConvert.DeserializeObject<T>(data);
            return default(T);
        }

    }
}
