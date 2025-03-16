using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using I_am_Hero_WPF.Models;

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
    public async Task<bool> IsTokenValid()
    {
        string token = TokenStorage.LoadToken();
        if (string.IsNullOrEmpty(token)) return false;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get");
        return response.IsSuccessStatusCode;
    }
    public void Logout()
    {
        TokenStorage.DeleteToken();
        _httpClient.DefaultRequestHeaders.Authorization = null;
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


    //Hero Skills
    public async Task<HttpResponseMessage> GetHeroSkillsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get/HeroSkills");
        string json = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<HeroSkillsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (responseObject?.Token != null)
        {
            TokenStorage.SaveToken(responseObject.Token);
        }
        return response;
    }
    public async Task<HttpResponseMessage> CreateHeroSkillAsync(HeroSkill skill)
    {
        if (skill == null)
            throw new ArgumentNullException(nameof(skill));

        string json = JsonSerializer.Serialize(skill);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Hero/create/HeroSkill", content);
        return response;
    }
    public async Task<HttpResponseMessage> DeleteHeroSkillAsync(long id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid skill ID", nameof(id));

        return await _httpClient.DeleteAsync($"api/Hero/delete/HeroSkill?id={id}");
    }
    public async Task<HttpResponseMessage> EditHeroSkillAsync(HeroSkill skill)
    {
        if (skill == null)
            throw new ArgumentNullException(nameof(skill));

        string json = JsonSerializer.Serialize(skill);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync("api/Hero/edit/HeroSkill", content);
        return response;
    }


    //Hero Attributes
    public async Task<HttpResponseMessage> GetHeroAttributesAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get/HeroAttributes");
        string json = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<HeroAttributesResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (responseObject?.Token != null)
        {
            TokenStorage.SaveToken(responseObject.Token);
        }
        return response;
    }
    public async Task<HttpResponseMessage> CreateHeroAttributeAsync(HeroAttribute attribute)
    {
        if (attribute == null)
            throw new ArgumentNullException(nameof(attribute));

        string json = JsonSerializer.Serialize(attribute);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Hero/create/HeroAtrribute", content);
        return response;
    }
    public async Task<HttpResponseMessage> DeleteHeroAttributeAsync(long id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid attribute ID", nameof(id));

        return await _httpClient.DeleteAsync($"api/Hero/delete/HeroAttribute?id={id}");
    }

    public async Task<HttpResponseMessage> EditHeroAttributeAsync(HeroAttribute attribute)
    {
        if (attribute == null)
            throw new ArgumentNullException(nameof(attribute));

        string json = JsonSerializer.Serialize(attribute);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync("api/Hero/edit/HeroAttribute", content);
        return response;
    }



    //Hero Quests
    public async Task<HttpResponseMessage> GetHeroQuestsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get/Quests");
        //string json = await response.Content.ReadAsStringAsync();
        //var responseObject = JsonSerializer.Deserialize<HeroQuestsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //if (responseObject?.Token != null)
        //{
        //    TokenStorage.SaveToken(responseObject.Token);
        //}
        return response;
    }
    public async Task<HttpResponseMessage> CreateHeroQuestAsync(Quest quest)
    {
        if (quest == null)
            throw new ArgumentNullException(nameof(quest));

        string json = JsonSerializer.Serialize(quest);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Hero/create/Quest", content);
        return response;
    }



    //Hero Status Effects
    public async Task<HttpResponseMessage> GetHeroEffectsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get/HeroStatusEffects");
        string json = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<HeroStatusEffectsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (responseObject?.Token != null)
        {
            TokenStorage.SaveToken(responseObject.Token);
        }
        return response;
    }
    public async Task<HttpResponseMessage> CreateHeroEffectAsync(HeroStatusEffect effect)
    {
        if (effect == null)
            throw new ArgumentNullException(nameof(effect));

        string json = JsonSerializer.Serialize(effect);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Hero/create/HeroStatusEffect", content);
        return response;
    }
    public async Task<HttpResponseMessage> DeleteHeroEffectAsync(long id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid effect ID", nameof(id));

        return await _httpClient.DeleteAsync($"api/Hero/delete/HeroStatusEffect?id={id}");
    }

    public async Task<HttpResponseMessage> EditHeroEffectAsync(HeroStatusEffect effect)
    {
        if (effect == null)
            throw new ArgumentNullException(nameof(effect));

        string json = JsonSerializer.Serialize(effect);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync("api/Hero/edit/HeroStatusEffect", content);
        return response;
    }
}
