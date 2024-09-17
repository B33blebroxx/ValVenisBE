using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using ValVenisBE.Dtos;
using ValVenisBE.Models;

namespace ValVenisBE.Controllers
{
    public class AuthApi
    {
        public static void Map(WebApplication app)
        {
            // Register User
            app.MapPost("/auth/register", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, User user) =>
            {
                if (await db.Users.AnyAsync(u => u.Username == user.Username))
                {
                    return Results.BadRequest("Username already exists.");
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                user.Role = "admin";

                db.Users.Add(user);
                await db.SaveChangesAsync();
                return Results.Ok("User registered successfully.");
            });

            // Check if user is logged in
            app.MapGet("/auth/check", [Authorize] (HttpContext context) =>
            {
                var username = context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = context.User.FindFirst("userID")?.Value;

                var user = new
                {
                    UserId = userId,
                    Username = username,
                    Role = role
                };

                return Results.Ok(new { isLoggedIn = true, user });
            });

            // User Login
            app.MapPost("/auth/login", async (ValVenisBEDbContext db, UserLoginDto login, IConfiguration config) =>
            {
                var user = await db.Users.SingleOrDefaultAsync(u => u.Username == login.Username);
                if (user == null || user.Role != "admin" || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                {
                    return Results.Unauthorized();
                }

                var token = GenerateJwtToken(user, config);
                return Results.Ok(new { user, token });
            });

            static string GenerateJwtToken(User user, IConfiguration config)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key")));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username ?? throw new ArgumentNullException("user.Username")),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, user.Username ?? throw new ArgumentNullException("user.Username")),
                        new Claim(ClaimTypes.Role, user.Role ?? throw new ArgumentNullException("user.Role")),
                        new Claim("userID", user.Id.ToString())
                    };

                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}
