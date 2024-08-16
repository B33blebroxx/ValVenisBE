using ValVenisBE.Models;

namespace ValVenisBE.Controllers
{
    public class SupportPageApi
    {
        public static void Map(WebApplication app)
        {
            //Get Support Page
            app.MapGet("/supportpage", (ValVenisBEDbContext db) =>
            {
                var supportPage = db.SupportPages.FirstOrDefault();
                return Results.Ok(supportPage);
            });

            //Update Support Page
            app.MapPut("/supportpage", (ValVenisBEDbContext db, SupportPage updatedSupportPage) =>
            {
                var supportPage = db.SupportPages.FirstOrDefault();
                if (supportPage == null)
                {
                    return Results.NotFound();
                }
                supportPage.SupportPageHeader = updatedSupportPage.SupportPageHeader;
                supportPage.SupportPageIntro = updatedSupportPage.SupportPageIntro;

                db.SaveChanges();
                return Results.Ok(supportPage);
            });
        }
    }
}
