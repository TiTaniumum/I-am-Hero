﻿using System;
using System.Collections.Generic;
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

        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get");
        return response.IsSuccessStatusCode;
    }

    public void Logout()
    {
        TokenStorage.DeleteToken();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    //Hero Skills
    public async Task<HttpResponseMessage> GetHeroSkillsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get/HeroSkills");
        return response;
    }
    public async Task<HttpResponseMessage> CreateSkillAsync(HeroSkill skill)
    {
        if (skill == null)
            throw new ArgumentNullException(nameof(skill));

        string json = JsonSerializer.Serialize(skill);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Hero/create/HeroSkills", content);
        return response;
    }

    //Hero Quests
    public async Task<HttpResponseMessage> GetQuestsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("api/Hero/get/Quests");
        return response;
    }
    public async Task<HttpResponseMessage> CreateQuestAsync(Quest quest)
    {
        if (quest == null)
            throw new ArgumentNullException(nameof(quest));

        string json = JsonSerializer.Serialize(quest);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("api/Hero/create/Quest", content);
        return response;
    }

}
