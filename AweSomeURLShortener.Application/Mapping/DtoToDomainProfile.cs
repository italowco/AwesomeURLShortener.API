using AutoMapper;
using AweSomeURLShortener.Application.DTO;
using AweSomeURLShortener.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Application.Mapping
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<AddUrlDTO, UrlRegistry>()
                 .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                 .ForMember(dest => dest.ShortUrl, opt => opt.Ignore())
                 .ForMember(dest => dest.Hits, opt => opt.Ignore())
                 .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
