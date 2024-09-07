using ValVenisBE.Models;
using ValVenisBE.Helpers;

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
            app.MapPut("/supportpage", (ValVenisBEDbContext db, SupportPage updatedSupportPage, HttpContext context) =>
            {
                if (!AuthHelper.IsAdmin(context))
                {
                    return Results.Forbid();
                }

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
