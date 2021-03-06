using CSharpFunctionalExtensions;

namespace Entities.Domain
{
    public class Ingredient:Entity<int>
    {
        public string? Name { get; set; }
        public List<ProductIngredient>? ProductsIngredients { get; set; }
    }
}
