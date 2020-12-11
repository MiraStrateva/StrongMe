namespace StrongMe.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using StrongMe.Data.Common.Repositories;
    using StrongMe.Data.Models;
    using StrongMe.Services.Mapping;
    using StrongMe.Web.ViewModels.Instructor.Exercises;

    public class ExercisesService : IExercisesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Exercise> exercisesRepository;
        private readonly IRepository<Image> imageRepository;

        public ExercisesService(
            IDeletableEntityRepository<Exercise> exercisesRepository,
            IRepository<Image> imageRepository)
        {
            this.exercisesRepository = exercisesRepository;
            this.imageRepository = imageRepository;
        }

        public async Task CreateAsync(CreateExerciseInputModel input, string userId, string imagePath)
        {
            var recipe = new Exercise
            {
                CategoryId = input.CategoryId,
                BodyPartId = input.BodyPartId,
                Name = input.Name,
                Description = input.Description,
                TrainerId = userId,
            };

            Directory.CreateDirectory($"{imagePath}/exercises/");
            if (input.ImageFiles != null)
            {
                foreach (var image in input.ImageFiles)
                {
                    var extension = Path.GetExtension(image.FileName).TrimStart('.');
                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extension {extension}");
                    }

                    var dbImage = new Image
                    {
                        AddedByUserId = userId,
                        Extension = extension,
                    };
                    recipe.Images.Add(dbImage);

                    var physicalPath = $"{imagePath}/exercises/{dbImage.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.exercisesRepository.AddAsync(recipe);
            await this.exercisesRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.exercisesRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage, string userId)
        {
            var exercises = this.exercisesRepository.AllAsNoTracking()
                .Where(x => x.TrainerId == userId)
                .OrderBy(x => x.CategoryId)
                .ThenBy(x => x.BodyPartId)
                .ThenBy(x => x.Name)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return exercises;
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var exercises = this.exercisesRepository.AllAsNoTracking()
                .Where(x => x.TrainerId == userId)
                .OrderBy(x => x.CategoryId)
                .ThenBy(x => x.BodyPartId)
                .ThenBy(x => x.Name)
                .To<T>().ToList();
            return exercises;
        }

        public T GetById<T>(int id)
        {
            var exercise = this.exercisesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return exercise;
        }

        public async Task DeleteAsync(int id)
        {
            var exercise = this.exercisesRepository.All().FirstOrDefault(x => x.Id == id);
            this.exercisesRepository.Delete(exercise);
            await this.exercisesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditExerciseInputModel input, string userId, string imagePath)
        {
            var exercise = this.exercisesRepository.All().FirstOrDefault(x => x.Id == id);
            exercise.Name = input.Name;
            exercise.Description = input.Description;
            exercise.CategoryId = input.CategoryId;
            exercise.BodyPartId = input.BodyPartId;

            if (input.ImageFiles != null)
            {
                foreach (var image in input.ImageFiles)
                {
                    var extension = Path.GetExtension(image.FileName).TrimStart('.');
                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extension {extension}");
                    }

                    var dbImage = new Image
                    {
                        AddedByUserId = userId,
                        Extension = extension,
                    };
                    exercise.Images.Add(dbImage);

                    var physicalPath = $"{imagePath}/exercises/{dbImage.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.exercisesRepository.SaveChangesAsync();
        }

        public async Task<int> DeleteImageAsync(string id)
        {
            var image = this.imageRepository.All().FirstOrDefault(x => x.Id == id);
            this.imageRepository.Delete(image);
            await this.exercisesRepository.SaveChangesAsync();

            // TO DO: Delete the physical image
            return image.ExerciseId;
        }
    }
}
