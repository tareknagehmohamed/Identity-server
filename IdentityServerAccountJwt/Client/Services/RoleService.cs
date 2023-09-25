using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Dtos.Administrations;
using IdentityServerAccountJwt.Shared.Models;
using System.Data;
using System.Net.Http.Json;
using System.Text.Json;

namespace IdentityServerAccountJwt.Client.Services
{
    public class RoleService : IRoleService
    {
        public HttpClient HttpClient { get; set; }
        private readonly JsonSerializerOptions _options;

        public RoleService( HttpClient httpClient)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            HttpClient = httpClient;
        }

        public async Task<AdminstrationRoleResponse> AddRole(AdministrationRole role)
        {
            var result = await HttpClient.PostAsJsonAsync<AdministrationRole>("api/AdministrationRele/AddRole", role);
            if (!result.IsSuccessStatusCode)
            {

                var message = await result.Content.ReadAsStringAsync();
                var msg = JsonSerializer.Deserialize<AdminstrationRoleResponse>(message, _options);
                return msg;
            }
            return new AdminstrationRoleResponse { IsSuccess = true };
        }

        public async Task<AdminstrationRoleResponse> DeleteAsync(string RoleId)
        {
            var result = await HttpClient.DeleteAsync($"api/AdministrationRele/DeleteRole?RoleId={RoleId}");
            if (!result.IsSuccessStatusCode)
            {

                var message = await result.Content.ReadAsStringAsync();
                var msg = JsonSerializer.Deserialize<AdminstrationRoleResponse>(message, _options);
                return msg;
            }
            return new AdminstrationRoleResponse { IsSuccess = true };
        }

        public async Task<AdminstrationRoleResponse> EditRole(AdministrationRole role)
        {
            var result = await HttpClient.PutAsJsonAsync<AdministrationRole>("api/AdministrationRele/EditRole", role);
            if (!result.IsSuccessStatusCode)
            {

                var message = await result.Content.ReadAsStringAsync();
                var msg = JsonSerializer.Deserialize<AdminstrationRoleResponse>(message, _options);
                return msg;
            }
            return new AdminstrationRoleResponse { IsSuccess = true };
        }

        public async Task<List<AdministrationRole>> GetRoles()
        {
            return await HttpClient.GetFromJsonAsync<List<AdministrationRole>>("api/AdministrationRele/GetRoles");
        }

        public async Task<AdministrationRole> GetRoleById(string id)
        {
            return await HttpClient.GetFromJsonAsync<AdministrationRole>($"api/AdministrationRele/GetRolesById/{id}");
        }
    }
}
