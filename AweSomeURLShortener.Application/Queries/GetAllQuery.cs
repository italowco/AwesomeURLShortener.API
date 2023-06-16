using AutoMapper;
using AweSomeURLShortener.Application.Interfaces;
using AweSomeURLShortener.Application.Models;
using AweSomeURLShortener.Domain.Models;
using MassTransit;

namespace AweSomeURLShortener.Application.Queries
{
    public record GetAllQuery(string queryParams) : IRequest<IResult>
    {
    }

    public class GetAllQueryHandler : BaseMessageHandler<GetAllQuery, IResult>
    {
        IBus _bus;
        IUrlRegistryRepository _repository;
        IMapper _mapper;

        public GetAllQueryHandler(IBus bus, IUrlRegistryRepository repository, IMapper mapper)
        {
            _bus = bus;
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<IResult> HandleAsync(GetAllQuery request)
        {
            var urlRegistryList = await _repository.GetAllAsync();
            return Result<IEnumerable<UrlRegistry>>.Ok(urlRegistryList);
        }


    }
}
