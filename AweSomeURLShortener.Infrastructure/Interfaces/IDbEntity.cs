using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Infrastructure.Interfaces
{
    public interface IDbEntity
    {
        int Id { get; set; }
    }
}
