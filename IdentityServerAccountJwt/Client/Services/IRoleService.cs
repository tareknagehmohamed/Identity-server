using IdentityServerAccountJwt.Shared.Dtos.Administrations;

namespace IdentityServerAccountJwt.Client.Services
{
    public interface IRoleService
    {
        Task<List<AdministrationRole>> GetRoles();
        Task<AdministrationRole> GetRoleById(string id);
        Task<AdminstrationRoleResponse> AddRole(AdministrationRole role);
        Task<AdminstrationRoleResponse> EditRole(AdministrationRole role);
        Task<AdminstrationRoleResponse> DeleteAsync(string RoleId);

    }
}
