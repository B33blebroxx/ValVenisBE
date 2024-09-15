using ValVenisBE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ValVenisBE.Controllers
{
    public class QuoteApi
    {
        public static void Map(WebApplication app)
        {
            //Get Quotes
            app.MapGet("/quotes", async (ValVenisBEDbContext db) =>
            {
                var quotes = await db.Quotes.ToListAsync();
                return Results.Ok(quotes);
            });

            //Get Quote by ID
            app.MapGet("/quotes/{id}", [Authorize(Roles = "admin")] (ValVenisBEDbContext db, int id) =>
            {
                var quote = db.Quotes.Find(id);
                if (quote == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(quote);
            });

            //Create Quote
            app.MapPost("/quotes", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, Quote quote) =>
            {
                db.Quotes.Add(quote);
                await db.SaveChangesAsync();
                return Results.Created($"/quotes/{quote.Id}", quote);
            });

            //Update Quote
            app.MapPut("/quotes/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id, Quote updatedQuote) =>
            {
                var quote = db.Quotes.Find(id);
                if (quote == null)
                {
                    return Results.NotFound();
                }
                quote.UserId = updatedQuote.UserId;
                quote.QuoteText = updatedQuote.QuoteText;
                quote.QuoteAuthor = updatedQuote.QuoteAuthor;

                await db.SaveChangesAsync();
                return Results.Ok(quote);
            });

            //Delete Quote
            app.MapDelete("/quotes/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id) =>
            {
                var quote = db.Quotes.Find(id);
                if (quote == null)
                {
                    return Results.NotFound();
                }
                db.Quotes.Remove(quote);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
