using AweSomeURLShortener.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSomeURLShortener.Application.Models
{
    public class Result : IResult
    {
        private List<IResultError> _errors = new List<IResultError>();

        
        public bool Succeded => !_errors.Any();

        public string Message { get; set; }

        public Dictionary<string, object> Metadata { get; internal set; } = new Dictionary<string, object>();


        public string MessageWithErrors => Message + Environment.NewLine + string.Join(",", _errors);

        public Exception Exception { get; set; }

        public IReadOnlyCollection<IResultError> Errors => _errors.AsReadOnly();


        public static IResult Ok()
        {
            return new Result();
        }

        public static IResult Error(string errorMessage)
        {
            Result result = new Result();
            result.AddError(errorMessage);
            return result;
        }

        public void AddError(string errorMessage)
        {
            _errors.Add(new ResultError(errorMessage));
        }

        public void AddErrors(IEnumerable<IResultError> errors)
        {
            _errors.AddRange(errors);
        }

        public void AddError(string errorMessage, string errorCode)
        {
            _errors.Add(new ResultError(errorMessage, errorCode));
        }

        public void AddMetadata(string key, object value)
        {
            if (Metadata == null)
            {
                Metadata = new Dictionary<string, object>();
            }

            Metadata.Add(key, value);
        }

        public T GetMetadata<T>(string key) where T : struct
        {
            return (T)Metadata[key];
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public Result(T? data)
        {
            Data = data;
        }

        public static IResult<T> Ok(T data)
        {
            return new Result<T>(data);
        }
    }

}
