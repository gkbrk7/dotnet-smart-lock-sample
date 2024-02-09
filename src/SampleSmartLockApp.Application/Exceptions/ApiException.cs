using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Application.Exceptions
{
    public class ApiException : Exception
    {
        public List<string> Errors { get; set; } = [];
        public ApiException() : base("One or more validation failures have occured.")
        {
        }
        public ApiException(string message) : base(message)
        {
        }
        public ApiException(IEnumerable<string> errors) : this()
        {
            Errors.AddRange(errors);
        }
        public ApiException(string message, params string[] args) : base(message)
        {
            Errors.AddRange(args);
        }

    }
}