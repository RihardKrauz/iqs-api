﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Iqs.BL.Infrastructure
{
    public class MethodResult<T>
    {
        public bool IsOk { get; set; }
        public T Value { get; set; }
        public string ExceptionMessage { get; set; }
    }

    public static class MethodResultExtensions
    {
        public static MethodResult<T> ToSuccessMethodResult<T>(this T value)
        {
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

        public static MethodResult<T> ToErrorMethodResult<T>(this Exception ex) {
            return new MethodResult<T> { IsOk = false, ExceptionMessage = ex.ToString() };
        }

        public static MethodResult<TDestination> GetExceptionResult<TDestination, TSource>(this MethodResult<TSource> source) {
            return source.ExceptionMessage.ToErrorMethodResult<TDestination>();
        }

        public static MethodResult<TDestination> ConvertMethodResult<TDestination, TSource>(this MethodResult<TSource> source, TDestination value)
        {
            return new MethodResult<TDestination> { Value = value, IsOk = source.IsOk, ExceptionMessage = source.ExceptionMessage };
        }

    }
}
