using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.MsSql;
using UseCases.API.Dto;
using UseCases.API.Exceptions;

namespace UseCases.API.Ingredients
{
    public class GetIngredients
    {
        public class Query : IRequest<IEnumerable<IngredientDto>> { }
        public class QueryHandler : IRequestHandler<Query, IEnumerable<IngredientDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<IngredientDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (_context.Ingredients == null)
                {
                    throw new EntityNotFoundException("Ingredients not found");
                }
                var ingredients = await _context.Ingredients.Include(i => i.ProductsIngredients).ToListAsync(cancellationToken);
                if (ingredients == null)
                {
                    throw new EntityNotFoundException("Ingredients not found");
                }
                List<IngredientDto> ingredientDtos = new();
                _mapper.Map(ingredients, ingredientDtos);
                return ingredientDtos;
            }
        }
    }
}
