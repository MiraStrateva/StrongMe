namespace StrongMe.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Barbell" });
            await dbContext.Categories.AddAsync(new Category { Name = "Dumbbell" });
            await dbContext.Categories.AddAsync(new Category { Name = "Machine/Other" });
            await dbContext.Categories.AddAsync(new Category { Name = "Weighted Bodyweight" });
            await dbContext.Categories.AddAsync(new Category { Name = "Assisted Bodyweight" });
            await dbContext.Categories.AddAsync(new Category { Name = "Reps Only" });
            await dbContext.Categories.AddAsync(new Category { Name = "Cardio" });
            await dbContext.Categories.AddAsync(new Category { Name = "Duration" });

            await dbContext.SaveChangesAsync();
        }
    }
}
