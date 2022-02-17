using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.MsSql;
using UseCases.API.Dto;
using UseCases.API.Exceptions;

namespace UseCases.API.Products
{
    public class GetProducts
    {
        public class Query : IRequest<IEnumerable<ProductDto>> { }
        public class QueryHandler : IRequestHandler<Query, IEnumerable<ProductDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public QueryHandler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IEnumerable<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                if (_context.Products == null)
                    throw new EntityNotFoundException("Products not found");
                var products = await _context.Products.Include(p => p.ProductsIngredients).ToListAsync(cancellationToken);
                if (products == null)
                    throw new EntityNotFoundException("Products not found");
                List<ProductDto> productDtos = new();
                _mapper.Map(products, productDtos);
                return productDtos;
            }
        }
    }
}
