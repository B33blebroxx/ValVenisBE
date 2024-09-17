using ValVenisBE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ValVenisBE.Controllers
{
    public class MissionStatementApi
    {
        public static void Map(WebApplication app)
        {
            //Get Mission Statement
            app.MapGet("/missionstatement", async (ValVenisBEDbContext db) =>
            {
                var missionStatement = await db.MissionStatements.FirstOrDefaultAsync();
                return Results.Ok(missionStatement);
            });

            //Update Mission Statement
            app.MapPut("/missionstatement", [Authorize(Roles = "admin")] async (ValVenisBEDbContext db, MissionStatement updatedMissionStatement) =>
            {
                var missionStatement = await db.MissionStatements.FirstOrDefaultAsync();
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
