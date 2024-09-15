using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;

namespace ValVenisBE.Controllers
{
    public class LogoApi
    {
        public static void Map(WebApplication app)
        {
            //Get Logo
            app.MapGet("/logos", (ValVenisBEDbContext db) =>
            {
                var logos = db.Logos.FirstOrDefault();
                return Results.Ok(logos);
            });

            //Update Logo
            app.MapPut("/logos/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, Logo updatedLogo) =>
            {
                var logo = db.Logos.FirstOrDefault();
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
