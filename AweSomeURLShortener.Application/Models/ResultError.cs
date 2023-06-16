using AweSomeURLShortener.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Application.Models
{
    public class ResultError : IResultError
    {

        public string Error { get; set; }

        public string Code { get; set; }

        public ResultError(string error)
        {
            Error = error;
        }

        public ResultError(string error, string code)
        {
            Code = code;
        }

        public override string ToString()
        {
            return $"Error[{Code}]: {Error}";
        }

    }
}
