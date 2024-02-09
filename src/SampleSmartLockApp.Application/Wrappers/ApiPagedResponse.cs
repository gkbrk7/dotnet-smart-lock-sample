using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSmartLockApp.Application.Wrappers
{
    public class ApiPagedResponse<T>(int pageNumber = 1, int pageSize = 10) : ApiResponse<T>
    {
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;

        public static ApiPagedResponse<T> PagedFail(string errorMessage, int pageNumber, int pageSize)
        {
            return new ApiPagedResponse<T> { Succeeded = false, Message = errorMessage, PageNumber = pageNumber, PageSize = pageSize };
        }
        public static ApiPagedResponse<T> PagedSuccess(T data, int pageNumber, int pageSize, string? message = null)
        {
            return new ApiPagedResponse<T> { Succeeded = true, Message = message, Data = data, PageNumber = pageNumber, PageSize = pageSize };
        }
    }
}