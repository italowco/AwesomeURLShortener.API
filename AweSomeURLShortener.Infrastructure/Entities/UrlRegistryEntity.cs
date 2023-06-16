using AweSomeURLShortener.Infrastructure.Interfaces;

namespace AweSomeURLShortener.Infrastructure.Entities
{
    public class UrlRegistryEntity : IDbEntity
    {
        public int Id { get; set; }
        public int Hits { get; set; }
        public string Url { get; set; }
        public string? ShortUrl { get; set; }   
    }
}
