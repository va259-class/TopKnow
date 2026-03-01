using System.Text.Json;

namespace TopKnow.Management.Client.HttpClients;

public class ManagementApi
{
	private readonly HttpClient httpClient;

	public ManagementApi(HttpClient httpClient)
    {
		this.httpClient = httpClient;
	}

	public async Task<T> SendGetRequest<T>(string url)
	{
		var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
			return default;
        }
		var content = await response.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<T>(content); //TODO: value null geldiği için sayfaya yansıtılamadı
	}

	public Task SendPostRequest<T>(string url)
	{
		return Task.CompletedTask;
	}
}
