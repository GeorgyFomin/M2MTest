using Entities.Domain;
using MediatR;
using Persistence.MsSql;

namespace UseCases.API.Products
{
    public class AddProduct
    {
        public class Command : IRequest<int>
        {
            public string? Name { get; set; }
            public decimal Price { get; set; }
            public double Weight { get; set; }
            public virtual ICollection<ProductIngredient>? ProductsIngredients { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly DataContext _context;

            public CommandHandler(DataContext context) => _context = context;
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                if (_context.Products == null)
                    return default;
                Product product = new() { Name = request.Name, Price = request.Price, Weight = request.Weight, ProductsIngredients = request.ProductsIngredients };
                await _context.Products.AddAsync(product, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return product.Id;
            }
        }
    }
}
