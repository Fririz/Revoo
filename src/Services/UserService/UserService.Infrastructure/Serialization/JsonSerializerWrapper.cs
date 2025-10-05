using System.Text.Json;

namespace UserService.Infrastracture.Serialization;

public class JsonSerializerWrapper : ISerializer
{
    public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj);
    public T? Deserialize<T>(string data) => JsonSerializer.Deserialize<T>(data);
}