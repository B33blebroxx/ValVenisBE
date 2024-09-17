using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ValVenisBE.Controllers
{
    public class LogoApi
    {
        public static void Map(WebApplication app)
        {
            //Get Logo
            app.MapGet("/logos", async (ValVenisBEDbContext db) =>
            {
                var logos = await db.Logos.FirstOrDefaultAsync();
                return Results.Ok(logos);
            });

            //Update Logo
            app.MapPut("/logos/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, Logo updatedLogo) =>
            {
                var logo = await db.Logos.FirstOrDefaultAsync();
                if (logo == null)
                {
                    return Results.NotFound();
                }
                logo.UserId = updatedLogo.UserId;
                logo.LogoImage = updatedLogo.LogoImage;

                await db.SaveChangesAsync();
                return Results.Ok(logo);
            });

        }
    }
}
