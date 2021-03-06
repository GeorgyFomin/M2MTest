using Entities.Domain;
using System.ComponentModel.DataAnnotations;

namespace UseCases.API.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public double Weight { get; set; }
        public virtual ICollection<ProductIngredient> ProductsIngredients { get; set; } = new List<ProductIngredient>();
    }
}
