using Entities.Domain;

namespace UseCases.API.Dto
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<ProductIngredient>? ProductsIngredients { get; set; }
    }
}
