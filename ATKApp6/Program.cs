using ATKApp6.Infrastructure.DataBase;
using ATKApp6.Infrastructure.Extensions;
using ATKApp6.Services;
using Newtonsoft.Json.Converters;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using ATKApp6.Infrastructure.Extensions.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHash, PasswordHasher>();

builder.Services.AddDbContext<DataBaseContext>();
builder.Services.AddHttpContextAccessor();


builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection(nameof(JWTConfiguration)));
builder.Services.AddScoped<IAuthorizationHandler, AuthorizeHandler>();

builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "RequestVerificationToken";    // заголовок, в который фронт кладёт request-токен
    options.Cookie.Name = "XSRF-TOKEN";                 // имя cookie с cookie-токеном
    options.Cookie.HttpOnly = true;                    // чтобы видеть в DevTools (но не используем в JS)
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {
        var jwt = builder.Configuration.GetSection(nameof(JWTConfiguration));

        var key = builder.Configuration.GetValue<string>("JWTConfiguration:SecretKey");
        var issuer = jwt.GetSection(nameof(JWTConfiguration.Issuer));
        var audience = jwt.GetSection(nameof(JWTConfiguration.Audience));

        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidIssuer = issuer.Value,

            ValidateAudience = true,
            ValidAudience = audience.Value,

            ValidateLifetime = true,
            RequireExpirationTime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key))
        };


        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                string? token = context.Request.Cookies["commissionToken"];

                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Token = token; //.Replace("Bearer ", "");
                }

                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanUserReadResource", policy =>
        policy.Requirements.Add(new PolicyNameRequirements("CanUserReadResource")));
});

builder.Services.AddControllers(o =>
{
    o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});


builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<OrganizationService>();



var app = builder.Build();

app.UseMiddleware<MyExceptionHandlerMiddleware>();

app.UseHsts();
app.UseHttpsRedirection();

app.UseRouting();

app.UseStatusCodePages(async context => {

    var request = context.HttpContext.Request;
    var response = context.HttpContext.Response;

    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        response.Redirect("/login");
    }

    if (response.StatusCode == (int)HttpStatusCode.NotFound)
    {
        response.ContentType = "text/html; charset=utf-8";

        string responseHTML = "<!DOCTYPE html>\r\n" +
                    "<html>\r\n" +
                    "<head>\r\n    " +
                        "<meta charset=\"utf-8\" />\r\n    " +
                        "<title>Страница не найдена!</title>\r\n" +
                    "</head>\r\n" +
                    "<body>\r\n    " +
                        "<div>\r\n       " +
                            "<h1 style=\"font-size:300%; color: black; text-align: center;\">Содержимое, которое вы ищете, не найдено</h1>\r\n       " +
                            "<h1 style=\"font-size:200%; color: black; text-align: center;\">Убедитесь, что ввели правильный адрес</h1>\r\n       " +
                            //$"<h1 style=\"font-size:150%; color: red; text-align: center;\">{responseText}</h1>\r\n    " +
                        "</div>\r\n\r\n\r\n    " +
                        "<script>\r\n\r\n        " +
                        "function openPreviousPage() {\r\n            " +
                        "window.location.href = window.history.go(-1);\r\n        }\r\n\r\n        " +
                        "setTimeout(openPreviousPage, 3000);\r\n\r\n\r\n    " +
                        "</script>\r\n" +
                    "</body>\r\n" +
                "</html>\r\n";

        await response.WriteAsync(responseHTML);
    }
    else
    {
        response.ContentType = "text/plain; charset=utf-8";
        await response.WriteAsync("Unexpected error with code: " + response.StatusCode);
    }
});


app.UseStaticFiles();
app.UseDefaultFiles();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapGet("/authorize/login", async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    context.Response.StatusCode = 401;
    await context.Response.SendFileAsync("wwwroot/html/LoginPage.html");
});

app.Run();