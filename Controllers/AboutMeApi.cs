using ValVenisBE.Models;

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

            //Update AboutMe
            app.MapPut("/aboutme/{id}", (ValVenisBEDbContext db, int id, AboutMe updatedAboutMe) =>
            {
                var aboutme = db.AboutMes.Find(id);
                if (aboutme == null)
                {
                    return Results.NotFound();
                }
                aboutme.UserId = updatedAboutMe.UserId;
                aboutme.AboutMeImage = updatedAboutMe.AboutMeImage;
                aboutme.AboutMeProfileLink = updatedAboutMe.AboutMeProfileLink;
                aboutme.AboutMeText = updatedAboutMe.AboutMeText;

                db.SaveChanges();
                return Results.Ok(aboutme);
            });
        }
    }
}
