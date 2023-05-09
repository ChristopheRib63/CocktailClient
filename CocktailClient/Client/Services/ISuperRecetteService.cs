namespace CocktailClient.Client.Services.SuperRecetteService
{
    public interface ISuperRecetteService
    {
        List<SuperRecette> Recettes { get; set; }
        List<Alcool> Alcools { get; set; }
        Task GetAlcools();
        Task GetSuperRecettes();
        Task<SuperRecette> GetSingleRecette(int id);
        Task CreateRecette(SuperRecette recette);
        Task UpdateRecette(SuperRecette recette);
        Task DeleteRecette(int id);
    }
}
