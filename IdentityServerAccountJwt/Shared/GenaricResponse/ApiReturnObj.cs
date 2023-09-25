using IdentityServerAccountJwt.Shared.Enums;

namespace IdentityServerAccountJwt.Shared.GenaricResponse
{
    public class ApiReturnObj<Tobj>
    {
        public Tobj ReturnValue {  get; set; }
        public ApiReturnStatus Status { get; set; }
        public int TotalPaginationCount = 0;
    }
}
