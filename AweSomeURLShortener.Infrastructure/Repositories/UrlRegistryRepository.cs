using AutoMapper;
using AweSomeURLShortener.Application.Interfaces;
using AweSomeURLShortener.Domain.Models;
using AweSomeURLShortener.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AweSomeURLShortener.Infrastructure.Repositories
{
    public class UrlRegistryRepository : BaseRepository<UrlRegistryEntity, UrlRegistry, AwesomeURLShortenerDbContext>, IUrlRegistryRepository
    {
        IMapper _mapper;

        public UrlRegistryRepository(AwesomeURLShortenerDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<UrlRegistry> FindByUrlAsync(string name)
        {
            var result = await _entities.FirstOrDefaultAsync(ur => ur.ShortUrl.Equals(name));
            return _mapper.Map<UrlRegistry>(result);
        }
    }
}
