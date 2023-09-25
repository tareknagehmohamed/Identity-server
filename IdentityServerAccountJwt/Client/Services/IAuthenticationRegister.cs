using IdentityServerAccountJwt.Client.ResponseResult;
using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Models;
using IdentityServerAccountJwt.Shared.Pagination_Models;

namespace IdentityServerAccountJwt.Client.Services
{
    public interface IAuthenticationRegister
    {
        Task<UserResponse> RegisterUser(UserRegister userRegister);
        Task<ResponseLogin> LoginUser(UserLogin userLogin);
        Task Logout();
        Task <List<ApplicationUser>> GetUsers();     
        Task<ApplicationUser> GetUserByName(string name);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApiReturnResult<List<ApplicationUser>>> GetUsersPagination(Pagination pagination);
    }
}
