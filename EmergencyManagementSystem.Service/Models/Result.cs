
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Models
{
    public class Result<T> : Result where T : class
    {
        public T Model { get; private set; }

        public Result()
        {
            Messages = new List<string>();
        }

        public static new Result<T> BuildError(string message)
            => new Result<T>
            {
                Success = false,
                Messages = new List<string> { message }
            };

        public static new Result<T> BuildError(List<string> messages)
            => new Result<T>
            {
                Success = false,
                Messages = messages
            };

        public static new Result<T> BuildError(string message, Exception error)
            => new Result<T>
            {
                Success = false,
                Messages = new List<string>
                {
                   $"Message: {message}",
                   $"BaseException : {error.GetBaseException().Message}",
                   $"StackTrace: {error.GetBaseException().StackTrace} "
                }
            };

        public static Result<T> BuildSucess(T model, string message = "")
            => new Result<T>
            {
                Model = model,
                Success = true,
                Messages = new List<string> { message }
            };
    }

    public class Result
    {
        public bool Success { get; protected set; }
        public List<string> Messages { get; protected set; }

        public static Result BuildError(string message)
            => new Result
            {
                Success = false,
                Messages = new List<string> { message }
            };

        public static Result BuildError(List<string> messages)
            => new Result
            {
                Success = false,
                Messages = messages
            };

        public static Result BuildError(string message, Exception error)
            => new Result
            {
                Success = false,
                Messages = new List<string>
                {
                   $"Message: {message}",
                   $"BaseException : {error.GetBaseException().Message}",
                   $"StackTrace: {error.GetBaseException().StackTrace} "
                }
            };

        public static Result BuildSucess(string message = "")
            => new Result
            {
                Success = true,
                Messages = new List<string> { message }
            };
    }
}
