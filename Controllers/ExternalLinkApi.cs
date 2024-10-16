using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ValVenisBE.Controllers
{
    public class ExternalLinkApi
    {
        public static void Map(WebApplication app)
        {
            //Get ExternalLinks
            app.MapGet("/externalLinks", async (ValVenisBEDbContext db) =>
            {
                var externalLinks = await db.ExternalLinks.ToListAsync();
                return Results.Ok(externalLinks);
            });

            //Add External Link
            app.MapPost("/externalLinks", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, ExternalLink newExternalLink) =>
            {
                db.ExternalLinks.Add(newExternalLink);
                await db.SaveChangesAsync();
                return Results.Created($"/externalLinks/{newExternalLink.Id}", newExternalLink);
            });

            //Update External Link
            app.MapPut("/externalLinks/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, ExternalLink updatedExternalLink) =>
            {
                var externalLink = await db.ExternalLinks.FindAsync(updatedExternalLink.Id);
                if (externalLink == null)
                {
                    return Results.NotFound();
                }
                externalLink.UserId = updatedExternalLink.UserId;
                externalLink.LinkName = updatedExternalLink.LinkName;
                externalLink.LinkUrl = updatedExternalLink.LinkUrl;

                await db.SaveChangesAsync();
                return Results.Ok(externalLink);
            });

            //Delete External Link
            app.MapDelete("/externalLinks/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id) =>
            {
                var externalLink = await db.ExternalLinks.FindAsync(id);
                if (externalLink == null)
                {
                    return Results.NotFound();
                }
                db.ExternalLinks.Remove(externalLink);
                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
