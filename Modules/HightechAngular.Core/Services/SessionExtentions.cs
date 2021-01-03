using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HightechAngular.Orders.Services
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
            where T: class
        {
            session.SetString(key,  JsonConvert.SerializeObject(value));
        }

        public static T? Get<T>(this ISession session, string key)
            where T: class
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}