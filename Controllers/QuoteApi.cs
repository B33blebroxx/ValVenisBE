using ValVenisBE.Models;
using ValVenisBE.Helpers;

namespace ValVenisBE.Controllers
{
    public class QuoteApi
    {
        public static void Map(WebApplication app)
        {
            //Get Quotes
            app.MapGet("/quotes", (ValVenisBEDbContext db) =>
            {
                var quotes = db.Quotes.ToList();
                return Results.Ok(quotes);
            });

            //Get Quote by ID
            app.MapGet("/quotes/{id}", (ValVenisBEDbContext db, int id) =>
            {
                var quote = db.Quotes.Find(id);
                if (quote == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(quote);
            });

            //Create Quote
            app.MapPost("/quotes", (ValVenisBEDbContext db, Quote quote, HttpContext context) =>
            {
                if (!AuthHelper.IsAdmin(context))
                {
                    return Results.Forbid();
                }

                db.Quotes.Add(quote);
                db.SaveChanges();
                return Results.Created($"/quotes/{quote.Id}", quote);
            });

            //Update Quote
            app.MapPut("/quotes/{id}", (ValVenisBEDbContext db, int id, Quote updatedQuote, HttpContext context) =>
            {
                if (!AuthHelper.IsAdmin(context))
                {
                    return Results.Forbid();
                }

                var quote = db.Quotes.Find(id);
                if (quote == null)
                {
                    return Results.NotFound();
                }
                quote.UserId = updatedQuote.UserId;
                quote.QuoteText = updatedQuote.QuoteText;
                quote.QuoteAuthor = updatedQuote.QuoteAuthor;

                db.SaveChanges();
                return Results.Ok(quote);
            });

            //Delete Quote
            app.MapDelete("/quotes/{id}", (ValVenisBEDbContext db, int id, HttpContext context) =>
            {
                if (!AuthHelper.IsAdmin(context))
                {
                    return Results.Forbid();
                }

                var quote = db.Quotes.Find(id);
                if (quote == null)
                {
                    return Results.NotFound();
                }
                db.Quotes.Remove(quote);
                db.SaveChanges();
                return Results.Ok();
            });
        }
    }
}
