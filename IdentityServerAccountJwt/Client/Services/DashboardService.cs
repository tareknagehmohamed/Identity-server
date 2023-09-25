using IdentityServerAccountJwt.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace IdentityServerAccountJwt.Client.Services
{
    public class DashboardService : IDashboardservice
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<DashboardData> GetDashboardData()
        {
            return await _httpClient.GetFromJsonAsync<DashboardData>("api/Dasboard/GetDashboardUsers");
        }
    }
}
