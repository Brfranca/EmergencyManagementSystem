using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Common.Response
{
    public class QueryResponse<T> : Response
    {
        public T Data { get; set; }

        public static QueryResponse<T> CreateSuccess(T data, string message = "")
        {
            return new QueryResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static new QueryResponse<T> CreateFailureException(string message, Exception error)
        {
            return new QueryResponse<T>
            {
                Success = false,
                Message = message,
                ExceptionError = error.Message,
                StackTrace = error.StackTrace
            };
        }

        public static new QueryResponse<T> CreateFailure(string message)
        {
            return new QueryResponse<T> { Success = false, Message = message };
        }
    }
}

