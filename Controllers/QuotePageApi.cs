using ValVenisBE.Models;

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
            app.MapPut("/quotepage", (ValVenisBEDbContext db, QuotePage updatedQuotePage) =>
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

                db.SaveChanges();
                return Results.Ok(quotepage);
            });
        }
    }
}
