using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Application.Interfaces
{
    public interface IRequest<TResponse> where TResponse : class
    {
    }
}
