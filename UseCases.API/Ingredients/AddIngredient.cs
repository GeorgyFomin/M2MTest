using MediatR;
using Persistence.MsSql;
using Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.API.Ingredients
{
    public class AddIngredient
    {
        public class Command : IRequest<int>
        {
            public string? Name { get; set; }
            public List<ProductIngredient>? ProductsIngredients { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly DataContext _context;

            public CommandHandler(DataContext context) => _context = context;
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                if (_context.Ingredients == null)
                    return default;
                Ingredient Ingredient = new() { Name = request.Name, ProductsIngredients = request.ProductsIngredients };
                await _context.Ingredients.AddAsync(Ingredient, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Ingredient.Id;
            }
        }
    }
}
