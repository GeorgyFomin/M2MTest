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
    public class DeleteIngredient
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }
        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext _context;
            public CommandHandler(DataContext context) => _context = context;
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                if (_context.Ingredients == null)
                    return Unit.Value;
                Ingredient? ingredient = await _context.Ingredients.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
                if (ingredient == null) return Unit.Value;
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}
