using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using ValVenisBE.Dtos;
using ValVenisBE.Models;
using ValVenisBE.Helpers;

namespace ValVenisBE.Controllers
{
    public class AuthApi
    {
        public static void Map(WebApplication app)
        {
            //Register User
            app.MapPost("/auth/register", async (ValVenisBEDbContext db, User user, HttpContext context) =>
            {
                if (!AuthHelper.IsAdmin(context))
                {
                    return Results.Forbid();
                }

                if (db.Users.Any(u => u.Username == user.Username))
                {
                    return Results.BadRequest("Username already exists.");
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                user.Role = "admin";

                db.Users.Add(user);
                await db.SaveChangesAsync();
                return Results.Ok("User registered successfully.");
            });

            //User Login
            app.MapPost("/auth/login", (ValVenisBEDbContext db, UserLoginDto login, IConfiguration config) =>
            {
                var user = db.Users.SingleOrDefault(u => u.Username == login.Username);
                if (user == null || user.Role != "admin" || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash)) // Verify hashed password
                {
                    return Results.Unauthorized();
                }

                var token = GenerateJwtToken(user, config);
                return Results.Ok(new { user, token });
            });


            //Token Generation
            static string GenerateJwtToken(User user, IConfiguration config)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
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