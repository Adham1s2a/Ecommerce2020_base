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
        public  static void SetObjectAsJson(this ISession session,string key,object value )
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session ,string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
        //public static void Add<T>(this ISession iSession, string key, T data)
        //{
        //    string serializedData = JsonConvert.SerializeObject(data);
        //    iSession.SetString(key, serializedData);
        //}
        //public static T Get<T>(this ISession iSession, string key)
        //{
        //    var data = iSession.GetString(key);
        //    if (null != data)
        //        return JsonConvert.DeserializeObject<T>(data);
        //    return default(T);
        //}

    }
}
