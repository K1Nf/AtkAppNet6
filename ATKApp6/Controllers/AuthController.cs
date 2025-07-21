using ATKApp6.Contracts.Request;
using ATKApp6.Domain.Enums;
using ATKApp6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ATKApplication.Controllers
{
    [Route("api/ref/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }



        [HttpGet("/login")]
        public async Task GetLoginPage()
        {
            Response.Headers.ContentType = "text/html";
            await Response.SendFileAsync("wwwroot/src/Login/LoginPage.html");
        }



        [HttpPost("authorize")]
        public IActionResult Authorize([FromBody] AuthorizeRequest authorizeRequest)
        {
            var result = _authService.Authorize(authorizeRequest);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            Response.Cookies.Append("tokenATK", result.Value);
            return Ok("Вы успешно авторизовались как '" + authorizeRequest.OrganizationName + "'!");    
        }



        [HttpGet("organizations")]
        public async Task<IActionResult> GetOrganizations()
        {
            StructuredOrganizations[] orgs = (StructuredOrganizations[])Enum.GetValues(typeof(StructuredOrganizations));
            return Ok(orgs);
        }
    }
}
