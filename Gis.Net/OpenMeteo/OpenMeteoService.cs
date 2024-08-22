using System.Text.Json;

namespace Gis.Net.OpenMeteo;

/// <summary>
/// An abstract base class for making API requests to the OpenMeteo service.
/// </summary>
public abstract class OpenMeteoService : IOpenMeteoService
{
    /// <summary>
    /// HttpClient used to send HTTP requests and receive HTTP responses from a resource identified by a URI.
    /// </summary>
    protected readonly HttpClient HttpClient;
    
    /// <summary>
    /// Initializes a new instance of the OpenMeteo class with an instance of HttpClient.
    /// </summary>
    /// <param name="httpClient">The HttpClient to be used for API requests.</param>
    protected OpenMeteoService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
    
    /// <summary>
    /// Constructs the URI parameters for the API request.
    /// </summary>
    /// <returns>A string representing additional URI parameters.</returns>
    protected abstract string UriParameters();

    /// <summary>
    /// Sends an API request to the OpenMeteo service and deserializes the response as a list of the specified type.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response as.</typeparam>
    /// <param name="uri">The URI of the API request.</param>
    /// <returns>The deserialized response as a list of the specified type.</returns>
    protected async Task<List<T>?> ApiRequest<T>(string uri) where T : class
    {
        var response = await HttpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<T>?>(responseBody);
    }
    
}