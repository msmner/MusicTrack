using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var roles = new List<Role>
            {
                new Role { Name = "admin"},
                new Role { Name = "user"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var admin = new User { UserName = "admin" };
            await userManager.CreateAsync(admin, "admin");
            await userManager.AddToRolesAsync(admin, new[] { "admin", "user" });

            var user = new User { UserName = "user" };
            await userManager.CreateAsync(user, "user");
            await userManager.AddToRoleAsync(user, "user");
        }

        public static async Task SeedData(MusicTrackDbContext dbContext)
        {
            if (dbContext.Albums.Any()) return;
            var albumOne = new Album
            {
               Name = "test one",
               PublishingYear = 2022,
            };

            var albumsTwo = new Album
            {
                Name = "test two",
                PublishingYear = 2023,
            };

            dbContext.Albums.AddRange(albumOne, albumsTwo);
            await dbContext.SaveChangesAsync();

            //var matchOne = new Match
            //{
            //    PlayerOne = playerOne,
            //    PlayerTwo = playerTwo,
            //    PlayerOneCenturyBreaks = 1,
            //    PlayerOneHalfCenturyBreaks = 3,
            //    PlayerOneMaximums = 5,
            //    PlayerTwoCenturyBreaks = 5,
            //    PlayerTwoHalfCenturyBreaks = 3,
            //    PlayerTwoMaximums = 5,
            //    Score = "3:1",
            //    Description = "match one test description"
            //};

            //var matchTwo = new Match
            //{
            //    PlayerOne = playerOne,
            //    PlayerTwo = playerTwo,
            //    PlayerOneCenturyBreaks = 5,
            //    PlayerOneHalfCenturyBreaks = 2,
            //    PlayerOneMaximums = 5,
            //    PlayerTwoCenturyBreaks = 5,
            //    PlayerTwoHalfCenturyBreaks = 3,
            //    PlayerTwoMaximums = 1,
            //    Score = "4:6",
            //    Description = "match two test description"
            //};

            //dbContext.Matches.AddRange(matchOne, matchTwo);
            //await dbContext.SaveChangesAsync();
        }
    }
}
