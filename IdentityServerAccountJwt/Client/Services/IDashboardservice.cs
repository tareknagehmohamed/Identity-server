using IdentityServerAccountJwt.Shared.Dtos;

namespace IdentityServerAccountJwt.Client.Services
{
    public interface IDashboardservice
    {
        Task<DashboardData> GetDashboardData();

    }
}
