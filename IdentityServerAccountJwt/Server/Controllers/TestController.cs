using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerAccountJwt.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string GetHello()
        {
            string result = "Hello World";
            return result;
        } 
        [HttpGet]
        [AllowAnonymous]
        public string GetWelcome()
        {
            string result = "Welcome To Egypt";
            return result;
        }
    }
}
