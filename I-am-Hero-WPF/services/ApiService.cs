using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080/")
        };

        string token = TokenStorage.LoadToken();
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    //Auth
    public async Task<string> Register(string email, string password)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { Email = email, Password = password }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("api/Auth/register", content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Login(string email, string password)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { Email = email, Password = password, ApplicationId = 2 }),
            Encoding.UTF8,
            "application/json"
        );

        HttpResponseMessage response = await _httpClient.PostAsync("api/Auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            string token = await response.Content.ReadAsStringAsync();
            TokenStorage.SaveToken(token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return token;
        }
        else
        {
            return "Ошибка: " + response.StatusCode;
        }
    }

    //Hero
    public async Task<HttpResponseMessage> GetHeroAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get");
        return response;
    }

    public async Task<string> CreateHeroAsync(string heroName)
    {
        var response = await _httpClient.PostAsync($"api/Hero/create?heroName={Uri.EscapeDataString(heroName)}", null);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<bool> IsTokenValid()
    {
        string token = TokenStorage.LoadToken();
        if (string.IsNullOrEmpty(token)) return false;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await _httpClient.GetAsync("api/Auth/validate");
        return response.IsSuccessStatusCode;
    }

    public void Logout()
    {
        TokenStorage.DeleteToken();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
