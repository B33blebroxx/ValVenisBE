using ValVenisBE.Models;
using ValVenisBE.Helpers;

namespace ValVenisBE.Controllers
{
    public class MissionStatementApi
    {
        public static void Map(WebApplication app)
        {
            //Get Mission Statement
            app.MapGet("/missionstatement", (ValVenisBEDbContext db) =>
            {
                var missionStatement = db.MissionStatements.FirstOrDefault();
                return Results.Ok(missionStatement);
            });

            //Update Mission Statement
            app.MapPut("/missionstatement", (ValVenisBEDbContext db, int id, MissionStatement updatedMissionStatement, HttpContext context) =>
            {
                if (!AuthHelper.IsAdmin(context))
                {
                    return Results.Forbid();
                }

                var missionStatement = db.MissionStatements.FirstOrDefault();
                if (missionStatement == null)
                {
                    return Results.NotFound();
                }
                missionStatement.MissionStatementText = updatedMissionStatement.MissionStatementText;

                db.SaveChanges();
                return Results.Ok(missionStatement);
            });
        }

    }
}
