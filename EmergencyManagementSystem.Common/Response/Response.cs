using System;
using System.Collections.Generic;
using System.Text;

namespace EmergencyManagementSystem.Common.Response
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ExceptionError { get; set; }
        public string StackTrace { get; set; }
        public int GeneratedId { get; set; }

        public string GetAllMessages()
            => $"Message: {Message}. ExceptionError: {ExceptionError}. StackTrace: {StackTrace}";

        public static Response CreateSuccess(string message = "", int id = 0)
        {
            return new Response { Success = true, Message = message, GeneratedId = id };
        }

        public static Response CreateFailure(string message)
        {
            return new Response { Success = false, Message = message };
        }

        public static Response CreateFailureException(string message, Exception error)
        {
            return new Response
            {
                Success = false,
                Message = message,
                ExceptionError = error.Message,
                StackTrace = error.StackTrace
            };
        }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }

        public static Response<T> CreateSuccess(T data, string message = "")
        {
            return new Response<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static new Response<T> CreateFailureException(string message, Exception error)
        {
            return new Response<T>
            {
                Success = false,
                Message = message,
                ExceptionError = error.Message,
                StackTrace = error.StackTrace
            };
        }

        public static new Response<T> CreateFailure(string message)
        {
            return new Response<T> { Success = false, Message = message };
        }
    }
}
