using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ValVenisBE.Controllers
{
    public class QuotePageApi
    {
        public static void Map(WebApplication app)
        {
            //Get QuotePage
            app.MapGet("/quotepage", async (ValVenisBEDbContext db) =>
            {
                var quotepage = await db.QuotePages.FirstOrDefaultAsync();
                return Results.Ok(quotepage);
            });

            // Update QuotePage
            app.MapPut("/quotepage", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, QuotePage updatedQuotePage) =>
            {
                var quotepage = await db.QuotePages.FirstOrDefaultAsync();
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
