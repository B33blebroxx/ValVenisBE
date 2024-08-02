using Microsoft.EntityFrameworkCore;
using ValVenisBE.Models;

namespace ValVenisBE.Controllers
{
    public class UserApi
    {
        public static void Map(WebApplication app)
        {
            //Check User
            app.MapGet("/user/check", (ValVenisBEDbContext db, int id) =>
            {
                var user = db.Users.Find(id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok();
            });
        }
    }
}
