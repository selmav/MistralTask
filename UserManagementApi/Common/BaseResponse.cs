using System.Net;

namespace Common
{
    public class BaseResponse<T> where T : class
    {
        public T Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public static BaseResponse<T> Ok(T result) => new BaseResponse<T>
        {
            Result = result,
            StatusCode = HttpStatusCode.OK
        };

        public static BaseResponse<T> Ok() => new BaseResponse<T>
        {
            Result = null,
            StatusCode = HttpStatusCode.OK
        };

        public static BaseResponse<T> BadRequest() => new BaseResponse<T>
        {
            Result = null,
            StatusCode = HttpStatusCode.BadRequest
        };

        public static BaseResponse<T> NotFound() => new BaseResponse<T>
        {
            Result = null,
            StatusCode = HttpStatusCode.NotFound
        };

        public static BaseResponse<T> InternalServerError() => new BaseResponse<T>
        {
            Result = null,
            StatusCode = HttpStatusCode.InternalServerError
        };
    }
}
