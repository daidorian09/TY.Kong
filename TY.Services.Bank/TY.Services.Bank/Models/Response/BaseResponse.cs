using System.Net;

namespace TY.Services.Bank.Models.Response
{
    public class BaseResponse<T>
    {
        public HttpStatusCode StatusCode{ get; set; }

        public bool IsSuccess { get; set; }

        public string Errors { get; set; }

        public T Result { get; set; }
    }
}