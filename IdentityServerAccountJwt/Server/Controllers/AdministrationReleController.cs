using IdentityServerAccountJwt.Shared.Dtos.Administrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerAccountJwt.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdministrationReleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationReleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles;
            return Ok(roles);   
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AdministrationRole administrationRole)
        {
            if (administrationRole == null || !ModelState.IsValid)
                return NotFound("Cant Add Role");
            var role = new IdentityRole
            {
            
                Name = administrationRole.Name
            };
            var result=await _roleManager.CreateAsync(role);
            if (!result.Succeeded) {
                var errors = result.Errors.Select(e=>e.Description);
                return BadRequest(new AdminstrationRoleResponse { Errors=errors});
            }
            return StatusCode(201);
        }
        [HttpPut]
        public async Task<IActionResult> EditRole([FromBody] AdministrationRole administrationRole) {
            if (administrationRole == null || !ModelState.IsValid)
                return NotFound("Role Not Found");
            var CurrRole=await _roleManager.FindByIdAsync(administrationRole.Id);
            if (CurrRole==null)
                return NotFound("Role Not Found");
            CurrRole.Name = administrationRole.Name;
            var result=await _roleManager.UpdateAsync(CurrRole);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new AdminstrationRoleResponse { Errors=errors});  
            }
            return StatusCode(202);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string RoleId)
        {
        
            var CurrRole = await _roleManager.FindByIdAsync(RoleId);
            if (CurrRole == null)
                return NotFound("Role Not Found");
            var result = await _roleManager.DeleteAsync(CurrRole);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new AdminstrationRoleResponse { Errors = errors });
            }
            return StatusCode(202);

        }
    }
}
