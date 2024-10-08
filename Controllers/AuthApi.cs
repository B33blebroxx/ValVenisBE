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
            app.MapPost("/auth/register", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, UserRegistrationDto userDto) =>
            {
                // Check if email already exists
                if (await db.Users.AnyAsync(u => u.Email.ToLower() == userDto.Email.ToLower()))
                {
                    return Results.BadRequest("Email already exists.");
                }

                // Check if username already exists
                if (await db.Users.AnyAsync(u => u.Username.ToLower() == userDto.Username.ToLower()))
                {
                    return Results.BadRequest("Username already exists.");
                }

                // Hash the password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

                // Create a new user
                var user = new User
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    PasswordHash = hashedPassword,
                    Role = "admin"
                };

                // Add user to the database
                db.Users.Add(user);
                await db.SaveChangesAsync();

                return Results.Ok("User registered successfully.");
            });


            //Get all users
            app.MapGet("/auth/users", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db) =>
            {
                var users = await db.Users.ToListAsync();

                return Results.Ok(users);
            });

            //Update User
            app.MapPut("/auth/users/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id, UserUpdateDto updateUserDto) =>
            {
                var user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound("User not found.");
                }

                // Update fields
                user.Username = updateUserDto.Username ?? user.Username;
                user.Email = updateUserDto.Email ?? user.Email;

                if (!string.IsNullOrEmpty(updateUserDto.Password))
                {
                    // Hash the new password
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
                }

                await db.SaveChangesAsync();

                return Results.Ok("User updated successfully.");
            });

            //Delete user
            app.MapDelete("/auth/users/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id) =>
            {
                var user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }

                db.Users.Remove(user);
                await db.SaveChangesAsync();

                return Results.Ok();
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
                    UserId = userId,
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
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddHours(1),
                    Path = "/"
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


            // User Logout
            app.MapPost("/auth/logout", (HttpContext context) =>
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddHours(-1) // Set expiration in the past to delete the cookie
                };

                context.Response.Cookies.Delete("AuthToken", cookieOptions);
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
