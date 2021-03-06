using Entities.Domain;
using MediatR;
using Persistence.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.API.Ingredients
{
    public class EditIngredient
    {
        public class Command : IRequest<int>
        {
            public int Id { get; set; }
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
                Ingredient? ingredient = await _context.Ingredients.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
                if (ingredient == null)
                    return default;
                ingredient.Name = request.Name;
                ingredient.ProductsIngredients = request.ProductsIngredients;
                await _context.SaveChangesAsync(cancellationToken);
                return ingredient.Id;
            }
        }
    }
}
