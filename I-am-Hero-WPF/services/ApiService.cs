using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080/")
        };
    }

    public async Task<string> Register(string email, string password)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { Email = email, Password = password }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("api/Users/register", content);

        if (response.IsSuccessStatusCode)
        {
            return "Регистрация успешна!";
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            return error;
        }
    }

    public async Task<string> Login(string email, string password)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { Email = email, Password = password }),
            Encoding.UTF8,
            "application/json"
        );

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsync("api/Users/login", content);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                return token;
            }
            else
            {
                return "Ошибка: " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            return "Ошибка подключения: " + ex.Message;
        }
    }
}
