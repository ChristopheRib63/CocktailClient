global using CocktailClient.Client.Pages;
global using Microsoft.AspNetCore.Components;
global using System.Net.Http.Json;

namespace CocktailClient.Client.Services.SuperRecetteService
{
    public class SuperRecetteService : ISuperRecetteService
    {
        private readonly HttpClient _http;

        private readonly NavigationManager _navigationManager;

        public SuperRecetteService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<SuperRecette> Recettes { get; set; } = new List<SuperRecette>();
        public List<Alcool> Alcools { get; set; } = new List<Alcool>();

        public async Task CreateRecette(SuperRecette recette)
        {
            var result = await _http.PostAsJsonAsync("api/superrecette", recette);
            await SetRecettes(result);
        }

        private async Task SetRecettes(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<SuperRecette>>();
            Recettes = response;
            _navigationManager.NavigateTo("superrecettes");
        }

        public async Task DeleteRecette(int id)
        {
            var result = await _http.DeleteAsync($"api/superrecette/{id}");
            await SetRecettes(result);
        }

        public async Task GetAlcools()
        {
            var result = await _http.GetFromJsonAsync<List<Alcool>>("api/superrecette/alcools");
            if (result != null)
                Alcools = result;
        }

        public async Task<SuperRecette> GetSingleRecette(int id)
        {
            var result = await _http.GetFromJsonAsync<SuperRecette>($"api/superrecette/{id}");
            if (result != null)
                return result;
            throw new Exception("Recette not found!");
        }

        public async Task GetSuperRecettes()
        {
            var result = await _http.GetFromJsonAsync<List<SuperRecette>>("api/superrecette");
            if (result != null)
                Recettes = result;
        }

        public async Task UpdateRecette(SuperRecette recette)
        {
            var result = await _http.PutAsJsonAsync($"api/superrecette/{recette.Id}", recette);
            await SetRecettes(result);
        }
    }
}
