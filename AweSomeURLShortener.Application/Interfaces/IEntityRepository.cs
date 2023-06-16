using AweSomeURLShortener.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Application.Interfaces
{
    public interface IEntityRepository<TDomain> : ICommandRepository<TDomain>, IQueryRepository<TDomain> where TDomain : class, IDomainEntity 
    {
    }
}
