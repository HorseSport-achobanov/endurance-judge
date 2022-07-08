using EnduranceJudge.Core.ConventionalServices;
using JsonNet.PrivatePropertySetterResolver;
using Newtonsoft.Json;
using System;

namespace EnduranceJudge.Application.Core.Services;

public class JsonSerializationService : IJsonSerializationService
{
    public static void Configure(JsonSerializerSettings settings)
    {
        settings.ContractResolver = new PrivatePropertySetterResolver();
        settings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
        settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }

    private readonly JsonSerializerSettings settings;

    public JsonSerializationService()
    {
        this.settings = new JsonSerializerSettings();
        Configure(this.settings);
    }

    public string Serialize(object data)
    {
        this.settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        var serialized = JsonConvert.SerializeObject(data, this.settings);
        return serialized;
    }

    public T Deserialize<T>(string serialized)
    {
        var deserialized = JsonConvert.DeserializeObject<T>(serialized, this.settings)!;
        return deserialized;
    }

    public object Deserialize(string serialized, Type resultType)
    {
        var deserialized = JsonConvert.DeserializeObject(serialized, resultType, this.settings)!;
        return deserialized;
    }
}

public interface IJsonSerializationService : ITransientService
{
    string Serialize(object data);
    T Deserialize<T>(string serialized);
    object Deserialize(string serialized, Type resultType);
}
