using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.Common
{
    public class ServiceResponse<T>
    {
        protected ServiceResponse() { }

        public ServiceResponse(T result, bool success)
        {
            Result = result;
            Success = success;
        }

        public static ServiceResponse<T> Successful(T result)
        {
            return new ServiceResponse<T>(result, true);
        }

        public static ServiceResponse<ServiceError> Error(string title, int status)
        {
            var error = new ServiceError { Title = title, Status = status };
            return new ServiceResponse<ServiceError>(error, false);
        }

        public T Result { get; set; }

        public bool Success { get; set; }
    }

    public class ServiceResponse : ServiceResponse<object>
    {
    }
}

