using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ValVenisBE.Models;

namespace ValVenisBE.Controllers
{
    public class SupportPageApi
    {
        public static void Map(WebApplication app)
        {
            //Get Support Page
            app.MapGet("/supportpage", async (ValVenisBEDbContext db) =>
            {
                var supportPage = await db.SupportPages.FirstOrDefaultAsync();
                return Results.Ok(supportPage);
            });

            //Update Support Page
            app.MapPut("/supportpage", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, SupportPage updatedSupportPage) =>
            {
                var supportPage = await db.SupportPages.FirstOrDefaultAsync();
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
