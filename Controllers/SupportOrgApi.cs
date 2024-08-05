using ValVenisBE.Models;

namespace ValVenisBE.Controllers
{
    public class SupportOrgApi
    {
        public static void Map(WebApplication app)
        {
            //Get Support Orgs
            app.MapGet("/supportorgs", (ValVenisBEDbContext db) =>
            {
                var orgs = db.SupportOrgs.ToList();
                return Results.Ok(orgs);
            });

            //Get Support Org by ID
            app.MapGet("/supportorgs/{id}", (ValVenisBEDbContext db, int id) =>
            {
                var org = db.SupportOrgs.Find(id);
                if (org == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(org);
            });

            //Create Support Org
            app.MapPost("/supportorgs", (ValVenisBEDbContext db, SupportOrg org) =>
            {
                db.SupportOrgs.Add(org);
                db.SaveChanges();
                return Results.Created($"/supportorgs/{org.Id}", org);
            });

            //Update Support Org
            app.MapPut("/supportorgs/{id}", (ValVenisBEDbContext db, int id, SupportOrg updatedOrg) =>
            {
                var org = db.SupportOrgs.Find(id);
                if (org == null)
                {
                    return Results.NotFound();
                }
                org.SupportOrgName = updatedOrg.SupportOrgName;
                org.SupportOrgSummary = updatedOrg.SupportOrgSummary;
                org.SupportOrgPhone = updatedOrg.SupportOrgPhone;
                org.SupportOrgUrl = updatedOrg.SupportOrgUrl;
                org.SupportOrgLogo = updatedOrg.SupportOrgLogo;

                db.SaveChanges();
                return Results.Ok(org);
            });

            //Delete Support Org
            app.MapDelete("/supportorgs/{id}", (ValVenisBEDbContext db, int id) =>
            {
                var org = db.SupportOrgs.Find(id);
                if (org == null)
                {
                    return Results.NotFound();
                }
                db.SupportOrgs.Remove(org);
                db.SaveChanges();
                return Results.NoContent();
            });

        }
    }
}
