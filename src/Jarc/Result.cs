using System;

namespace Bcm.Jarc
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public Exception Error { get; protected set; }

        protected Result()
        {
            IsSuccess = true;
            Error = null;
        }

        public Result(Exception error)
        {
            IsSuccess = false;
            Error = error;
        }

        public static Result Empty => new Result();
        public static implicit operator Result(Exception error) => new Result(error);

        /// <summary>
        /// Checks if there is an error, throws if there is.
        /// </summary>
        public void EnsureSuccess()
        {
            if (!IsSuccess)
            {
                throw Error;
            }
        }

        public TValue Match<TValue>(Func<TValue> onSuccess, Func<Exception, TValue> onError)
        {
            return IsSuccess ? onSuccess() : onError(Error);
        }
    }

    public class Result<T> : Result
    {
        protected Result(T value) : base()
        {
            Value = value;
        }

        protected Result(Exception error) : base(error)
        {
            Value = default;
        }

        public T Value { get; }
        public static implicit operator Result<T>(T data) => new Result<T>(data);
        public static implicit operator Result<T>(Exception error) => new Result<T>(error);

        public TValue Match<TValue>(Func<T, TValue> onSuccess, Func<Exception, TValue> onError)
        {
            return IsSuccess ? onSuccess(Value) : onError(Error);
        }
    }
}


