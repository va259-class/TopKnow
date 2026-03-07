using System.Net;
using System.Text;
using System.Text.Json;
using TopKnow.Management.Client.Helpers;

namespace TopKnow.Management.Client.HttpClients;

public class ManagementApi
{
	private readonly HttpClient httpClient;

	public ManagementApi(HttpClient httpClient)
    {
		this.httpClient = httpClient;
	}

	public async Task<Result<T>> SendGetRequest<T>(string url)
	{
		var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
			return default;
        }

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
			return new Result<T> { Error = new Error("0001", "Empty Response") };
        }
        var content = await response.Content.ReadAsStringAsync();
		var jsonSettings = new JsonSerializerOptions 
		{ 
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
		};
		return JsonSerializer.Deserialize<Result<T>>(content, jsonSettings);
	}

	public async Task<Result<T>> SendPostRequest<T, R>(string url, R data)
	{
		var json = JsonSerializer.Serialize(data);
		var payload = new StringContent(json, Encoding.UTF8, "application/json");
		var response = await httpClient.PostAsync(url, payload);
        if (!response.IsSuccessStatusCode)
        {
            return default;
        }

		var content = await response.Content.ReadAsStringAsync();
        var jsonSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return JsonSerializer.Deserialize<Result<T>>(content, jsonSettings);
    }
}
