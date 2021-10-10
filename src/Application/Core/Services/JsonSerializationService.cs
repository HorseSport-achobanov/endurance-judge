using EnduranceJudge.Core.ConventionalServices;
using JsonNet.PrivatePropertySetterResolver;
using Newtonsoft.Json;
using System;

namespace EnduranceJudge.Application.Core.Services
{
    public class JsonSerializationService : IJsonSerializationService
    {
        private readonly JsonSerializerSettings settings;

        public JsonSerializationService()
        {
            this.settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivatePropertySetterResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            };
        }

        public string Serialize(object data)
        {
            var serialized = JsonConvert.SerializeObject(data);
            return serialized;
        }

        public T Deserialize<T>(string serialized)
        {
            var deserialized = JsonConvert.DeserializeObject<T>(serialized, this.settings);
            return deserialized;
        }

        public object Deserialize(string serialized, Type resultType)
        {
            var deserialized = JsonConvert.DeserializeObject(serialized, resultType, this.settings);
            return deserialized;
        }
    }

    public interface IJsonSerializationService : IService
    {
        string Serialize(object data);
        T Deserialize<T>(string serialized);
        object Deserialize(string serialized, Type resultType);
    }
}
