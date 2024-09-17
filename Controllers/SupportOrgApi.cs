using ValVenisBE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ValVenisBE.Controllers
{
    public class SupportOrgApi
    {
        public static void Map(WebApplication app)
        {
            //Get Support Orgs
            app.MapGet("/supportorgs", async (ValVenisBEDbContext db) =>
            {
                var orgs = await db.SupportOrgs.ToListAsync();
                return Results.Ok(orgs);
            });

            //Get Support Org by ID
            app.MapGet("/supportorgs/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id) =>
            {
                var org = await db.SupportOrgs.FindAsync(id);
                if (org == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(org);
            });

            //Create Support Org
            app.MapPost("/supportorgs", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, SupportOrg org) =>
            {
                db.SupportOrgs.Add(org);
                await db.SaveChangesAsync();
                return Results.Created($"/supportorgs/{org.Id}", org);
            });

            //Update Support Org
            app.MapPut("/supportorgs/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id, SupportOrg updatedOrg) =>
            {
                var org = await db.SupportOrgs.FindAsync(id);
                if (org == null)
                {
                    return Results.NotFound();
                }
                org.UserId = updatedOrg.UserId;
                org.SupportOrgName = updatedOrg.SupportOrgName;
                org.SupportOrgSummary = updatedOrg.SupportOrgSummary;
                org.SupportOrgPhone = updatedOrg.SupportOrgPhone;
                org.SupportOrgUrl = updatedOrg.SupportOrgUrl;
                org.SupportOrgLogo = updatedOrg.SupportOrgLogo;

                await db.SaveChangesAsync();
                return Results.Ok(org);
            });

            //Delete Support Org
            app.MapDelete("/supportorgs/{id}", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, int id) =>
            {
                var org = await db.SupportOrgs.FindAsync(id);
                if (org == null)
                {
                    return Results.NotFound();
                }
                db.SupportOrgs.Remove(org);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

        }
    }
}
