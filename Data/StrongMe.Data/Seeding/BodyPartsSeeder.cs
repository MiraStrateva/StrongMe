namespace StrongMe.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Models;

    public class BodyPartsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BodyParts.Any())
            {
                return;
            }

            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "Core" });
            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "Arms" });
            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "Back" });
            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "Chest" });
            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "Legs" });
            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "Shoulders" });
            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "FullBody" });
            await dbContext.BodyParts.AddAsync(new BodyPart { Name = "Other" });

            await dbContext.SaveChangesAsync();
        }
    }
}
