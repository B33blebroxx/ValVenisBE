using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;

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
            app.MapPut("/missionstatement", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, MissionStatement updatedMissionStatement) =>
            {
                var missionStatement = db.MissionStatements.FirstOrDefault();
                if (missionStatement == null)
                {
                    return Results.NotFound();
                }
                missionStatement.MissionStatementText = updatedMissionStatement.MissionStatementText;
                missionStatement.WelcomeMessage = updatedMissionStatement.WelcomeMessage;
                missionStatement.MissionStatementAcronym = updatedMissionStatement.MissionStatementAcronym;
                missionStatement.UserId = updatedMissionStatement.UserId;

                await db.SaveChangesAsync();
                return Results.Ok(missionStatement);
            });
        }

    }
}
