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

            Response.Cookies.Append("tokenATK", result.Value.Item1, new CookieOptions
            {
                HttpOnly = true,                // защита от XSS — JS не увидит cookie
                Secure = true,                  // cookie передается только по HTTPS
                SameSite = SameSiteMode.Strict, // запрещает отправку cookie при переходах с других сайтов
                Expires = DateTimeOffset.UtcNow.AddMinutes(90)
            });

            return Ok("Вы успешно авторизовались как '" + result.Value.Item2 + "'!");
        }



        [HttpGet("organizations")]
        public async Task<IActionResult> GetOrganizations()
        {
            StructuredOrganizations[] orgs = (StructuredOrganizations[])Enum.GetValues(typeof(StructuredOrganizations));
            return Ok(orgs);
        }
    }
}
