using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Core.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors?.ToArray() ?? Array.Empty<string>();
        }

        public static Result Success
            => new Result(true, null);

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Failure(params string[] errors)
            => new Result(false, errors);
    }

    public class Result<T> : Result
        where T : class
    {
        internal Result(T data)
            : base(true, null)
            => this.Data = data;

        internal Result(bool succeeded, IEnumerable<string> errors)
            : base(succeeded, errors)
        {
        }

        internal Result(Result failedResult)
            : base(false, failedResult.Errors)
        {
        }

        public T Data { get; set; }

        public new static Result<T> Success(T data)
            => new Result<T>(data);

        public new static Result<T> Failure(params string[] errors)
            => new Result<T>(Result.Failure(errors));
    }
}
