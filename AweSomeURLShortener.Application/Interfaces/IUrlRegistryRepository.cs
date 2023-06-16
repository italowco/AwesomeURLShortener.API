using AweSomeURLShortener.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Application.Interfaces
{
    public interface IUrlRegistryRepository : IEntityRepository<UrlRegistry>
    {
        Task<UrlRegistry> FindByUrlAsync(string url);
    }
}
