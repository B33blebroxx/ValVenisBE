﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ValVenisBE.Models;

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
                var quote = await db.Quotes.FindAsync(id);
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
                var quote = await db.Quotes.FindAsync(id);
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
