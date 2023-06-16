using AutoMapper;
using AweSomeURLShortener.Application.Interfaces;
using AweSomeURLShortener.Application.Models;
using AweSomeURLShortener.Domain.Models;
using AweSomeURLShortener.Domain.Utils;
using MassTransit;

namespace AweSomeURLShortener.Application.Queries
{
    public record GetUrlQuerie(string shortUrl) : IRequest<IResult>
    {
    }

    public class GetUrlQuerieHandler : BaseMessageHandler<GetUrlQuerie, IResult>
    {
        IBus _bus;
        IUrlRegistryRepository _repository;
        IMapper _mapper;

        public GetUrlQuerieHandler(IBus bus, IUrlRegistryRepository repository, IMapper mapper)
        {
            _bus = bus;
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<IResult> HandleAsync(GetUrlQuerie request)
        {
            var urlRegistry = await _repository.FindByUrlAsync(request.shortUrl);

            if (urlRegistry == null)
            {
                //var result = new Result();
                return Result.Error("Not exists");
            }

            urlRegistry.Hits = ++urlRegistry.Hits;
            var result = await _repository.UpdateAsync(urlRegistry);

            return Result<UrlRegistry>.Ok(urlRegistry);
        }
    }
}
