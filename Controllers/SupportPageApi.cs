using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;

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
            app.MapPut("/supportpage", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, SupportPage updatedSupportPage) =>
            {
                var supportPage = db.SupportPages.FirstOrDefault();
                if (supportPage == null)
                {
                    return Results.NotFound();
                }
                supportPage.SupportPageHeader = updatedSupportPage.SupportPageHeader;
                supportPage.SupportPageIntro = updatedSupportPage.SupportPageIntro;

                await db.SaveChangesAsync();
                return Results.Ok(supportPage);
            });
        }
    }
}
