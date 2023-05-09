using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailClient.Shared
{
    public class SuperRecette
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Ingredient { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Alcool? Alcool { get; set; }
        public int AlcoolId { get; set; }
    }
}
