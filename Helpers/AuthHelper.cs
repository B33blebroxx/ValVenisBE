// File: Helpers/AuthHelper.cs
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ValVenisBE.Helpers
{
    public static class AuthHelper
    {
        public static bool IsAdmin(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                return false; // No token provided
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null)
                    return false;

                // Extract the role claim
                var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
                return roleClaim?.Value == "admin"; // Return true if the user is an admin
            }
            catch
            {
                return false; // Invalid token
            }
        }
    }
}
