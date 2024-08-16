using ValVenisBE.Models;

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
            app.MapPut("/logos/{id}", (ValVenisBEDbContext db, Logo updatedLogo) =>
            {
                var logo = db.Logos.FirstOrDefault();
                if (logo == null)
                {
                    return Results.NotFound();
                }
                logo.UserId = updatedLogo.UserId;
                logo.LogoImage = updatedLogo.LogoImage;

                db.SaveChanges();
                return Results.Ok(logo);
            });

        }
    }
}
