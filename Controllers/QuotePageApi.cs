using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;

namespace ValVenisBE.Controllers
{
    public class QuotePageApi
    {
        public static void Map(WebApplication app)
        {
            //Get QuotePage
            app.MapGet("/quotepage", (ValVenisBEDbContext db) =>
            {
                var quotepage = db.QuotePages.FirstOrDefault();
                return Results.Ok(quotepage);
            });

            // Update QuotePage
            app.MapPut("/quotepage", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, QuotePage updatedQuotePage) =>
            {
                var quotepage = db.QuotePages.FirstOrDefault();
                if (quotepage == null)
                {
                    return Results.NotFound();
                }

                if (updatedQuotePage == null)
                {
                    return Results.BadRequest("Invalid QuotePage data");
                }

                quotepage.UserId = updatedQuotePage.UserId;
                quotepage.QuotePageHeader = updatedQuotePage.QuotePageHeader;
                quotepage.QuotePageIntro = updatedQuotePage.QuotePageIntro;

                await db.SaveChangesAsync();
                return Results.Ok(quotepage);
            });
        }
    }
}
