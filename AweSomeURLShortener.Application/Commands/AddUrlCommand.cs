using AutoMapper;
using AweSomeURLShortener.Application.DTO;
using AweSomeURLShortener.Application.Interfaces;
using AweSomeURLShortener.Application.Models;
using AweSomeURLShortener.Domain.Models;
using MassTransit;

namespace AweSomeURLShortener.Application.Commands
{
    public record AddUrlCommand(AddUrlDTO urlEntry) : IRequest<IResult>
    {
    }

    public class AddUrlCommandHandler : BaseMessageHandler<AddUrlCommand, IResult>
    {
        IBus _bus;
        IUrlRegistryRepository _repository;
        IMapper _mapper;

        public AddUrlCommandHandler(IBus bus, IUrlRegistryRepository repository, IMapper mapper)
        {
            _bus = bus;
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<IResult> HandleAsync(AddUrlCommand request)
        {
            var urlRegistry = await _repository.FindByUrlAsync(request.urlEntry.Url);
            
            if (urlRegistry != null)
                return Result<UrlRegistry>.Ok(urlRegistry);

            var newRegistry = _mapper.Map<UrlRegistry>(request.urlEntry);
            var result = await _repository.AddAsync(newRegistry);

            return Result<UrlRegistry>.Ok(result);
        }
    }
}
