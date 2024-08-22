using System.Text;
using System.Text.Json;
using Jose;

namespace Gis.Net.Core;

/// <summary>
/// Provides utility methods for working with networking and core operations.
/// </summary>
public static class NetCore
{
    /// <summary>
    /// Deserialize the given JSON string into an object of type T.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="jsonString">The JSON string to deserialize.</param>
    /// <returns>The deserialized object of type T.</returns>
    public static T? DeserializeString<T>(string jsonString) where T : class => JsonSerializer.Deserialize<T>(jsonString);

    /// <summary>
    /// Serializes an object to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="dto">The object to serialize.</param>
    /// <param name="options">The optional JsonSerializerOptions.</param>
    /// <returns>The JSON string representation of the serialized object.</returns>
    public static string SerializeObject<T>(T dto, JsonSerializerOptions? options = null) where T : class
    {
        options ??= new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(dto, options);
        return jsonString;
    }

    /// <summary>
    /// Creates a JWT token for the specified object using the provided secret.
    /// </summary>
    /// <param name="dto">The object to be encoded into the token.</param>
    /// <param name="secret">The secret key used for encoding the token.</param>
    /// <returns>The encoded JWT token.</returns>
    public static string? CreateJwtToken(object dto, string secret)
    {
        var secretAsByteArray = Encoding.UTF8.GetBytes(secret);
        return JWT.Encode(
            dto,
            secretAsByteArray,
            JwsAlgorithm.HS256
        );
    }

    /// <summary>
    /// Decodes a JWT token using the specified secret and returns the deserialized object.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="token">The JWT token to decode.</param>
    /// <param name="secret">A secret used to validate the token.</param>
    /// <returns>The deserialized object.</returns>
    public static T DecodeToken<T>(string token, string secret)
    {
        var secretAsByteArray = Encoding.UTF8.GetBytes(secret);
        var json = JWT.Decode(token, secretAsByteArray, JwsAlgorithm.HS256);
        if (json is null) throw new Exception("Cannot decode JWT token");
        var obj = JsonSerializer.Deserialize<T>(json);
        if (obj is null) throw new Exception($"Cannot deserialize JWT token to {typeof(T)}");
        return obj;
    }
}