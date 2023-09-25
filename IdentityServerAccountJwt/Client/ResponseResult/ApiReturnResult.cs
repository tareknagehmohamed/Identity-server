using IdentityServerAccountJwt.Shared.Enums;

namespace IdentityServerAccountJwt.Client.ResponseResult
{
    public class ApiReturnResult<T>
    {
        public T Result { get; set; }
        public ApiReturnStatus Status { get; set; }
        public int TotalPaginationCount = 0;
    }
}
