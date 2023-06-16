using AweSomeURLShortener.Domain.Interfaces;

namespace AweSomeURLShortener.Domain.Models
{
    public class UrlRegistry : IDomainEntity
    {
        public int Id { get; set; }
        public int Hits { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }

    }
}
