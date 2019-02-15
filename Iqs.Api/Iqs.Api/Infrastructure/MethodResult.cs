using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iqs.Api.Infrastructure
{
    public class MethodResult<T>
    {
        public bool IsOk { get; set; }
        public T Value { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public static class MethodResultExtensions {
        public static MethodResult<T> ToSuccessMethodResult<T>(this T value) {
            return new MethodResult<T> { IsOk = true, Value = value };
        }

        public static MethodResult<T> ToErrorMethodResult<T>(this T value)
        {
            return new MethodResult<T> { IsOk = false, Value = value };
        }

        public static MethodResult<T> ToErrorMethodResult<T>(this string errorMsg)
        {
            return new MethodResult<T> { IsOk = false, ExceptionMessage = errorMsg };
        }
    }
}
