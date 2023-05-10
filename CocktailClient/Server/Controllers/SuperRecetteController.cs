using CocktailClient.Server.Data;
using CocktailClient.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CocktailClient.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperRecetteController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperRecetteController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperRecette>>> GetSuperRecettes()
        {
            var recettes = await _context.SuperRecettes.Include(sr => sr.Alcool).ToListAsync();
            return Ok(recettes);
        }

        [HttpGet("alcools")]
        public async Task<ActionResult<List<Alcool>>> GetAlcools()
        {
            var alcools = await _context.Alcools.ToListAsync();
            return Ok(alcools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperRecette>> GetSingleRecette(int id)
        {
            var recette = await _context.SuperRecettes
                .Include(r => r.Alcool)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (recette == null)
            {
                return NotFound("Sorry, no recette here. :/");
            }
            return Ok(recette);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperRecette>>> CreateSuperRecette(SuperRecette recette)
        {
            recette.Alcool = null;
            _context.SuperRecettes.Add(recette);
            await _context.SaveChangesAsync();

            return Ok(await GetDbRecettes());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperRecette>>> UpdateSuperRecette(SuperRecette recette, int id)
        {
            var dbRecette = await _context.SuperRecettes
                .Include(sr => sr.Alcool)
                .FirstOrDefaultAsync(sr => sr.Id == id);
            if (dbRecette == null)
                return NotFound("Sorry, but no recette for you. :/");

            dbRecette.Name = recette.Name;
            dbRecette.Ingredient = recette.Ingredient;
            dbRecette.Description = recette.Description;
            dbRecette.AlcoolId = recette.AlcoolId;

            await _context.SaveChangesAsync();

            return Ok(await GetDbRecettes());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperRecette>>> DeleteSuperRecette(int id)
        {
            var dbRecette = await _context.SuperRecettes
                .Include(sr => sr.Alcool)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (dbRecette == null)
                return NotFound("Sorry, but no recette for you. :/");

            _context.SuperRecettes.Remove(dbRecette);
            await _context.SaveChangesAsync();

            return Ok(await GetDbRecettes());
        }

        private async Task<List<SuperRecette>> GetDbRecettes()
        {
            return await _context.SuperRecettes.Include(sr => sr.Alcool).ToListAsync();
        }
    }
}
