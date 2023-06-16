using AweSomeURLShortener.Infrastructure.Entities;
using AweSomeURLShortener.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Infrastructure
{
    public static class DataSeeder
    {
        public static void Seed(DbContext dbContext, string filePath)
        {
            
            if (!dbContext.Set<UrlRegistryEntity>().Any())
            {
                string jsonData = File.ReadAllText(filePath);

                var urlDataList = JsonConvert.DeserializeObject<SeedModel<UrlRegistrySeedEntity>>(jsonData);

                var urlRegistryEntities = MapAndTakeTopHits(urlDataList.UrlList);

                dbContext.Set<UrlRegistryEntity>().AddRange(urlRegistryEntities);
                dbContext.SaveChanges();
            }
        }

        public static List<UrlRegistryEntity> MapAndTakeTopHits(IEnumerable<UrlRegistrySeedEntity> seedEntities)
        {
            // Attempt to parse the Id and map the rest of the properties.
            // If Id can't be parsed to int, ignore that entity.
            var urlRegistryEntities = seedEntities
                .Where(seedEntity => int.TryParse(seedEntity.Id, out _))
                .Select(seedEntity => new UrlRegistryEntity
                {
                    Id = int.Parse(seedEntity.Id),
                    Hits = seedEntity.Hits,
                    Url = seedEntity.Url,
                    ShortUrl = seedEntity.ShortUrl
                })
                .ToList();

            // Order by Hits descending and take the top 5.
            var topHits = urlRegistryEntities.OrderByDescending(entity => entity.Hits).Take(5).ToList();

            return topHits;
        }

    }

    public class SeedModel<T>
    {
        public List<T> UrlList;
    }

    public class UrlRegistrySeedEntity
    {
        public string Id { get; set; }
        public int Hits { get; set; }
        public string Url { get; set; }
        public string? ShortUrl { get; set; }
    }
}
