using Microsoft.AspNetCore.Authorization;
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
                // Check if email already exists
                if (await db.Users.AnyAsync(u => u.Email.ToLower() == user.Email.ToLower()))
                {
                    return Results.BadRequest("Email already exists.");
                }

                // Check if username already exists
                if (await db.Users.AnyAsync(u => u.Username.ToLower() == user.Username.ToLower()))
                {
                    return Results.BadRequest("Username already exists.");
                }

                // Hash the password
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                // Assign role (this example assigns "admin" role; modify as necessary)
                user.Role = "admin";

                // Add user to the database
                db.Users.Add(user);
                await db.SaveChangesAsync();

                return Results.Ok("User registered successfully.");
            });

            // Check if user is logged in
            app.MapGet("/auth/check", [Authorize] (HttpContext context) =>
            {
                var userId = context.User.FindFirst("userID")?.Value;
                var username = context.User.FindFirst(ClaimTypes.Name)?.Value;
                var email = context.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
                var role = context.User.FindFirst(ClaimTypes.Role)?.Value;

                var user = new
                {
                    Id = userId,
                    Username = username,
                    Email = email,
                    Role = role
                };

                return Results.Ok(new { isLoggedIn = true, user });
            });


            // User Login
            app.MapPost("/auth/login", async (HttpContext context, ValVenisBEDbContext db, UserLoginDto login, IConfiguration config) =>
            {
                var user = await db.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == login.Email.ToLower());
                if (user == null || user.Role != "admin" || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                {
                    return Results.Unauthorized();
                }

                var token = GenerateJwtToken(user, config);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddHours(1)
                };

                context.Response.Cookies.Append("AuthToken", token, cookieOptions);
                return Results.Ok(new
                {
                    isLoggedIn = true,
                    user = new
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        Username = user.Username,
                        Role = user.Role
                    }
                });
            });

            //User Logout
            app.MapPost("/auth/logout", (HttpContext context) =>
            {
                context.Response.Cookies.Delete("AuthToken");
                return Results.Ok(new { isLoggedIn = false });
            });

            static string GenerateJwtToken(User user, IConfiguration config)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key")));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username ?? throw new ArgumentNullException("user.Username")),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? throw new ArgumentNullException("user.Email")),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, user.Role ?? throw new ArgumentNullException("user.Role")),
                    new Claim("userID", user.Id.ToString()) // Keep userId as a custom claim
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
