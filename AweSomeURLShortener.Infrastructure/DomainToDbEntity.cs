using AutoMapper;
using AweSomeURLShortener.Application.DTO;
using AweSomeURLShortener.Domain.Models;
using AweSomeURLShortener.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Infrastructure
{
    public class DomainToDbEntity : Profile
    {
        public DomainToDbEntity()
        {
            CreateMap<UrlRegistry, UrlRegistryEntity>();
            CreateMap<UrlRegistryEntity, UrlRegistry>();
        }
    }
}
