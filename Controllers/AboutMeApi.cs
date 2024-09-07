using ValVenisBE.Models;
using ValVenisBE.Helpers;

namespace ValVenisBE.Controllers
{
    public class AboutMeApi
    {
        public static void Map(WebApplication app)
        {
            //Get AboutMe
            app.MapGet("/aboutme", (ValVenisBEDbContext db) =>
            {
                var aboutme = db.AboutMes.FirstOrDefault();
                return Results.Ok(aboutme);
            });

            // Update AboutMe
            app.MapPut("/aboutme", (ValVenisBEDbContext db, AboutMe updatedAboutMe, HttpContext context) =>
            {
                if (!AuthHelper.IsAdmin(context))
                {
                    return Results.Forbid();
                }

                var aboutme = db.AboutMes.FirstOrDefault();
                if (aboutme == null)
                {
                    return Results.NotFound();
                }

                if (updatedAboutMe == null)
                {
                    return Results.BadRequest("Invalid AboutMe data");
                }

                aboutme.UserId = updatedAboutMe.UserId;
                aboutme.AboutMeHeader = updatedAboutMe.AboutMeHeader;
                aboutme.AboutMeImage = updatedAboutMe.AboutMeImage;
                aboutme.AboutMeProfileLink = updatedAboutMe.AboutMeProfileLink;
                aboutme.AboutMeText = updatedAboutMe.AboutMeText;

                db.SaveChanges();
                return Results.Ok(aboutme);
            });
        }
    }
}
