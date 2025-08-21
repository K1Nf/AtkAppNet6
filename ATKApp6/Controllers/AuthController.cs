using ATKApp6.Contracts.Request;
using ATKApp6.Domain.Enums;
using ATKApp6.Services;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ATKApp6.Controllers
{
    [Route("api/ref/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IAntiforgery _antiforgery;

        public AuthController(AuthService authService, IAntiforgery antiforgery)
        {
            _authService = authService;
            _antiforgery = antiforgery;
        }



        [HttpGet("/login")]
        public async Task GetLoginPage()
        {
            Response.Headers.ContentType = "text/html";
            await Response.SendFileAsync("wwwroot/src/Login/LoginPage.html");
        }



        [HttpPost("authorize")]
        [ValidateAntiForgeryToken]
        public IActionResult Authorize([FromBody] AuthorizeRequest authorizeRequest)
        {
            // метод генерации JWT токена и соответствующего для пользователя сообщения
            var result = _authService.Authorize(authorizeRequest); 

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            // Отправка Cookie авторизованному пользователю
            Response.Cookies.Append("commissionToken", result.Value.Item1, new CookieOptions  
            {
                HttpOnly = true,                // защита от XSS — JS не увидит cookie
                Secure = true,                  // cookie передается только по HTTPS
                SameSite = SameSiteMode.Strict, // запрещает отправку cookie при переходах с других сайтов
                Expires = DateTimeOffset.UtcNow.AddMinutes(90) // Время жизни токена
            });

            return Ok($"Вы успешно авторизовались как «{result.Value.Item2}»!");
        }



        [HttpGet("organizations")]
        public IActionResult GetOrganizations()
        {
            StructuredOrganizations[] orgs = (StructuredOrganizations[])Enum.GetValues(typeof(StructuredOrganizations));
            return Ok(orgs);
        }


        [HttpGet("csrf-token")]
        public IActionResult GetCsrfToken()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            return Ok(new { csrf = tokens.RequestToken });
        }
    }
}
