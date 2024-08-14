﻿using System.Text;
using System.Text.Json;
using Jose;

namespace Gis.Net.Core;

public static class NetCore
{
    public static T? DeserializeString<T>(string jsonString) where T : class => JsonSerializer.Deserialize<T>(jsonString);

    public static string SerializeObject<T>(T dto, JsonSerializerOptions? options = null) where T : class
    {
        options ??= new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(dto, options);
        return jsonString;
    }

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
    /// Decodes a JWT token and deserializes it to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the token to.</typeparam>
    /// <param name="token">The JWT token to decode.</param>
    /// <param name="secret">The secret key for decoding the token.</param>
    /// <returns>The deserialized object of type T from the token.</returns>
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